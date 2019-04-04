using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMenuPositioner : MonoBehaviour
{
    private Vector3 hiddenPos = new Vector3(615.0f,0.0f,0.0f);
    private Vector3 basePos = new Vector3(158.9f, 0.0f, 0.0f);
  //  private Vector3 currentPos = hiddenPos;
    private bool visible = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            visible = !visible;
        }
        if (visible)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, basePos, 100 * Time.deltaTime);

        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, hiddenPos, 100 * Time.deltaTime);

        }

    }
}
