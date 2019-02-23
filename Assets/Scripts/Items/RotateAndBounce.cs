using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndBounce : MonoBehaviour
{
    [SerializeField]
    private float deltaT = .02f;

    [SerializeField]
    private float heightScale = .25f;

    [SerializeField]
    private float rotateRate = .7f;

    private float t = 0;
    private float ypos;
    // Start is called before the first frame update
    void Start()
    {
        ypos = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, rotateRate, 0));
        t += deltaT;
        this.transform.position = new Vector3(this.transform.position.x, ypos + heightScale * Mathf.Sin(Mathf.PI * t), this.transform.position.z);
        t = t % 2;
    }
}
