using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpdater : MonoBehaviour
{
    private StatBlock stats;
    private SimpleHealthBar healthBar;

    void Awake()
    {
        stats = gameObject.transform.parent.transform.parent.GetComponentInParent<StatBlock>();
        healthBar = GetComponent<SimpleHealthBar>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("max = " + stats.HealthMax);
            Debug.Log("cur = " + stats.HealthCur);
            Debug.Log("bas = " + stats.HealthBase);
        }

        healthBar.UpdateBar(stats.HealthBase, stats.HealthMax);
    }
}
