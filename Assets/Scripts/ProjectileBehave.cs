using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehave : MonoBehaviour
{
    public Damage dmg;
    public float speed;
    public float ttl;
    public bool friendly = false;

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
        StatBlock enemy = col.gameObject.GetComponent<StatBlock>();
        ProjectileBehave colProj = col.gameObject.GetComponent<ProjectileBehave>();
        if (colProj == null) //check to see if we collided with another projectile. if so ignore
        {
            if (enemy != null)
            {
                if (friendly != enemy.Friendly) {
                    enemy.TakeDamage(dmg);
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
