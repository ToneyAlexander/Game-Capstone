using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine.Rendering.PostProcessing;

public class GenerateIsland : MonoBehaviour
{
    //TODO: OPTIMIZATIONS USING DATA STRUCTURES - DONT DO N^3 ALL THE TIME

    private int TILE_SIZE = 16;
    private int TILE_HEIGHT = 4;
    private int WATER_INDEX = 0;
    private int LAND_INDEX = 33;
    private static int TILES_PER_LAYER = 33;

    private Vector3 PlayerStart;
    private bool done = false;

	private PostProcessVolume volume;

	[SerializeField]
	private ProfilesList profileList; 

	[SerializeField]
    private NameGenerator nameGenerator;

	[SerializeField]
	private IslandNameController islandNameDisplay;

    [SerializeField]
    private NavMeshSurface surface;
    private float updateNavMeshTimer = 0f;

    [SerializeField]
    private GameObject remy;
    [SerializeField]
    private Texture2D img;

	private int themeID;

    private string themeString; 

    [SerializeField]
    private int ISLE_WIDE_HIGH;

    [SerializeField]
    private bool makeEnvironment = false;
    [SerializeField]
    private bool makeNavmesh = true;
    [SerializeField]
    private ThemeList treeList;

    [SerializeField]
    private ThemeList grassList;

    [SerializeField]
    private ThemeList mediumObjectList;
    [SerializeField]
    private ThemeList particleEffects;

    [SerializeField]
    private ThemeList specialObjects;

    [SerializeField]
    private ThemeList vegetationSpawner;

    [SerializeField]
	private ThemeList enemySpawner;

	[SerializeField]
    private ThemeDictionary themeDictionary;

	private int themeCount;

	[SerializeField]
    private Vector3 startingLocation = Vector3.zero;

    [SerializeField]
    private bool drawTileSet = false;

    List<TilePiece> tiles;

    private AdjacencyIndex index;

    private List<GameObject> generatedMap;
    private GameObject[,] mapObjects;

    private GameObject terrain;

    bool[,][] ISLAND_REPRESENTATION;

    private NameGenerator g;

    [SerializeField]
    private int forestChance = 100;

    [SerializeField]
    private int snowChance = 20;

    [SerializeField]
    private int swampChance = 20;

    [SerializeField]
    private int desertChance = 20;





    //max 3 right now
    //fix csv/code to be able to work with arbitaray number more
    [SerializeField]
    private int LAYERS_ABOVE_BEACH = 6;

    private int NUMBER_OF_TILES = TILES_PER_LAYER + 1;

    [SerializeField]
    private int LESS_THAN_LAND_REGEN_COUNT = 00;

    [SerializeField]
    private bool useLVR = true;
    [SerializeField]
    private bool RUN_INDEX_TEST = false;

    private bool layersChanged = false;

    // This code is so incredibly ugly rn. Planning on cleaning it up. 
    void Start()
    {
		themeCount = themeDictionary.themeDictionary.Count;
		volume = Camera.main.GetComponent<PostProcessVolume>();

		//TODO: add backtracking and optimize search
		//TODO: remove these lines
		//TODO: optimization - in the findLowestEntropy / tile selection, maintain list of tiles left instead of searching over all tiles
		//AKA iterate over tile list, if entropy = 1, remove

        NUMBER_OF_TILES = TILES_PER_LAYER * (LAYERS_ABOVE_BEACH + 1) + 1;
        //read in file 
        index = IslandGeneratorUtilities.GenerateIndex(img, LAYERS_ABOVE_BEACH);
        if (RUN_INDEX_TEST)
        {
            IslandGeneratorTesting.testIndex(index, LAYERS_ABOVE_BEACH);
        }

        tiles = IslandGeneratorUtilities.CreateTileset(NUMBER_OF_TILES, TILES_PER_LAYER, TILE_HEIGHT);

        //Sets generated map
        createIsland(TILE_SIZE, NUMBER_OF_TILES, LAYERS_ABOVE_BEACH, ISLE_WIDE_HIGH);
        if (makeNavmesh)
        {
            surface.BuildNavMesh();
        }
		string islandName = nameGenerator.generateName();
		islandNameDisplay.DisplayName(islandName);
        done = true;

	}

