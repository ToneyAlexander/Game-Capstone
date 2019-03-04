using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(ControlStatBlock))]
[RequireComponent(typeof(PlayerClass))]
public class AlienBeetle : MonoBehaviour
{
    private readonly float AbilityZeroCd = 0.5f;
    private readonly float AbilityOneCd = 1f;
    private readonly float AbilityTwoCd = 0.75f;

    private StatBlock stats;
    private ControlStatBlock controlStats;
    private PlayerClass beetleClass;
    private GameObject player;
    public PerkPrototype StatPerk;
    public GameObject EggPrefab;
    public GameObject VolleyPrefab;
    public GameObject TrackerPrefab;
    public int Level;

    private float timeSinceUse;
    private float cooldown;
    private bool inUse;
    private int nextAttack;
    public float arenaEndX, arenaEndZ, arenaStartX, arenaStartZ;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<StatBlock>();
        controlStats = GetComponent<ControlStatBlock>();
        beetleClass = GetComponent<PlayerClass>();
        player = GameObject.FindGameObjectWithTag("Player");

        beetleClass.TakePerk(StatPerk);

        timeSinceUse = 0f;
        cooldown = AbilityZeroCd;
        nextAttack = 0;
        inUse = false;
    }

    void AbilityZero()
    {
        cooldown = AbilityZeroCd;
        //TODO: Play some sort of animation.
        StartCoroutine(AbilZeroAsync());
    }

    IEnumerator AbilZeroAsync()
    {
        int projCast = 0;
        float rangeX = (arenaEndX - arenaStartX) * 0.3f, rangeZ = (arenaEndZ - arenaStartZ) * 0.3f;
        Vector3 target = new Vector3(Random.Range(arenaStartX+rangeX, arenaEndX-rangeX), 0, Random.Range(arenaStartZ+rangeZ, arenaEndZ-rangeZ)); ;
        while (projCast < rangeX*rangeZ/2)
        {
            ++projCast;
            Vector3 pos = new Vector3(target.x + Random.Range(-rangeX, rangeX), 0, target.z + Random.Range(-rangeZ, rangeZ));
            GameObject o = Instantiate(EggPrefab, pos, new Quaternion());
            EggBehave eb = o.GetComponent<EggBehave>();
            eb.ttl = 4.5f;
            eb.maxScale = 3f + Level/4f;
            eb.dmg = stats.RealDamage(new Damage(2f * Level, 2f * Level, false, true, true));
            yield return new WaitForSeconds(2f/(rangeX * rangeZ / 2));
        }
        inUse = false;
        float choice = Random.Range(0f, 1f);
        Debug.Log(choice);
        if(choice < 0.5)
        {
            nextAttack = 1;
        } else if(choice < 0.9)
        {
            nextAttack = 2;
        } else
        {
            nextAttack = 0;
        }
    }

    void AbilityOne()
    {
        cooldown = AbilityOneCd;
        //TODO: Play some sort of animation.
        StartCoroutine(AbilOneAsync());
    }

    IEnumerator AbilOneAsync()
    {
        int projCast = 0;
        while (projCast < 15)
        {
            ++projCast;
            GameObject obj = Instantiate(VolleyPrefab, gameObject.transform.position + new Vector3(0, 0.5f, 0), new Quaternion());
            ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
            var lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            obj.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);

            obj.transform.Rotate(Vector3.up * 90 * Random.Range(-0.03f, 0.03f), Space.World);
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            pbh.speed = 35f;
            Damage dmg = new Damage(0f, Random.Range(17.5f * Level, 22.5f * Level), true, false, false);
            pbh.dmg = stats.RealDamage(dmg);
            pbh.ttl = 3f;

            yield return new WaitForSeconds(0.15f);
        }
        inUse = false;
        float choice = Random.Range(0f, 1f);
        Debug.Log(choice);
        if (choice < 0.6)
        {
            nextAttack = 2;
        }
        else if (choice < 0.85)
        {
            nextAttack = 0;
        }
        else
        {
            nextAttack = 1;
        }
    }

    void AbilityTwo()
    {
        cooldown = AbilityTwoCd;
        //TODO: Play some sort of animation.
        GameObject obj = Instantiate(TrackerPrefab, gameObject.transform.position + new Vector3(0, 0.5f, 0), new Quaternion());
        ProjectileBehave pbh = obj.GetComponent<ProjectileBehave>();
        obj.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        pbh.speed = 9f + Level/5;
        Damage dmg = new Damage(0f, Random.Range(82.5f * Level, 97.5f * Level), true, false, true);
        pbh.dmg = stats.RealDamage(dmg);
        pbh.ttl = 5f /*+ Level/2f*/;//had to remove scaling due to fixed duration of projectile particle system.
        TrackingBehave tbh = obj.GetComponent<TrackingBehave>();
        tbh.RotSpeed = 2.5f + Level/5f;
        tbh.Target = player;


        inUse = false;
        float choice = Random.Range(0f, 1f);
        Debug.Log(choice);
        if (choice < 0.8)
        {
            nextAttack = 0;
        }
        else
        { 
            nextAttack = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!inUse)
            timeSinceUse += Time.deltaTime;

        if(timeSinceUse > cooldown)
        {
            timeSinceUse = 0;
            inUse = true;
            switch(nextAttack)
            {
                case 0:
                    AbilityZero();
                    break;
                case 1:
                    AbilityOne();
                    break;
                case 2:
                    AbilityTwo();
                    break;
            }
        }
    }
}
