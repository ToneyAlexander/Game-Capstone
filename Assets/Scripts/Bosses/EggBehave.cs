using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehave : MonoBehaviour, IAttackIgnored
{
    public Damage dmg;
    public float ttl;
    public float maxScale;
    private float scale;
    private float baseTtl;

    // Start is called before the first frame update
    void Start()
    {
        baseTtl = ttl;
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(scale < maxScale)
        scale += 2 * maxScale / baseTtl * Time.deltaTime;
        transform.localScale = new Vector3(scale, scale, scale);
        ttl -= Time.deltaTime;
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
                if (enemy.Friendly)
                {
                    enemy.TakeDamage(dmg);
                    if (enemyControl != null)
                    {
                        enemyControl.OnHit(dmg);
                    }
                    //Debug.Log("Destroy due to hit non friend");
                    Destroy(gameObject);
                }
            }
        }
    }
}
