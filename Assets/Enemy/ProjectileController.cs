using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Damage dmg;
    public float speed;
    public float ttl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        ttl -= Time.deltaTime;
        if(ttl < 0)
        {
            Destroy(gameObject);
        }
    }

     void OnTriggerEnter(Collider col)
     {
         if (col.gameObject.name.Equals("remy")) {
             StatBlock enemy = col.gameObject.GetComponent<StatBlock>();
             if (enemy != null)
             {
                 enemy.TakeDamage(dmg);
             }
             Destroy(gameObject);
         }
     }
}
