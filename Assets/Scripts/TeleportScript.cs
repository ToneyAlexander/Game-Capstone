using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public float TargetX, TargetY, TargetZ;
    private Environment enviro;

    // Start is called before the first frame update
    void Start()
    {
        enviro = GameObject.Find("EnvironmentSound").GetComponent<Environment>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = new Vector3(TargetX,TargetY,TargetZ);

            GameObject.Find("remy").GetComponent<RemyMovement>().setDetination(new Vector3(TargetX, TargetY, TargetZ));

            GameObject.Find("Main Camera").transform.position = new Vector3(TargetX, TargetY + 16, TargetZ);

            enviro.InBossFight = true;

            GameObject[] bosses = GameObject.FindGameObjectsWithTag("BossEnemy");
            foreach(GameObject boss in bosses)
            {
                IActivatableBoss scrpt = boss.GetComponent<IActivatableBoss>();
                if (scrpt != null)
                    scrpt.Activate();
            }
        }
    }
}
