using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

public class GenerateIsland : MonoBehaviour
{
    //TODO: OPTIMIZATIONS USING DATA STRUCTURES - DONT DO N^3 ALL THE TIME
    //TODO: SWAP N AND M OH FUCK

    private int TILE_SIZE = 16;
    private int TILE_HEIGHT = 4;
    private int WATER_INDEX = 0;
    private int LAND_INDEX = 33;
    private static int TILES_PER_LAYER = 33;

    [SerializeField]
    private NameGenerator nameGenerator;
    
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
    private GameObject terrain;

    private int ISLE_WIDE = 50;
    private int ISLE_HIGH = 50;

    private NameGenerator g;


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
        //TODO: add backtracking and optimize search
        //TODO: remove these lines
        //TODO: optimization - in the findLowestEntropy / tile selection, maintain list of tiles left instead of searching over all tiles
        //AKA iterate over tile list, if entropy = 1, remove
        ISLE_WIDE = ISLE_WIDE_HIGH;
        ISLE_HIGH = ISLE_WIDE_HIGH;

        NUMBER_OF_TILES = TILES_PER_LAYER * (LAYERS_ABOVE_BEACH + 1) + 1;
        //read in file 
        index = generateIndex(img, LAYERS_ABOVE_BEACH);
        if (RUN_INDEX_TEST)
        {
            IslandGeneratorTesting.testIndex(index, LAYERS_ABOVE_BEACH);
        }

        tiles = createTileset(NUMBER_OF_TILES);

        //Sets generated map
        createIsland(TILE_SIZE, NUMBER_OF_TILES, LAYERS_ABOVE_BEACH, ISLE_WIDE, ISLE_HIGH);

        surface.BuildNavMesh();