    void Update()
    {
        if (makeNavmesh)
        {
            // It takes some time for the navMesh to update based on the new island.
            // if (updateNavMeshTimer > 0)
            // {
            //     surface.UpdateNavMesh(surface.navMeshData);
            //     updateNavMeshTimer--;
            // }
            surface.UpdateNavMesh(surface.navMeshData);
        }

        if (Input.GetButtonDown("Regenerate"))
        {
            deleteIsland();
            if (layersChanged)
            {
                index = IslandGeneratorUtilities.GenerateIndex(img, LAYERS_ABOVE_BEACH);
                if (RUN_INDEX_TEST)
                {
                    IslandGeneratorTesting.testIndex(index, LAYERS_ABOVE_BEACH);
                }

                tiles = IslandGeneratorUtilities.CreateTileset(NUMBER_OF_TILES, TILES_PER_LAYER, TILE_HEIGHT);
            }
            createIsland(TILE_SIZE, NUMBER_OF_TILES, LAYERS_ABOVE_BEACH, ISLE_WIDE_HIGH);
            updateNavMeshTimer = 500;
            string islandName = nameGenerator.generateName();
		    Debug.Log(islandName);
		    islandNameDisplay.DisplayName(islandName);
        }
        else if (Input.GetButtonDown("Terrain"))
        {
            makeEnvironment = !makeEnvironment;
            terrain.SetActive(!terrain.activeSelf);
        }
        else if (Input.GetButtonDown("Island Height"))
        {
            Vector3 worldPlace = remy.GetComponent<MousePositionDetector>().CalculateWorldPosition();
            if (remy.GetComponent<MousePositionDetector>().IsValid(worldPlace))
            {
                int tileX = (int)worldPlace.z / TILE_SIZE;
                int tileY = (int)(worldPlace.x - TILE_SIZE/2) / TILE_SIZE;

                //Vector3 aaa = new Vector3(y * TILE_SIZE + TILE_SIZE / 2, height + 1, x * TILE_SIZE);


                cliffToRamp(tileX, tileY, ISLAND_REPRESENTATION, index, NUMBER_OF_TILES, TILE_SIZE, mapObjects);
            }
            /*if (Input.GetAxisRaw("Island Height") > 0)
            {
                LAYERS_ABOVE_BEACH++;
                int adjust = 0;
                if (ISLE_WIDE_HIGH % 6 == 0)
                {
                    adjust = -1;
                }
                if (LAYERS_ABOVE_BEACH >= (ISLE_WIDE_HIGH / 3) - 1 + adjust)
                {
                    LAYERS_ABOVE_BEACH = (ISLE_WIDE_HIGH / 3) - 1 + adjust;
                }

            }
            else if (Input.GetAxisRaw("Island Height") < 0)
            {
                LAYERS_ABOVE_BEACH--;
                if (LAYERS_ABOVE_BEACH < 0)
                {
                    LAYERS_ABOVE_BEACH = 0;
                }
            }
            NUMBER_OF_TILES = TILES_PER_LAYER * (LAYERS_ABOVE_BEACH + 1) + 1;

            layersChanged = true;*/
        }
        else if (Input.GetButtonDown("Island Radius"))
        {
            if (Input.GetAxisRaw("Island Radius") > 0)
            {
                ISLE_WIDE_HIGH++;
            }
            else if (Input.GetAxisRaw("Island Radius") < 0)
            {
                ISLE_WIDE_HIGH--;
                int adjust = 0;
                if (ISLE_WIDE_HIGH % 6 == 0)
                {
                    adjust = -1;
                }
                if (LAYERS_ABOVE_BEACH >= (ISLE_WIDE_HIGH / 3) - 1 + adjust)
                {
                    LAYERS_ABOVE_BEACH = (ISLE_WIDE_HIGH / 3) - 1 + adjust;
                    layersChanged = true;
                }
            }
        }
    }

    #region Setup Methods
    private void collapseStartLocation(Vector2Int beach, int beachID, Vector2Int land, bool[,][] island, int tileCount, List<Vector2Int> updated)
    {
        island[beach.x, beach.y] = IslandTemplateUtilities.makeTile(beachID, tileCount);
        updated.Add(beach);
        island[land.x, land.y] = IslandTemplateUtilities.makeTile(33, tileCount);
        updated.Add(land);
        propagate(island, updated, index);
    }
    #endregion

