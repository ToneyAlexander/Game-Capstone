using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehave : MonoBehaviour, IAttackIgnored
{
    public Damage dmg;
    public float speed;
    public float ttl;
    public bool friendly = false;
    public bool destroyable = true;

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
            // Debug.Log("Destoryed due to ttl");
            Destroy(gameObject);
        }
    }

    public void PlayAnim(Collider col)
    {
        //override if you need particle effects on hit
    }

    void OnTriggerEnter(Collider col)
    {
        StatBlock enemy = col.gameObject.GetComponent<StatBlock>();
        ControlStatBlock enemyControl = col.gameObject.GetComponent<ControlStatBlock>();
        IAttackIgnored colProj = col.gameObject.GetComponent<IAttackIgnored>();
        if (colProj == null) //check to see if we collided with another projectile. if so ignore
        {
            //Debug.Log("Col with non-proj, Proj is: " + friendly);

            if (enemy != null)
            {
                //Debug.Log("Enemy has stat block, enem is friendly: " + enemy.Friendly);
                if (friendly != enemy.Friendly) {
                    enemy.TakeDamage(dmg, col.gameObject);
                    if(enemyControl != null)
                    {
                        enemyControl.OnHit(dmg);
                    }
                    if(destroyable)
                    {
                        PlayAnim(col);
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                if (destroyable)
                {
                    PlayAnim(col);
                    Destroy(gameObject);
                }
            }
        }
    }
}
