using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public ItemData dropItem;
    public int amount = 5;

    public void Harvest(Inventory inventory)
    {
        inventory.AddItem(dropItem, amount);
        Destroy(gameObject);
    }
}
