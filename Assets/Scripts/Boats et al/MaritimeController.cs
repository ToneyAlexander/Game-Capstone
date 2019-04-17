using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaritimeController : MonoBehaviour
{

    public BloodlineController bc;
    public GameObject player;
    public GameObject prefab;
    public ThemeDictionary themes;
    public NameGenerator nameGen;
    public CrewController crewController;
    
    //private GameStateChanger gameStateChanger;
    public GameObject info;
    public IslandStorage islandStorage;
    Vector3 hiddenPos = new Vector3(-960, 0, 0);
    Vector3 visiblePos = new Vector3(-1360, 0, 0);
    bool shown= false;
    public string guideText = "Press H to show/hide";

    //  public List<Vector3> islandpos = new List<>
    // Start is called before the first frame update\
    int maxnum = 9;
    int minnum = 6;
    int nums;
    float tolerance = 15.0f;
    public List<IslandData.Island> islands = new List<IslandData.Island>();

    public Text islandName;
    public Text infoText;
    public Text descText;

    #region MonoBehaviour Messages
    private void Start()
    {
       float nums = Mathf.Floor(Random.Range(minnum, maxnum));
        for (int i = 0; i < nums; i++)
        {
            float cos = Mathf.Cos(i / nums * 2 * Mathf.PI);
            float sin = Mathf.Sin(i / nums * 2 * Mathf.PI);
            GameObject x =  Instantiate(prefab, new Vector3(sin*200, 0, cos*200),Quaternion.identity);
            
            x.GetComponent<positionText>().player = player;
            x.GetComponent<positionText>().themeDictionary = themes;
            x.GetComponent<positionText>().crewController = crewController;
            //  x
            string name = nameGen.generateName();
            Debug.Log(name);
            x.GetComponent<positionText>().setName(name);
            x.GetComponent<positionText>().maritimeController = gameObject;

            IslandData.Island isle = new IslandData.Island(1, new Vector3(sin * 200, 0, cos * 200),name,x);
        }

        for(int j = 0; j < info.transform.childCount; j++)
        {
            if(info.transform.GetChild(j).name.Equals("DynText"))
            {
                infoText = info.transform.GetChild(j).GetComponent<Text>();
            }
            else if(info.transform.GetChild(j).name.Equals("IslandName"))
            {
                islandName = info.transform.GetChild(j).GetComponent<Text>();
            }
            else if (info.transform.GetChild(j).name.Equals("Desc"))
            {
                descText = info.transform.GetChild(j).GetComponent<Text>();
            }
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            shown = !shown;
        }
        if (shown)
        {
            info.transform.localPosition = Vector3.MoveTowards(info.transform.localPosition, visiblePos, 800 * Time.deltaTime);
        }
        else
        {
            info.transform.localPosition = Vector3.MoveTowards(info.transform.localPosition, hiddenPos, 800 * Time.deltaTime);
        }
    }
    #endregion
}
