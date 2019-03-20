using CCC.GameManagement.GameStates;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClassSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public BloodlineController bc;
    public GameObject title;
    public GameObject desc;

    private PlayIslandGameStateChanger playIslandGameStateChanger;
    private int index = 0;
    private int count;
    private List<GameObject> classIcons = new List<GameObject>() ;

    TextMeshPro titleText;
    TextMeshPro descText;
    Color darkenedColor = new Color(0.29f, 0.29f, 0.29f, 1.0f);
    Color backdropColor = new Color(0.7f, 0.7f, 0.7f);

    #region MonoBehaviour Messages
    private void Awake()
    {
        playIslandGameStateChanger = GetComponent<PlayIslandGameStateChanger>();
    }

    void Start()
    {
         count = bc.ClassList.Count;
        //count = 2;
        titleText = title.GetComponent<TextMeshPro>();
        descText = desc.GetComponent<TextMeshPro>();
        titleText.text = bc.ClassList[index].name;
        descText.text = bc.ClassList[index].description;
        for (int i = 0; i < count; i++)
        {
            GameObject classIcon = new GameObject();
            classIcon.name = bc.ClassList[i].name;
            classIcons.Add(classIcon);
            Vector3 v = computePos(i); 
            classIcon.transform.position = v;
            
            SpriteRenderer sprite = classIcon.AddComponent<SpriteRenderer>();
            sprite.sprite = bc.ClassList[i].image;
            if (index == i)
            {
                sprite.color = Color.white;
            }
            else
            {
                sprite.color = Color.Lerp(backdropColor, darkenedColor, (v.z + 3) / 2.0f);
            }
            
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index--;
            if (index < 0)
            {
                index = count - 1;
            }
            titleText.text = bc.ClassList[index].name;
            descText.text = bc.ClassList[index].description;
            bc.currentClass = bc.ClassList[index];
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index++;
            if (index >= count)
            {
                index = 0;
            }
            titleText.text = bc.ClassList[index].name;
            descText.text = bc.ClassList[index].description;
            bc.currentClass = bc.ClassList[index];
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            bc.currentClass = bc.ClassList[index];
            Debug.Log("class selected: " + bc.ClassList[index].name);
            playIslandGameStateChanger.ChangeGameState();
        }
        for (int i = 0; i < count; i++)
        {
            GameObject classIcon = classIcons[i];
            Vector3 v = computePos(i);
            classIcon.transform.position = Vector3.MoveTowards(classIcon.transform.position, v, 5*Time.deltaTime);

            SpriteRenderer sprite = classIcon.GetComponent<SpriteRenderer>();
           
            if (index == i)
            {
                sprite.color = Color.white;
            }
            else
            {
                sprite.color = Color.Lerp(backdropColor, darkenedColor, (v.z + 3) / 2.0f);
            }
        }
        
    }
    #endregion

    private Vector3 computePos(int i)
    {
        int shiftedindex = (index - i + count) % count;
        float pos =  shiftedindex * 1.0f / count * 2.0f * Mathf.PI;
        float z = -Mathf.Cos(pos)-2;
        float x = -4* Mathf.Sin(pos);
        float y = (z + 1) / 2.0f * 2.25f + 3.75f;
      //  Debug.Log(i +" shifted to "+shiftedindex + " with a position at " + pos);
      //  Debug.Log((z + 3) / 2.0f);
        return new Vector3(x, y, z);
    }
}
