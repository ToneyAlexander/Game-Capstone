using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: this is bad code, dont duplicate the code from projectile behave if I ever change it, it wont propogate here
//either dont extend projectile behave at all or use the code that exists in it.
public class HorizontalSwipe : ProjectileBehave
{
    /*
    public Damage dmg;
    public float speed;
    public float ttl;
    public bool friendly;
    
    */
    public GameObject player;
    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(player.transform.position, Vector3.up, speed * Time.deltaTime);
        ttl -= speed * Time.deltaTime;
        if (ttl < 0)
        {
            //Debug.Log("Destoryed due to ttl");
            Destroy(gameObject);
        }
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
                if (friendly != enemy.Friendly)
                {
                    enemy.TakeDamage(dmg, col.gameObject);
                    if (enemyControl != null)
                    {
                        enemyControl.OnHit(dmg);
                    }
                    PlayAnim(col);
                    //Debug.Log("Destroy due to hit non friend");
                   // Destroy(gameObject);
                }
            }
            else
            {
                PlayAnim(col);
                //Debug.Log("Destroy due to hit non stat char " + col.gameObject.name);
               // Destroy(gameObject);
            }
        }
    }
    public void PlayAnim(Collider col)
    {
        //override if you need particle effects on hit
    }

    
}