    #region Make and Draw the Island
    private void createIsland(int tileSize, int tileCount, int layersAboveBeach, int width_height)
    {
        List<Vector2Int> updated = new List<Vector2Int>();

        Texture[] templates = Resources.LoadAll<Texture2D>("IslandTemplateImages");
        Texture template = templates[Random.Range(0, templates.Length)];

        bool[,][] island = IslandTemplateUtilities.initializeIslandFromTemplate(template, width_height, tileCount, WATER_INDEX, updated);
        ISLAND_REPRESENTATION = island;
        propagate(island, updated, index);

        //Guarantee that he'll have a beach
        //TODO: Places it at the top left - put other places later
        Vector2Int start = new Vector2Int();
        Vector2Int firstFlat = new Vector2Int();
        //Don't look at edges - guaranteed to be water

        //TODO: REFACTOR INTO METHOD
        //setupStart(start, firstFlat, island, tileCount, updated, width);

        for (int i = 1; i < width_height-1; i++)
        {
            for (int j = 1; j < width_height - 1; j++)
            {
                start.x = i;
                start.y = j;
                if(island[i, j][17] && island[i - 1, j][33])
                {
                    firstFlat = new Vector2Int(i - 1, j);

                    collapseStartLocation(start, 17, firstFlat, island, tileCount, updated);

                    j = width_height;
                    i = width_height;
                }
                else if (island[i, j][18] && island[i, j-1][33])
                {
                    firstFlat = new Vector2Int(i, j - 1);

                    collapseStartLocation(start, 18, firstFlat, island, tileCount, updated);

                    j = width_height;
                    i = width_height;
                }
                else if (island[i, j][19] && island[i+1, j][33])
                {
                    firstFlat = new Vector2Int(i + 1, j);

                    collapseStartLocation(start, 19, firstFlat, island, tileCount, updated);

                    j = width_height;
                    i = width_height;
                }
                else if (island[i, j][20] && island[i, j + 1][33])
                {
                    firstFlat = new Vector2Int(i, j + 1);

                    collapseStartLocation(start, 20, firstFlat, island, tileCount, updated);

                    j = width_height;
                    i = width_height;
                }
            }
        }
        if(start.x == 0 && start.y == 0)
        {
           //TODO: ERROR - NO BEACH TILE
        }
        SetPlayerStart(start.x, start.y, TILE_HEIGHT, tileSize);
        //WESLEY REMOVE THESE LINES
        remy.transform.position = this.PlayerStart;
        remy.GetComponent<RemyMovement>().setDetination(this.PlayerStart);
        //WESLEY REMOVE THESE LINES

        HashSet<int> fourWayTiles = new HashSet<int>();
        for(int i = 0; i < tiles.Count; i++)
        {
            if(tiles[index.indexToTile(i)].navigability[0] && tiles[index.indexToTile(i)].navigability[1] && tiles[index.indexToTile(i)].navigability[2] && tiles[index.indexToTile(i)].navigability[3])
            {
                fourWayTiles.Add(i);
            }
        }

        Dictionary<Vector2Int, Vector2Int> fullTreeBackpointers = new Dictionary<Vector2Int, Vector2Int>();
        List<Vector2Int> leaves = new List<Vector2Int>();

        //make tree from start to all tiles
        //make end on leave
        //then collapse everything
        int treeType = Random.Range(0, 2);
        switch (treeType)
        {
            case 0:
                createFullTreeDFS(fullTreeBackpointers, leaves, firstFlat, island, fourWayTiles);
                break;
            case 1:
                createFullTreeBFS(fullTreeBackpointers, leaves, firstFlat, island, fourWayTiles);
                break;

        }

        //Draw the tree rays
        /*foreach(KeyValuePair<Vector2Int, Vector2Int> p in fullTreeBackpointers)
        {
            Vector3 a = new Vector3(p.Key.y * tileSize + tileSize / 2, 30, p.Key.x * tileSize);
            Vector3 b = new Vector3(p.Value.y * tileSize + tileSize / 2, 30, p.Value.x * tileSize);
            Debug.DrawRay(a, b - a, Color.magenta, 300);
        }*/

        //Decide on end point and collapse it TODO: (lighthouse, boss tile)
        //TODO: RAMP ISLANDS? - FIND A TILE THAT CAN BE THE TALLEST STARTING AT BOTTOM AND SET THAT ONE TO TALLEST
        //pick random leaf
        //follow back to start
        Vector2Int end = leaves[Random.Range(0, leaves.Count-1)];
        Vector2Int negative = new Vector2Int(-1, -1);

        //create path
        Vector2Int prev = end;
        List<Vector2Int> path = new List<Vector2Int>();
        while (!prev.Equals(negative))
        {
            for(int i = 0; i < island[prev.x, prev.y].Length; i++)
            {
                if(island[prev.x, prev.y][i] && !fourWayTiles.Contains(i))
                {
                    island[prev.x, prev.y][i] = false;
                    updated.Add(prev);
                }
            }
            path.Insert(0, prev);
            prev = fullTreeBackpointers[prev];
        }
        propagate(island, updated, index);

        placePortalOnTileCentered(end.x, end.y, TILE_HEIGHT, tileSize);

        foreach (Vector2Int v in path)
        {
            placeMarkerOnTileCentered(v.x, v.y, TILE_HEIGHT, tileSize);
        }
        //TODO: given a set of tiles and weights, normalize and pick one
        //TODO: give tiles weights

        if (drawTileSet)
        {
            IslandGeneratorTesting.drawTileset(tiles, Vector3.zero, tileSize);
        }

        while (!finished(island))
        {
            observe(island, updated, tileCount);
            propagate(island, updated, index);
        }

        themePicker(); //pick theme before generation 
        generatedMap = drawMap(island, startingLocation, tileSize);
        textureAllTiles();
        terrain = new GameObject();

        if (makeEnvironment)
        {
            replaceObjects(treeList.themeList[themeID].spawnChance, treeList.themeList[themeID].EnvironmentList, "Tree");
            replaceObjects(grassList.themeList[themeID].spawnChance, grassList.themeList[themeID].EnvironmentList, "Grass");
            replaceObjects(mediumObjectList.themeList[themeID].spawnChance, mediumObjectList.themeList[themeID].EnvironmentList, "Rock");
            replaceObjects(particleEffects.themeList[themeID].spawnChance, particleEffects.themeList[themeID].EnvironmentList, "Particles");
            replaceObjects(specialObjects.themeList[themeID].spawnChance, specialObjects.themeList[themeID].EnvironmentList, "SpecialObject");
            replaceObjects(vegetationSpawner.themeList[themeID].spawnChance, vegetationSpawner.themeList[themeID].EnvironmentList, "Vegetation");
            replaceObjects(enemySpawner.themeList[themeID].spawnChance, enemySpawner.themeList[themeID].EnvironmentList, "EnemySpawner");
        }
        else
        {
            replaceObjects(0, treeList.themeList[themeID].EnvironmentList, "Tree");
            replaceObjects(0, grassList.themeList[themeID].EnvironmentList, "Grass");
            replaceObjects(0, mediumObjectList.themeList[themeID].EnvironmentList, "Rock");
            replaceObjects(0, particleEffects.themeList[themeID].EnvironmentList, "Particles");
            replaceObjects(0, specialObjects.themeList[themeID].EnvironmentList, "SpecialObject");
            replaceObjects(0, vegetationSpawner.themeList[themeID].EnvironmentList, "Vegetation");
            replaceObjects(0, enemySpawner.themeList[themeID].EnvironmentList, "EnemySpawner");
        }
    }

