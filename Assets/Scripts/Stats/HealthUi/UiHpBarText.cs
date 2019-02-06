using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHpBarText : MonoBehaviour
{

    StatBlock stats;
    Text hpText;
    // Start is called before the first frame update
    void Start()
    {
        hpText = GetComponent<Text>();
        stats = gameObject.transform.parent.GetComponentInParent<StatBlock>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stats != null)
        {
            hpText.text = "" + (int)stats.HealthCur/* + "/" + (int)stats.HealthMax*/;
        }
    }
}
