using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBarController : MonoBehaviour
{

    private Image bossFill;

    private Text bossText;

    [SerializeField]
    private Environment environment;

    private GameObject boss;

    private GameObject par;

    // Start is called before the first frame update
    void Start()
    {
        GameObject tmp = GameObject.FindGameObjectWithTag("Generator");
        if (tmp != null){
            environment = tmp.GetComponent<Environment>();
        }

        par = this.transform.GetChild(0).gameObject;
        for (int i = 0; i < par.transform.childCount; i++)
        {
            if (par.transform.GetChild(i).name.Equals("BossFill"))
            {
                GameObject temp = par.transform.GetChild(i).gameObject;
                bossFill = temp.GetComponent<Image>();
            }
            else if (par.transform.GetChild(i).name.Equals("BossText"))
            {
                GameObject temp = par.transform.GetChild(i).gameObject;
                bossText = temp.GetComponent<Text>();
            }

        }
        par.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(environment.InBossFight == true)
        {
            par.transform.gameObject.SetActive(true);
            GameObject[] bosses = GameObject.FindGameObjectsWithTag("BossEnemy");
            if (bosses.Length > 0)
            {
                boss = bosses[0];
                StatBlock stats = boss.GetComponent<StatBlock>();
                bossFill.fillAmount = stats.HealthCur / stats.HealthMax;
            }
        }
        else
        {
            par.transform.gameObject.SetActive(false);
        }
    }
}
