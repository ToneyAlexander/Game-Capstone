using CCC.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemDropper : MonoBehaviour, ItemDropper
{
    [SerializeField]
    private ItemGenerator gen;

    [SerializeField]
    private int layer;

    private float radius = 3f;

    private float worldItemHeight = .5f;
    private float worldItemHalfFrequency = .25f;
    private float worldItemMargin = 1f;

    void Start()
    {
        for(int i = 0; i < 100; i++)
            DropItem();
    }

    public Item DropItem()
    {
        Item param = gen.GenerateItem();
        //Debug.Log(param.Name);

        float x = Random.Range(-radius, radius);
        float zRange = Mathf.Sqrt(radius*radius - x * x);
        float z = Random.Range(-zRange, zRange);

        Vector3 position = new Vector3(x, this.transform.position.y + worldItemHeight-worldItemHalfFrequency, z);

        Vector3 origin = new Vector3(this.transform.position.x, this.transform.position.y + worldItemHeight - worldItemHalfFrequency, this.transform.position.z);

        RaycastHit r = new RaycastHit();

        //Debug.DrawRay(origin, position-origin, Color.magenta, 200, true);

        //Adjecut itmes that fell inside other things/flew through an object
        if (Physics.Raycast(origin, position-origin, out r, radius, 1 << layer))
        {
            //Debug.Log(r.transform.position);

            position = r.point;
            /*if (position.x > origin.x)
            {
                position.x -= worldItemMargin;
            }
            else if (position.x < origin.x)
            {
                position.x += worldItemMargin;
            }*/

            /*if (position.z > origin.z)
            {
                position.z -= worldItemMargin;
            }
            else if (position.z < origin.z)
            {
                position.z += worldItemMargin;
            }*/

            //adjust x and z to have a border

        }
        position.y += worldItemHeight + worldItemHalfFrequency;


        //Cast ray down to match downhills
        if(Physics.Raycast(position, Vector3.down, out r, radius, 1 << layer)){
            position = r.point;
            position.y += worldItemMargin;
        }

        GameObject i = Instantiate(Resources.Load<GameObject>("WorldItem"), position, Quaternion.identity);
        i.GetComponent<WorldItemScript>().setItem(param);

        return param;
    }
}