    private void createFullTreeBFS(Dictionary<Vector2Int, Vector2Int> fullTreeBackpointers, List<Vector2Int> leaves, Vector2Int firstFlat, bool[,][] island, HashSet<int> fourWayTiles)
    {
        Queue<Vector2Int> locations = new Queue<Vector2Int>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        Queue<KeyValuePair<Vector2Int, Vector2Int>> tempBackpointers = new Queue<KeyValuePair<Vector2Int, Vector2Int>>();
        Vector2Int negative = new Vector2Int(-1, -1);
        tempBackpointers.Enqueue(new KeyValuePair<Vector2Int, Vector2Int>(firstFlat, negative));
        locations.Enqueue(firstFlat);
        while (locations.Count > 0)
        {
            firstFlat = locations.Dequeue();
            KeyValuePair<Vector2Int, Vector2Int> backPair = tempBackpointers.Dequeue();
            if (!visited.Contains(firstFlat))
            {
                fullTreeBackpointers.Add(backPair.Key, backPair.Value);
                visited.Add(firstFlat);
                List<Vector2Int> neighbors = new List<Vector2Int>();
                neighbors.Add(new Vector2Int(firstFlat.x, firstFlat.y - 1));
                neighbors.Add(new Vector2Int(firstFlat.x, firstFlat.y + 1));
                neighbors.Add(new Vector2Int(firstFlat.x + 1, firstFlat.y));
                neighbors.Add(new Vector2Int(firstFlat.x - 1, firstFlat.y));
                bool hasChildren = false;
                while (neighbors.Count > 0)
                {
                    int ind = Random.Range(0, neighbors.Count);
                    Vector2Int n = neighbors[ind];
                    neighbors.RemoveAt(ind);
                    if (!visited.Contains(n) && IslandTemplateUtilities.canBeTile(island[n.x, n.y], fourWayTiles))
                    {
                        tempBackpointers.Enqueue(new KeyValuePair<Vector2Int, Vector2Int>(n, firstFlat));
                        locations.Enqueue(n);
                        hasChildren = true;
                    }
                }
                if (!hasChildren)
                {
                    leaves.Add(firstFlat);
                }
            }
        }
    }

    private void createFullTreeDFS(Dictionary<Vector2Int, Vector2Int> fullTreeBackpointers, List<Vector2Int> leaves, Vector2Int firstFlat, bool[,][] island, HashSet<int> fourWayTiles)
    {
        Stack<Vector2Int> locations = new Stack<Vector2Int>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        Stack<KeyValuePair<Vector2Int, Vector2Int>> tempBackpointers = new Stack<KeyValuePair<Vector2Int, Vector2Int>>();
        Vector2Int negative = new Vector2Int(-1, -1);
        tempBackpointers.Push(new KeyValuePair<Vector2Int, Vector2Int>(firstFlat, negative));
        locations.Push(firstFlat);
        while (locations.Count > 0)
        {
            firstFlat = locations.Pop();
            KeyValuePair<Vector2Int, Vector2Int> backPair = tempBackpointers.Pop();
            if (!visited.Contains(firstFlat))
            {
                fullTreeBackpointers.Add(backPair.Key, backPair.Value);
                visited.Add(firstFlat);
                List<Vector2Int> neighbors = new List<Vector2Int>();
                neighbors.Add(new Vector2Int(firstFlat.x, firstFlat.y - 1));
                neighbors.Add(new Vector2Int(firstFlat.x, firstFlat.y + 1));
                neighbors.Add(new Vector2Int(firstFlat.x + 1, firstFlat.y));
                neighbors.Add(new Vector2Int(firstFlat.x - 1, firstFlat.y));
                bool hasChildren = false;
                while (neighbors.Count > 0)
                {
                    int ind = Random.Range(0, neighbors.Count);
                    Vector2Int n = neighbors[ind];
                    neighbors.RemoveAt(ind);
                    if (!visited.Contains(n) && IslandTemplateUtilities.canBeTile(island[n.x, n.y], fourWayTiles))
                    {
                        tempBackpointers.Push(new KeyValuePair<Vector2Int, Vector2Int>(n, firstFlat));
                        locations.Push(n);
                        hasChildren = true;
                    }
                }
                if (!hasChildren)
                {
                    leaves.Add(firstFlat);
                }
            }
        }
    }

