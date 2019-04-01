using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : ScriptableObject
{
    // Start is called before the first frame update
    public enum crewType
    {
        Navigator,
        Hunter,
        Naturalist,
        Explorer
    }
    
    


    [SerializeField]
    private Sprite image;

    [SerializeField]
    private Color tint;

    [SerializeField]
    private int starRating;
    public int StarRating{get{ return starRating; }}

    [SerializeField]
    private int power;
    public int Power{get{ return power; }}

    [SerializeField]
    private string _name;
    public string name { get { return _name; } }

    [SerializeField]
    private crewType cType;
    public crewType CType { get { return cType; } }
    [SerializeField]
    private int typeID = 0;
    public int Type { get { return typeID; } }

    private bool active= false;
    public bool Active { get { return active; } set { active = value; } }


    public void grantBonus()
    {
        

    }

    
}
