using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFairBall : ProjectileBehave
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

    void FixedUpdate()
    {

    }

    new void PlayAnim(Collider col)
    {
        Debug.Log("Playing animation");
        Vector3 pos = col.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

        if (hit != null)
        {
            var hitInstance = Instantiate(hit, pos, Quaternion.identity);
            if (UseFirePointRotation)
            { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else
            { hitInstance.transform.LookAt(pos); }

            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs == null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }
    }
    
}