        Debug.Log(nameGenerator.generateName());
    }

    void Update()
    {
        // It takes some time for the navMesh to update based on the new island.
        // if (updateNavMeshTimer > 0)
        // {
        //     surface.UpdateNavMesh(surface.navMeshData);
        //     updateNavMeshTimer--;
        // }
        surface.UpdateNavMesh(surface.navMeshData);

        if (Input.GetButtonDown("Regenerate"))
        {
            deleteIsland();
            if (layersChanged)
            {
                index = generateIndex(img, LAYERS_ABOVE_BEACH);
                if (RUN_INDEX_TEST)
                {
                    IslandGeneratorTesting.testIndex(index, LAYERS_ABOVE_BEACH);
                }

                tiles = createTileset(NUMBER_OF_TILES);
            }
            createIsland(TILE_SIZE, NUMBER_OF_TILES, LAYERS_ABOVE_BEACH, ISLE_WIDE, ISLE_HIGH);
            updateNavMeshTimer = 500;
            Debug.Log(nameGenerator.generateName());
        }
        else if (Input.GetButtonDown("Terrain"))
        {
            makeEnvironment = !makeEnvironment;
            terrain.SetActive(!terrain.activeSelf);
        }
        else if (Input.GetButtonDown("Island Height"))
        {
            if (Input.GetAxisRaw("Island Height") > 0)
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

            layersChanged = true;
        }
        else if (Input.GetButtonDown("Island Radius"))
        {
            if (Input.GetAxisRaw("Island Radius") > 0)
            {
                ISLE_WIDE_HIGH++;
                ISLE_WIDE = ISLE_WIDE_HIGH;
                ISLE_HIGH = ISLE_WIDE_HIGH;
            }
            else if (Input.GetAxisRaw("Island Radius") < 0)
            {
                ISLE_WIDE_HIGH--;
                ISLE_WIDE = ISLE_WIDE_HIGH;
                ISLE_HIGH = ISLE_WIDE_HIGH;
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

    /*---------- Setup Methods ----------*/

    private List<TilePiece> createTileset(int tileCount)
    {
        //read in TileData
        TextAsset textFile = Resources.Load<TextAsset>("TileData");

        string[] lines = textFile.text.Split('\n');
        List<TilePiece>  tileset = new List<TilePiece>();

        for (int i = 1; i < tileCount + 1; i++)
        {
            if (i <= TILES_PER_LAYER + 1)
            {
                string[] fields = lines[i].Split(',');
                if (fields.Length >= 11)
                {
                    TilePiece t = new TilePiece(Resources.Load<GameObject>(fields[10].Trim()), int.Parse(fields[1]), int.Parse(fields[5]), new Vector3(float.Parse(fields[2]), float.Parse(fields[3]), float.Parse(fields[4])));
                    tileset.Add(t);
                }
            }
            else
            {
                //higher layers
                TilePiece lowClone = tileset[i - TILES_PER_LAYER - 1];
                Vector3 newLoc = new Vector3(lowClone.modifier.x, lowClone.modifier.y + TILE_HEIGHT, lowClone.modifier.z);
                TilePiece t = new TilePiece(lowClone.prefab, lowClone.ID + TILES_PER_LAYER, lowClone.rotation, newLoc);
                tileset.Add(t);
            }
        }
        return tileset;
    }

    private AdjacencyIndex generateIndex(Texture2D adjacencyData, int layersAboveBeach)
    {
        AdjacencyIndex index = new AdjacencyIndex();

        //Get patterns from sample and build propagator
        //THIS READS IMAGE FROM TOP LEFT TO BOTTOM RIGHT, LIKE A BOOK
        //YES IT DOES, UNITY READS IMAGES STRANGELY
        for (int j = adjacencyData.height - 1; j >= 0; j--) //y
        {
            for (int i = 0; i < adjacencyData.width; i++)//x
            {

                if ((int)adjacencyData.GetPixel(i, j).r == 1)
                {
                    continue;
                }

                int pixelValue = pixelToId(adjacencyData.GetPixel(i, j));

                //Top 0
                if (j < adjacencyData.height - 1)
                {
                    int otherPixel = pixelToId(adjacencyData.GetPixel(i, j + 1));
                    if ((int)adjacencyData.GetPixel(i, j + 1).r != 1)
                    {
                        for (int layer = 0; layer <= layersAboveBeach; layer++)
                        {
                            index.Add(pixelValue + 33 * layer, otherPixel + 33 * layer, 0);
                        }
                    }
                }
                //Right 1
                if (i < adjacencyData.width - 1)
                {
                    int otherPixel = pixelToId(adjacencyData.GetPixel(i + 1, j));
                    if ((int)adjacencyData.GetPixel(i + 1, j).r != 1)
                    {
                        for (int layer = 0; layer <= layersAboveBeach; layer++)
                        {
                            index.Add(pixelValue + 33 * layer, otherPixel + 33 * layer, 1);
                        }
                    }
                }
                //Bottom 2
                if (j > 0)
                {
                    int otherPixel = pixelToId(adjacencyData.GetPixel(i, j - 1));
                    if ((int)adjacencyData.GetPixel(i, j - 1).r != 1)
                    {
                        for (int layer = 0; layer <= layersAboveBeach; layer++)
                        {
                            index.Add(pixelValue + 33 * layer, otherPixel + 33 * layer, 2);
                        }
                    }
                }
                //Left 3
                if (i > 0)
                {
                    int otherPixel = pixelToId(adjacencyData.GetPixel(i - 1, j));
                    if ((int)adjacencyData.GetPixel(i - 1, j).r != 1)
                    {
                        for (int layer = 0; layer <= layersAboveBeach; layer++)
                        {
                            index.Add(pixelValue + 33 * layer, otherPixel + 33 * layer, 3);
                        }
                    }
                }
            }
        }
        return index;
    }

    private int pixelToId(Color pixel)
    {
        int rgb = (int)(pixel.r * 255);
        rgb = (rgb << 8) + (int)(pixel.g * 255);
        rgb = (rgb << 8) + (int)(pixel.b * 255);
        return rgb;
    }

    /*---------- Make and Draw the Island ----------*/
    private void createIsland(int tileSize, int tileCount, int layersAboveBeach, int width, int height)
    {
        List<Vector2Int> updated = new List<Vector2Int>();

        Texture[] templates = Resources.LoadAll<Texture2D>("IslandTemplateImages");
        Texture template = templates[3];

        bool[,][] island = IslandTemplateUtilities.initializeIslandFromTemplate(template, width, tileCount, WATER_INDEX, updated);
        propagate(island, updated, index);

        if (drawTileSet)
        {
            IslandGeneratorTesting.drawTileset(tiles, Vector3.zero, tileSize, tileCount);
        }

        //initializeEmptyMap(island, tileCount);
        //initializeCircleMap(island, updated, tileCount, width, height);
        //initializeSquareMap(island, updated, tileCount, width, height);
        //initializeCrescentMap(island, updated, tileCount, width, height);
        //initializeAntiCrescentMap(island, updated, tileCount, width, height);
        //makeCenterTallest(island, updated, tileCount, width, height);
        //placePlayerMaxCenter(island, updated, tileCount, width, height, tileSize, layersAboveBeach);

        while (!finished(island))
        {
            observe(island, updated, tileCount);
            propagate(island, updated, index);
        }
        //Better place player methdo - look at map more
        //Place remy on a beach ramp
        int x = width / 2;
        int y = width / 2;

        //TODO: Guarantee that he'll have a beach
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (island[i, j][17] || island[i, j][18] || island[i, j][19] || island[i, j][20])
                {
                    x = i;
                    y = j;
                    i = width;
                    j = width;
                }
            }
        }
        placePlayerOnTileCentered(x, y, TILE_HEIGHT, tileSize);

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

    private List<GameObject> drawMap(bool[,][] island, Vector3 startingLocation, int tileSize)
    {
        List<GameObject> generatedMap = new List<GameObject>();
        //Draw the map (all at once - interlace with decision for speed)
        for (int n = 0; n < island.GetLength(0); n++)
        {
            for (int m = 0; m < island.GetLength(1); m++)
            {
                for (int o = 0; o < island[m, n].Length; o++)
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

    private void makeCenterTallest(bool[,][] island, List<Vector2Int> updated, int tileCount, int width, int height)
    {
        //FORCE CENTER TO BE TALLEST TILE
        island[width / 2, height / 2] = IslandTemplateUtilities.makeTile(tileCount - 1, tileCount);
        updated.Add(new Vector2Int(width / 2, height / 2));
    }

    private void placePlayerMaxCenter(bool[,][] island, List<Vector2Int> updated, int tileCount, int width, int height, int tileSize, int layersAboveBeach)
    {
        Vector3 remyStart = new Vector3((width / 2) * tileSize + tileSize / 2, TILE_HEIGHT * (layersAboveBeach + 1), (height / 2) * tileSize);
        remy.transform.position = remyStart;
        remy.GetComponent<RemyMovement>().setDetination(remyStart);
    }

    private void placePlayerOnTileCentered(int x, int y, int height, int tileSize)
    {
        Vector3 remyStart = new Vector3(y * tileSize + tileSize / 2, height, x * tileSize);
        remy.transform.position = remyStart;
        remy.GetComponent<RemyMovement>().setDetination(remyStart);
    }

    /*---------- Terrain methods ----------*/

	private void themePicker()
	{
		RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
		byte[] randomNumber = new byte[100];
		rngCsp.GetBytes(randomNumber); 
	    themeID = (byte)((randomNumber[0] % themeCount)); //randomly decide on island
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

    /*---------- Methods to Solve the CSP ----------*/

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
        List<int> unchosenIndecies = new List<int>();
        for (int o = 0; o < island[m_index, n_index].Length; o++)
        {
            if (island[m_index, n_index][o])
            {
                unchosenIndecies.Add(o);
            }
        }

        if (unchosenIndecies.Count == 0)
        {
            return;
        }

        Vector2Int changed = new Vector2Int(m_index, n_index);
        if (!updated.Contains(changed))
        {
            updated.Add(changed);
        }
        int tileIndex = unchosenIndecies[Random.Range(0, unchosenIndecies.Count)];

        for(int r = 0; r < LESS_THAN_LAND_REGEN_COUNT; r++)
        {
            if (tileIndex < LAND_INDEX)
            {
                tileIndex = unchosenIndecies[Random.Range(0, unchosenIndecies.Count)];
            }
        }
        for (int o = 0; o < island[m_index, n_index].Length; o++)
        {
            island[m_index, n_index][o] = (o == tileIndex);
        }
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
}