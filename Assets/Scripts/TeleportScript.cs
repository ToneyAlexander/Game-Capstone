using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public float TargetX, TargetY, TargetZ;
    private Environment enviro;
    private float timeAlive;
    public bool exitingFight;

    // Start is called before the first frame update
    void Awake()
    {
        enviro = GameObject.Find("EnvironmentSound").GetComponent<Environment>();
        timeAlive = 0f;
        exitingFight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive < 1.5f)
            timeAlive += Time.deltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if (timeAlive >= 1.5f)
        {
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.transform.position = new Vector3(TargetX, TargetY, TargetZ);

                GameObject.Find("remy").GetComponent<RemyMovement>().setDetination(new Vector3(TargetX, TargetY, TargetZ));

                GameObject.Find("Main Camera").transform.position = new Vector3(TargetX, TargetY + 16, TargetZ);

                if(!exitingFight)
                {

                    enviro.InBossFight = true;

                    GameObject[] bosses = GameObject.FindGameObjectsWithTag("BossEnemy");
                    foreach (GameObject boss in bosses)
                    {
                        IActivatableBoss scrpt = boss.GetComponent<IActivatableBoss>();
                        if (scrpt != null)
                            scrpt.Activate();
                    }
                } else
                {
                    enviro.InBossFight = false;
                }
            }
        }
    }
}