    private List<GameObject> drawMap(bool[,][] island, Vector3 startingLocation, int tileSize)
    {
        List<GameObject> generatedMap = new List<GameObject>();

        this.mapObjects = new GameObject[island.GetLength(0), island.GetLength(1)];

        //Draw the map (all at once - interlace with decision for speed)
        for (int n = 0; n < island.GetLength(0); n++)
        {
            for (int m = 0; m < island.GetLength(1); m++)
            {
                for (int o = 0; o < island[n, m].Length; o++)
                {
                    if (island[n, m][o] && o != WATER_INDEX)
                    {
                        int tileId = index.indexToTile(o);
                        TilePiece currentPiece = tiles[o];
                        GameObject newlyCreatedTile = Instantiate(currentPiece.prefab, startingLocation + currentPiece.modifier, Quaternion.identity);
                        //TODO: REFLECTION
                        //GameObject reflection = Instantiate(currentPiece.prefab, startingLocation + currentPiece.modifier, Quaternion.identity);

                        newlyCreatedTile.transform.Rotate(Vector3.up, currentPiece.rotation);
                        //reflection.transform.Rotate(Vector3.up, currentPiece.rotation);
                        //reflection.transform.localScale = new Vector3(-1, -1, 1);

                        //apply adjustment to tile after rotation
                        if (currentPiece.rotation == 90)
                        {
                            newlyCreatedTile.transform.position += new Vector3(tileSize / 2, 0, tileSize / 2);
                            //reflection.transform.position += new Vector3(tileSize / 2, 0, tileSize / 2);
                        }
                        else if (currentPiece.rotation == 180)
                        {
                            newlyCreatedTile.transform.position += new Vector3(tileSize, 0, 0);
                            //reflection.transform.position += new Vector3(tileSize / 2, 0, tileSize / 2);
                        }
                        else if (currentPiece.rotation == 270)
                        {
                            newlyCreatedTile.transform.position += new Vector3(tileSize / 2, 0, -(tileSize / 2));
                            //reflection.transform.position += new Vector3(tileSize / 2, 0, tileSize / 2);
                        }
                        generatedMap.Add(newlyCreatedTile);
                        mapObjects[n, m] = newlyCreatedTile;
                        break;
                    }
                }
                startingLocation.x += tileSize;
            }
            startingLocation.x = 0;
            startingLocation.z += tileSize;
        }
        return generatedMap;
    }

    private void SetPlayerStart(int x, int y, int height, int tileSize)
    {
        this.PlayerStart = new Vector3(y * tileSize + tileSize / 2, height, x * tileSize);
    }

    public bool Done()
    {
        return this.done;
    }

    public Vector3 GetPlayerStart()
    {
        return this.PlayerStart;
    }

    private void placePortalOnTileCentered(int x, int y, int height, int tileSize)
    {
        Vector3 arenaPosition = new Vector3(-30, 50, -30);
        //TODO: make it the right height for right level, pass in id
        //TODO: make an dactual tile, not hacky
        GameObject teleporter = Instantiate(Resources.Load<GameObject>("Teleporter"));
        GameObject arena = Instantiate(Resources.Load<GameObject>("BossBeetle/Arena"));
        GameObject boss = Instantiate(Resources.Load<GameObject>("BossBeetle/Boss Beetle"));
        arena.transform.position = arenaPosition;
        boss.transform.position = new Vector3(arenaPosition.x, arenaPosition.y, arenaPosition.z - 8);
        boss.transform.localScale = new Vector3(2, 2, 2);
        boss.GetComponent<TrackingBehave>().Target = this.remy;
        teleporter.transform.position = new Vector3(y * tileSize + tileSize / 2, height+1, x * tileSize);

        teleporter.GetComponent<TeleportScript>().TargetX = arenaPosition.x;
        teleporter.GetComponent<TeleportScript>().TargetY = arenaPosition.y;
        teleporter.GetComponent<TeleportScript>().TargetZ = arenaPosition.z;

        AlienBeetle b = boss.GetComponent<AlienBeetle>();
        b.arenaEnd = new Vector3(arenaPosition.x + 32, arenaPosition.y, arenaPosition.z + 32);
        b.arenaStart = new Vector3(arenaPosition.x - 32, arenaPosition.y, arenaPosition.z - 16);
    }
    private void placeMarkerOnTileCentered(int x, int y, int height, int tileSize)
    {
        GameObject marker = Instantiate(Resources.Load<GameObject>("Marker"));
        marker.transform.position = new Vector3(y * tileSize + tileSize / 2, height + 1, x * tileSize);
    }
    #endregion

