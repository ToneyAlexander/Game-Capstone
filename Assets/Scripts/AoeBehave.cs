using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeBehave : MonoBehaviour
{
    public float ttl;
    public bool friendly = false;
    public Damage dmg;
    public Damage friendDmg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            //Debug.Log("Destoryed due to ttl");
            Destroy(gameObject);
        }
    }


    void OnTriggerStay(Collider col)
    {
        StatBlock enemy = col.gameObject.GetComponent<StatBlock>();
        ControlStatBlock enemyControl = col.gameObject.GetComponent<ControlStatBlock>();
        
        if (enemy != null)
        {
            if (friendly != enemy.Friendly)
            {

                if (enemyControl != null && dmg != null)
                {
                    enemyControl.OnHit(dmg);
                }
            } else
            {
                if (enemyControl != null && friendDmg != null)
                {
                    enemyControl.OnHit(friendDmg);
                }
            }
        }
    }
}
