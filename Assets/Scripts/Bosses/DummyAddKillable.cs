using CCC.Behaviors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAddKillable : MonoBehaviour, IKillable
{

    public PerkPrototype DefualtPerk;

    public void Die()
    {
        StartCoroutine(DieAsync());
    }

    void Start()
    {
        GetComponent<PlayerClass>().TakePerk(DefualtPerk, false);
    }

    public IEnumerator DieAsync()
    {
        GetComponent<Animator>().SetTrigger("downSpin");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