    #region Terrain Methods
    private void themePicker()
	{
		RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
		byte[] randomNumber = new byte[100];
		rngCsp.GetBytes(randomNumber); 
	    int rnd = (randomNumber[0] % (forestChance + desertChance + snowChance + swampChance));

        if(rnd <= forestChance)
        {
            themeID = 0; //forest
		}
		else if(rnd > forestChance && rnd <= forestChance + desertChance)
        {
            themeID = 1;
        }else if(rnd > forestChance + desertChance && rnd < forestChance + desertChance + snowChance)
        {
            themeID = 2;
        }else if(rnd > forestChance + desertChance + snowChance && rnd <= forestChance + desertChance + snowChance + swampChance)
        {
            themeID = 3;
        }
		volume.profile = profileList.profiles[themeID];
	}

    private void replaceObjects(int percentage, List<GameObject> environmentList, string type)
    {
        GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag(type);
        //a tree's spawn chance shall be 75%
        RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        RNGCryptoServiceProvider rngCsp2 = new RNGCryptoServiceProvider();
        byte[] randomNumber = new byte[100];
        byte[] randomNumber2 = new byte[100];
        foreach (GameObject x in listOfObjects)
        {
            if (environmentList.Count > 0)
            {
                rngCsp.GetBytes(randomNumber);
                byte spawnRoll = (byte)((randomNumber[0] % 100) + 1);
                if (spawnRoll <= percentage) //75% chance
                {
                    rngCsp2.GetBytes(randomNumber2);
                    byte index = (byte)((randomNumber[0] % environmentList.Count));
                    GameObject newObject = Instantiate(environmentList[index], x.transform.position + environmentList[index].transform.position, Quaternion.identity, terrain.transform);
                    newObject.transform.rotation = new Quaternion(0, Random.rotation.y, 0, 1);
                }
            }
            Destroy(x);
        }

    }
    
    /*
    Textures all tiles based off the desired theme.
     */
    private void textureAllTiles()
	{
		string baseFolder = "SquareTiles/TexturedTiles";
		string tileName, materialLocation;
		themeString = themeDictionary.themeDictionary[themeID];
		GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag("Texturable");
		foreach(GameObject tile in listOfObjects)
		{
			tileName = tile.name; 
			materialLocation = string.Format("{0}/{1}/Materials/{2}", baseFolder, tileName, themeString);
			tile.GetComponent<Renderer>().material = new Material(Resources.Load<Material>(materialLocation));
		}
	}

    private void deleteObjects(string type)
    {
        GameObject[] listOfObjects = GameObject.FindGameObjectsWithTag(type);
        foreach (GameObject x in listOfObjects)
        {
            Destroy(x);
        }
    }

    private void deleteIsland()
    {
        //Destroy stuff on terrain
        Destroy(terrain);
        while (generatedMap.Count > 0)
        {
            Object.Destroy(generatedMap[0]);
            generatedMap.RemoveAt(0);
        }
    }
    #endregion

    #region Methods to Solve the CSP
    private void propagate(bool[,][] island, List<Vector2Int> updated, AdjacencyIndex index)
    {
        //defn Propagate(coefficient_matrix):
        //Loop until no more cells are left to be update:
        while (updated.Count > 0)
        {
            Vector2Int current = updated[0];
            updated.RemoveAt(0);

            int x = current.x;
            int y = current.y;
            //For each neighboring cell:

            //Bottom 2
            if (y < island.GetLength(1) - 1)
            {
                if (makeArcConsistent(x, y + 1, x, y, 2, island, index))
                {
                    Vector2Int other = new Vector2Int(x, y + 1);
                    if (!updated.Contains(other))
                    {
                        updated.Add(other);
                    }
                }
            }
            //Left 3
            if (x > 0)
            {
                if (makeArcConsistent(x - 1, y, x, y, 3, island, index))
                {
                    Vector2Int other = new Vector2Int(x - 1, y);
                    if (!updated.Contains(other))
                    {
                        updated.Add(other);
                    }
                }
            }
            //Top 0
            if (y > 0)
            {
                if (makeArcConsistent(x, y - 1, x, y, 0, island, index))
                {
                    Vector2Int other = new Vector2Int(x, y - 1);
                    if (!updated.Contains(other))
                    {
                        updated.Add(other);
                    }
                }
            }
            //Right 1
            if (x < island.GetLength(0) - 1)
            {
                if (makeArcConsistent(x + 1, y, x, y, 1, island, index))
                {
                    Vector2Int other = new Vector2Int(x + 1, y);
                    if (!updated.Contains(other))
                    {
                        updated.Add(other);
                    }
                }
            }
        }
    }

