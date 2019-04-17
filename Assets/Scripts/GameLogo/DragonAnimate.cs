using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimate : MonoBehaviour
{
    [SerializeField]
    private GameObject drgaon;

    [SerializeField]
    private string animation;

    // Start is called before the first frame update
    void Start()
    {
        this.drgaon.GetComponent<Animator>().Play(animation);
    }
}
