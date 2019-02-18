using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{
    Vector3 selectedPos;
    Vector3 hiddenPos;
    Color selectedColor = Color.red;
    Color hiddenColor = new Color(29, 194, 202);
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        selectedPos = new Vector3(transform.localPosition.x, 175, transform.localPosition.z);
        
    }

    // Update is called once per frame
    
    void Update()
    {
       Image col = gameObject.GetComponent<Image>();
        /*
        timer += Time.deltaTime;
        timer = Mathf.Min(timer, 1);
        col.color = Color.Lerp(hiddenColor, selectedColor, timer);
        */
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, selectedPos, 80 * Time.deltaTime);
       // Debug.Log(timer);
    }
}
