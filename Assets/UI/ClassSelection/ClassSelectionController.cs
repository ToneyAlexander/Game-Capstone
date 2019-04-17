using CCC.GameManagement.GameStates;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(GameStateChanger))]
public sealed class ClassSelectionController : MonoBehaviour
{
    [SerializeField]
    private BloodlineController bloodlineController;

    [SerializeField]
    private TextMeshPro descText;

    [SerializeField]
    private TextMeshPro titleText;

    /// <summary>
    /// The GameStateChanger that will be used to change the game state.
    /// </summary>
    private GameStateChanger gameStateChanger;

    private int index = 0;
    private int count;
    private List<GameObject> classIcons = new List<GameObject>() ;

    private bool selectedClass = false;

    Color darkenedColor = new Color(0.29f, 0.29f, 0.29f, 1.0f);
    Color backdropColor = new Color(0.7f, 0.7f, 0.7f);

    #region MonoBehaviour Messages
    private void Awake()
    {
        gameStateChanger = GetComponent<GameStateChanger>();
    }

    private void Start()
    {
        count = bloodlineController.ClassList.Count;
        titleText.SetText(bloodlineController.ClassList[index].name);
        titleText.ForceMeshUpdate();
        Debug.Log(titleText.text);
        Debug.Log(bloodlineController.ClassList[index].name);
        descText.text = bloodlineController.ClassList[index].description;

        for (int i = 0; i < count; i++)
        {
            GameObject classIcon = new GameObject();
            classIcon.name = bloodlineController.ClassList[i].name;
            classIcons.Add(classIcon);
            Vector3 v = computePos(i); 
            classIcon.transform.position = v;
            
            SpriteRenderer sprite = classIcon.AddComponent<SpriteRenderer>();
            sprite.sprite = bloodlineController.ClassList[i].image;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index--;
            if (index < 0)
            {
                index = count - 1;
            }
            titleText.text = bloodlineController.ClassList[index].name;
            descText.text = bloodlineController.ClassList[index].description;
            bloodlineController.CurrentClass = bloodlineController.ClassList[index];
           // Debug.Log("class selected: " + bc.currentClass.name);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index++;
            if (index >= count)
            {
                index = 0;
            }
            titleText.text = bloodlineController.ClassList[index].name;
            descText.text = bloodlineController.ClassList[index].description;
            bloodlineController.CurrentClass = bloodlineController.ClassList[index];
           // Debug.Log("class selected: " + bc.currentClass.name);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (!selectedClass)
            {
                selectedClass = true;
                // bc.currentClass = bc.ClassList[index];
                Debug.Log("class selected: " + bloodlineController.CurrentClass.name);
                gameStateChanger.ChangeState();
            }
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
