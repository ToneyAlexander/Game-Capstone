using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Items;


public class InventoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onClick()
    {
        int index = transform.GetSiblingIndex();
        InventoryUI selected = transform.parent.parent.gameObject.GetComponent<InventoryUI>();
        selected.euser.EquipItem(selected.user.Items[index]);
    }
}
