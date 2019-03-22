using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : ScriptableObject
{
    // Start is called before the first frame update
    enum crewType
    {

    }
    enum crewAttribute
    {
        Daring,
        Wise,
        Playful,
        Suave,
        Sophisticated,
        Clever
    }
    [SerializeField]
    private Sprite image;

    [SerializeField]
    private Color tint;

    [SerializeField]
    private int starRating;
    public int StarRating{get{ return starRating; }}

    [SerializeField]
    private string _name;
    public string name { get { return _name; } }

    public void grantBonus()
    {

    }

    
}
