﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public GameObject hit;
    public GameObject flash;
    
    public Damage dmg;

    void Start ()
    {
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

    void FixedUpdate ()
    {
		if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);         
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("remy")) {
            StatBlock enemy = col.gameObject.GetComponent<StatBlock>();
            if (enemy != null)
            {
                enemy.TakeDamage(dmg, col.gameObject);
            }
        }
        
        speed = 0;   
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

        Destroy(gameObject);
    }
}
