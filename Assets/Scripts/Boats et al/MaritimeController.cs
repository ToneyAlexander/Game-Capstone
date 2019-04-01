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
    
    //private GameStateChanger gameStateChanger;
    public GameObject info;
    public IslandStorage islandStorage;
    Vector3 hiddenPos = new Vector3(-520, 0, 0);
    Vector3 visiblePos = new Vector3(-300, 0, 0);
    bool shown= false;
    public string guideText = "Press H to show/hide";

    //  public List<Vector3> islandpos = new List<>
    // Start is called before the first frame update\
    int maxnum = 9;
    int minnum = 6;
    int nums;
    float tolerance = 15.0f;
    public List<IslandData.Island> islands = new List<IslandData.Island>();

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
            //  x
            string name = nameGen.generateName();
            Debug.Log(name);
            x.GetComponent<positionText>().setName(name);
            
            IslandData.Island isle = new IslandData.Island(1, new Vector3(sin * 200, 0, cos * 200),name,x);
        }

    }

    private void Update()
    {
        info.transform.GetChild(0).gameObject.GetComponent<Text>().text = guideText;
        if (Input.GetKeyDown(KeyCode.H))
        {
            shown = !shown;
        }
        if (shown)
        {
            info.transform.localPosition = Vector3.MoveTowards(info.transform.localPosition, visiblePos, 100 * Time.deltaTime);
        }
        else
        {
            info.transform.localPosition = Vector3.MoveTowards(info.transform.localPosition, hiddenPos, 100 * Time.deltaTime);
        }
    }
    #endregion
}
