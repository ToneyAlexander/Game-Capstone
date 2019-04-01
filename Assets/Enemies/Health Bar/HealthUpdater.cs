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
            Debug.Log("cur = " + stats.HealthCur);
            Debug.Log("max = " + stats.HealthMax);
        }

        healthBar.UpdateBar(stats.HealthCur, stats.HealthMax);
    }
}