    //Update (x, y)'s values based on what is allowed in (otherX, otherY)
    //Returns whether or not (x,y) had an update
    private bool makeArcConsistent(int x, int y, int otherX, int otherY, int direction, bool[,][] island, AdjacencyIndex index)
    {
        if (IslandTemplateUtilities.countTrues(island[x, y]) <= 1)
        {
            return false;
        }
        bool changed = false;
        for (int i = 0; i < island[x, y].Length; i++)
        {
            if (island[x, y][i])
            {
                bool possible = false;
                //For each pattern that is still potentially valid:
                for (int j = 0; j < island[otherX, otherY].Length; j++)
                {
                    if (island[otherX, otherY][j] && index.isValid(index.indexToTile(j), index.indexToTile(i), direction))
                    {
                        possible = true;
                        j = island[otherX, otherY].Length;
                    }
                }
                //If this point in the pattern no longer matches:
                //Set the array in the wave to false for this pattern
                changed = changed || (possible != island[x, y][i]);
                island[x, y][i] = possible;

                //if possible == false
                //Flag this cell as needing to be updated in the next iteration
                //Go other way - from the other guy to self?
            }
        }
        return changed;
    }

    private void observe(bool[,][] island, List<Vector2Int> updated, int tileCount)
    {
        //defn Observe(coefficient_matrix):
        Vector3Int cell;
        if (useLVR)
        {
            //Use least remaining values (LRV)
            cell = findLowestEntropy(island, tileCount);
        }
        else
        {
            //select random tile
            cell = pickRandomTile(island);
        }
        int m_index = cell.x;
        int n_index = cell.y;

        //If there is a contradiction, throw an error and quit
        //If all cells are at entropy 0, processing is complete:
        //Return CollapsedObservations()
        //Else:
        //Choose a pattern by a random sample, weighted by the pattern frequency in the source data
        //Set the boolean array in this cell to false, except for the chosen pattern
        List<int> unchosenIndices = new List<int>();
        for (int o = 0; o < island[m_index, n_index].Length; o++)
        {
            if (island[m_index, n_index][o])
            {
                unchosenIndices.Add(o);
            }
        }

        if (unchosenIndices.Count == 0)
        {
            return;
        }

        Vector2Int changed = new Vector2Int(m_index, n_index);
        if (!updated.Contains(changed))
        {
            updated.Add(changed);
        }

        int tileIndex = unchosenIndices[weightedIndexSelect(makeDistribution(unchosenIndices))];

        for (int o = 0; o < island[m_index, n_index].Length; o++)
        {
            island[m_index, n_index][o] = (o == tileIndex);
        }
    }

    private List<float> makeDistribution(List<int> unchosenIndices)
    {
        List<List<float>> heuristics = new List<List<float>>();
        List<float> combinedDistribution = new List<float>();
        heuristics.Add(evenDistribution(unchosenIndices));
        heuristics.Add(plateauDistribution(unchosenIndices));
        //heuristics.Add(increasingDistribution(unchosenIndices));
        for (int i = 0; i < unchosenIndices.Count; i++)
        {
            combinedDistribution.Add(1.0f);
            for (int j = 0; j < heuristics.Count; j++)
            {
                combinedDistribution[i] *= heuristics[j][i];
            }
        }
        return combinedDistribution;
    }

    private List<float> evenDistribution(List<int> unchosenIndices)
    {
        List<float> probabilities = new List<float>();
        foreach (int TileID in unchosenIndices)
        {
            probabilities.Add(1.0f);
        }
        return probabilities;
    }
    private List<float> increasingDistribution(List<int> unchosenIndices)
    {
        float increaseAmount = 2f;
        List<float> probabilities = new List<float>();
        float prob = 1.0f;
        foreach (int TileID in unchosenIndices)
        {
            probabilities.Add(prob + increaseAmount * (TileID/33));
        }
        return probabilities;
    }
    private List<float> lowFlatlandsDistribution(List<int> unchosenIndicies)
    {
        return null;
    }
    private List<float> plateauDistribution(List<int> unchosenIndices)
    {
        List<float> probabilities = new List<float>();
        float prob = 1.0f;
        foreach (int TileID in unchosenIndices)
        {
            if (TileID > 0 && TileID % 33 == 0)
            {
                probabilities.Add(unchosenIndices.Count * prob);
            }
            else
            {
                probabilities.Add(prob);
            }
        }
        return probabilities;
    }

    private int weightedIndexSelect(List<float> probabilities)
    {
        float total = 0;
        foreach (float p in probabilities)
        {
            total += p;
        }
        float value = Random.Range(0, total);
        int index = 0;
        while (value > 0)
        {
            value -= probabilities[index];
            index++;
        }
        return index - 1;
    }

