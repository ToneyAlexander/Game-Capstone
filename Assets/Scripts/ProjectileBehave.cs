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
    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        ttl -= Time.deltaTime;
        if(ttl < 0)
        {
            Debug.Log("Destoryed due to ttl");
            Destroy(gameObject);
        }
    }

    public void PlayAnim(Collider col)
    {

    }

    void OnTriggerEnter(Collider col)
    {
        StatBlock enemy = col.gameObject.GetComponent<StatBlock>();
        ProjectileBehave colProj = col.gameObject.GetComponent<ProjectileBehave>();
        if (colProj == null) //check to see if we collided with another projectile. if so ignore
        {
            Debug.Log("Col with non-proj, Proj is: " + friendly);

            if (enemy != null)
            {
                Debug.Log("Enemy has stat block, enem is: " + enemy.Friendly);
                if (friendly != enemy.Friendly) {
                    enemy.TakeDamage(dmg);
                    PlayAnim(col);
                    Debug.Log("Destroy due to hit non friend");
                    Destroy(gameObject);
                }
            }
            else
            {
                PlayAnim(col);
                Debug.Log("Destroy due to hit non stat char " + col.gameObject.name);
                Destroy(gameObject);
            }
        } else
        {
            Debug.Log("Col with proj");
        }
    }
}
