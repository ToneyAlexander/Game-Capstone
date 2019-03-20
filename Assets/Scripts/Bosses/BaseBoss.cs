using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(ControlStatBlock))]
[RequireComponent(typeof(PlayerClass))]
public abstract class BaseBoss : MonoBehaviour, IActivatableBoss
{
    protected StatBlock stats;
    protected ControlStatBlock controlStats;
    protected PlayerClass bossClass;
    protected GameObject player;
    protected Damage collideDmg;
    protected bool inUse;
    protected int expValue;
    private float dmgTimer;
    public bool active;
    public int Level;
    public PerkPrototype StatPerk;
    public PerkPrototype LevelPerk;
    public Vector3 arenaStart, arenaEnd;

    public void Activate()
    {
        active = true;
    }

    public void IsKilled()
    {
        active = false;
        Collider col = GetComponent<Collider>();
        if (col != null)
            Destroy(col);
        if (player != null)
            player.GetComponent<PlayerClass>().ApplyExp(expValue);
        StartCoroutine(Die());
    }

    protected void SpawnTeleportOut()
    {
        GameObject tmp = GameObject.FindGameObjectWithTag("Generator");
        if (tmp != null)
        {
            GenerateIsland gen = tmp.GetComponent<GenerateIsland>();
            Debug.Log("added a way out");
            GameObject tele = Instantiate(Resources.Load<GameObject>("Teleporter"));
            TeleportScript tp = tele.GetComponent<TeleportScript>();
            tp.exitingFight = true;
            tp.TargetX = gen.GetPlayerStart().x;
            tp.TargetY = gen.GetPlayerStart().y;
            tp.TargetZ = gen.GetPlayerStart().z;
            tele.transform.position = arenaStart + ((arenaEnd - arenaStart) / 2);
        }
    }

    protected abstract IEnumerator Die();

    protected void Awake()
    {
        stats = GetComponent<StatBlock>();
        controlStats = GetComponent<ControlStatBlock>();
        bossClass = GetComponent<PlayerClass>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    protected void Start()
    {

        dmgTimer = 1f;
        bossClass.TakePerk(StatPerk);
        bossClass.onLevelUp = LevelPerk;
        for (int i = 0; i < Level; ++i)
        {
            bossClass.LevelUp();
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if (dmgTimer < 2f)
            dmgTimer += Time.deltaTime;
    }

    void OnTriggerStay(Collider col)
    {
        if (dmgTimer > 0.5f && !inUse)
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
                        dmgTimer = 0f;

                        enemy.TakeDamage(collideDmg, col.gameObject);
                        if (enemyControl != null)
                        {
                            enemyControl.OnHit(collideDmg);
                        }
                    }
                }
            }
        }
    }
}
