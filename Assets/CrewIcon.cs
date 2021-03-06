﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CrewIcon : MonoBehaviour
{
    public CrewMember cMember;
    public CrewController cController;
    private Text name;
    private Text skill;
    private Image image;
    private static int maxCount = 3;
    private static int count = 0;
    private static int check = 0;
    [SerializeField]
    private bool active = false;

    private int index = 0;

    void Start()
    {
        Debug.Log("CrewMember: " + CrewIcon.count);
        index = transform.GetSiblingIndex();
        Debug.Log("Index: " + index);
        cMember = cController.fullCrew[index];
        EventTrigger ev = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { OnMouseClick((PointerEventData)eventData); });
        ev.triggers.Add(entry);

       // EventTrigger ev = storedButtons[i].GetComponent<EventTrigger>();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback.AddListener((eventData) => { OnMouseOver(); });
        ev.triggers.Add(entry2);

       // EventTrigger ev = storedButtons[i].GetComponent<EventTrigger>();
        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry3.eventID = EventTriggerType.PointerExit;
        entry3.callback.AddListener((eventData) => { OnMouseExit(); });
        ev.triggers.Add(entry3);

        image = gameObject.GetComponent<Image>();

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
        CrewIcon.count = cController.selectedCrew.Count;
        CrewIcon.check = 0;
        foreach (CrewMember c in cController.selectedCrew) 
        {
            if (c == cMember && CrewIcon.check < CrewIcon.count)
            {
                CrewIcon.check++;
                Debug.Log(c + " : "+cMember);
                active = !active;
                Color newColor = new Color();
                ColorUtility.TryParseHtmlString("#FFBC7C", out newColor);
                image.color = newColor;
                //cController.selectedCrew.Add(cMember);
            }

            
        }

    }

    // Update is called once per frame
    void Update()
    {
        cMember = cController.fullCrew[index];
        string stars = new String('☆', cMember.StarRating);
        name.text = cMember.name + "    " + stars;
        skill.text = cMember.CType + cMember.addendum();
    }
    void OnMouseOver()
    {
        Debug.Log("woke");
    }
    void OnMouseClick(PointerEventData data)
    {
        
        if (data.button == PointerEventData.InputButton.Right)
        {
            cController.dismiss(index);
            active = false;
            cController.selectedCrew.Remove(cMember);
        }
        if (data.button == PointerEventData.InputButton.Left)
        {
           // Debug.Log("Toggle");
            if (active)
            {
                active = !active;
                CrewIcon.count -= 1;
                Color x = new Color();
                ColorUtility.TryParseHtmlString("#373737", out x);
                image.color = x;
                cController.selectedCrew.Remove(cMember);
            }
            else
            {
                if (CrewIcon.count < CrewIcon.maxCount)
                {
                    CrewIcon.count += 1;
                    active = !active;
                    Color newColor = new Color();
                    ColorUtility.TryParseHtmlString("#FFBC7C", out newColor);
                    image.color = newColor;
                    cController.selectedCrew.Add(cMember);
                }
            }
            Debug.Log(cController.selectedCrew.Count);

        }
    }
    void OnMouseExit()
    {
        Debug.Log("broke");
    }
}
