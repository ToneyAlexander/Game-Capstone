using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaritimeController : MonoBehaviour
{

    private struct Island
    {
        public Vector3 position;
        public string name;
        public int theme;
        public GameObject transfer;
        public Island(int i, Vector3 pos, string str, GameObject obj)
        {
            position = pos;
            name = str;
            theme = i;
            transfer = obj;

        }
    }
    public GameObject player;
    public GameObject prefab;
    public ThemeDictionary themes;

    //  public List<Vector3> islandpos = new List<>
    // Start is called before the first frame update\
    int maxnum = 9;
    int minnum = 6;
    int nums;
    float tolerance = 15.0f;
    List<Island> islands = new List<Island>();

    void Start()
    {
       int nums = (int)Mathf.Floor(Random.Range(minnum, maxnum));
        for (int i = 0; i < nums; i++)
        {
            float cos = Mathf.Cos(i / nums * 2 * Mathf.PI);
            float sin = Mathf.Sin(i / nums * 2 * Mathf.PI);
           GameObject x =  Instantiate(prefab, new Vector3(sin*200, 0, cos*200),Quaternion.identity);
            Island isle = new Island(1, new Vector3(sin * 200, 0, cos * 200),"WARG",x);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
