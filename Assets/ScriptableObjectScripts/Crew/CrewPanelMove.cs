using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewPanelMove : MonoBehaviour
{
    Vector3 visiblePos = new Vector3(159,0,0);
    Vector3 hiddenPos = new Vector3(615, 0, 0);
    bool shown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            shown = !shown;
        }
        if (shown)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, visiblePos, 300 * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, hiddenPos, 300 * Time.deltaTime);
        }
    }
}
