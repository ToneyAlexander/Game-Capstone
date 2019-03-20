using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(ControlStatBlock))]
[RequireComponent(typeof(PlayerClass))]
public abstract class BaseBoss : MonoBehaviour, IActivatableBoss
{
    protected bool active;
    protected StatBlock stats;
    protected ControlStatBlock controlStats;
    protected PlayerClass bossClass;
    protected GameObject player;
    protected int expValue;
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
        bossClass.TakePerk(StatPerk);
        bossClass.onLevelUp = LevelPerk;
        for (int i = 0; i < Level; ++i)
        {
            bossClass.LevelUp();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