    //defn FindLowestEntropy(coefficient_matrix):
    //Return the cell that has the lowest greater-than-zero
    //entropy, defined as:
    //A cell with one valid pattern has 0 entropy
    //A cell with no valid patterns is a contradiction
    //Else: the entropy is based on the sum of the frequency
    //that the patterns appear in the source data, plus
    //Use some random noise to break ties and
    //near-ties.
    private Vector3Int findLowestEntropy(bool[,][] island, int tileCount)
    {
        int minEntropy = tileCount + 1;
        int m_index = 0;
        int n_index = 0;
        for (int m = 0; m < island.GetLength(0); m++)
        {
            for (int n = 0; n < island.GetLength(1); n++)
            {
                int entropy = IslandTemplateUtilities.countTrues(island[m, n]);
                if (entropy > 1 && entropy < minEntropy)
                {
                    minEntropy = entropy;
                    n_index = n;
                    m_index = m;
                    //short circuit on zero entropy
                }
            }
        }
        return new Vector3Int(m_index, n_index, minEntropy);
    }

    //TODO: store available itles instead of regenerating - have to make sure an island works before implementing though so can cancel island
    private Vector3Int pickRandomTile(bool[,][] island)
    {
        List<Vector2Int> availableTiles = new List<Vector2Int>();
        for (int m = 0; m < island.GetLength(0); m++)
        {
            for (int n = 0; n < island.GetLength(1); n++)
            {
                int entropy = IslandTemplateUtilities.countTrues(island[m, n]);
                if (entropy > 1)
                {
                    availableTiles.Add(new Vector2Int(m, n));
                }
            }
        }
        Vector2Int tilepos = availableTiles[Random.Range(0, availableTiles.Count)];
        return new Vector3Int(tilepos.x, tilepos.y, -1);
    }

    private bool finished(bool[,][] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (IslandTemplateUtilities.countTrues(map[i, j]) > 1)
                {
                    return false;
                }
            }
        }
        return true;
    }
    #endregion

    //Debug this child
    private void cliffToRamp(int x, int y, bool[,][] island, AdjacencyIndex index, int TileCount, int TileSize, GameObject[,] mapObjects)
    {
        int tileID = 0;
        for(int i = 0; i < island[x,y].Length; i++)
        {
            if (island[x, y][i])
            {
                tileID = i;
            }
        }
        int idMod = tileID % 33;
        int newID = tileID;
        if(!(idMod >= 1 && idMod <= 12))
        {
            return;
        }
        newID = tileID + 12;
        List<Vector2Int> updated = new List<Vector2Int>();
        island[x, y][tileID] = false;
        island[x, y][newID] = true;
        updated.Add(new Vector2Int(x, y));

        bool[] blank = IslandTemplateUtilities.makeBlankTile(TileCount);

        for(int i = x-1; i <= x + 1; i++)
        {
            for(int j = y-1; j <= y + 1; j++)
            {
                if(i != x && j != y)
                {
                    island[i, j] = (bool[])blank.Clone();
                    updated.Add(new Vector2Int(i, j));
                }
            }
        }
        propagate(island, updated, index);
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                for (int o = 0; o < island[i, j].Length; o++)
                {
                    Destroy(mapObjects[i, j]);

                    int tileId = index.indexToTile(o);
                    TilePiece currentPiece = tiles[o];
                    GameObject newlyCreatedTile = Instantiate(currentPiece.prefab, startingLocation + currentPiece.modifier, Quaternion.identity);
                    //TODO: REFLECTION
                    //GameObject reflection = Instantiate(currentPiece.prefab, startingLocation + currentPiece.modifier, Quaternion.identity);

                    newlyCreatedTile.transform.Rotate(Vector3.up, currentPiece.rotation);
                    //reflection.transform.Rotate(Vector3.up, currentPiece.rotation);
                    //reflection.transform.localScale = new Vector3(-1, -1, 1);

                    //apply adjustment to tile after rotation
                    if (currentPiece.rotation == 90)
                    {
                        newlyCreatedTile.transform.position += new Vector3(TileSize / 2, 0, TileSize / 2);
                        //reflection.transform.position += new Vector3(tileSize / 2, 0, tileSize / 2);
                    }
                    else if (currentPiece.rotation == 180)
                    {
                        newlyCreatedTile.transform.position += new Vector3(TileSize, 0, 0);
                        //reflection.transform.position += new Vector3(tileSize / 2, 0, tileSize / 2);
                    }
                    else if (currentPiece.rotation == 270)
                    {
                        newlyCreatedTile.transform.position += new Vector3(TileSize / 2, 0, -(TileSize / 2));
                        //reflection.transform.position += new Vector3(tileSize / 2, 0, tileSize / 2);
                    }
                    mapObjects[i, j] = newlyCreatedTile;
                }
            }
        }
    }
}