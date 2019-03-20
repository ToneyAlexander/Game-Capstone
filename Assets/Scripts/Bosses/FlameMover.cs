using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameMover : ProjectileBehave
{
    public bool UseFirePointRotation;
    public GameObject hit;
    public GameObject flash;

    new void Start()
    {
        base.Start();

        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs == null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
