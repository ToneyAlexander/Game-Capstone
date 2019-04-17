using CCC.Behaviors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAddKillable : MonoBehaviour, IKillable
{
    public bool IsDead;
    public PerkPrototype DefualtPerk;

    public void Die()
    {
        IsDead = true;
        StartCoroutine(DieAsync());
    }

    void Start()
    {
        GetComponent<PlayerClass>().TakePerk(DefualtPerk, false);
        IsDead = false;
    }

    public IEnumerator DieAsync()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
            Destroy(col);
        GetComponent<Animator>().SetTrigger("DownSpin");
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
