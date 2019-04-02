using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CrewIcon : MonoBehaviour
{
    public CrewMember cMember;
    private Text name;
    private Text skill;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("Name"))
            {
                name = transform.GetChild(i).GetComponent<Text>();
            }
            if (transform.GetChild(i).name.Equals("Skill"))
            {
                skill = transform.GetChild(i).GetComponent<Text>();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        string stars = new String('☆', cMember.StarRating);
        name.text = cMember.name + "    " + stars;
        skill.text = cMember.CType + cMember.addendum();
    }
}
