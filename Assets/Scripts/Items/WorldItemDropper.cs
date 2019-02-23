using CCC.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemDropper : MonoBehaviour, ItemDropper
{
    [SerializeField]
    private ItemGenerator gen;

    private float radius = 3f;

    void Start()
    {
        for(int i = 0; i < 100; i++)
            DropItem();
    }

    public Item DropItem()
    {
        Item param = gen.GenerateItem();
        Debug.Log(param.Name);

        float x = Random.Range(-radius, radius);
        float zRange = Mathf.Sqrt(radius*radius - x * x);
        float z = Random.Range(-zRange, zRange);

        Vector3 position = new Vector3(x, this.transform.position.y + 1, z);

        //while(Physics.Raycast(position, new Vector3(0, 1, 0)))
        //{
        //    position.y++;
        //}
        //MousePositionDetector pattern
        //public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);

        GameObject i = Instantiate(Resources.Load<GameObject>("WorldItem"), position, Quaternion.identity);

        return param;
    }
}
