using System;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class LootItem
{
    public GameObject itemPrefab;
    public int amount;
    public float chance;
    public bool getOneOnFailure;
}

public class ResourceNode : MonoBehaviour
{
    public int health = 5;
    public LootItem[] lootItems;
    public ItemData dropItem;
    public int amount = 5;

    private int currentHealth;

    private void Start()
    {
        currentHealth = health;
    }

    public void Harvest(Inventory inventory)
    {
        print(transform.name + " " + currentHealth);
        if (lootItems == null || lootItems.Length == 0)
        {
            inventory.AddItem(dropItem, amount);
            Destroy(gameObject);
        }

        --currentHealth;

        if (currentHealth <= 0)
        {
            foreach (LootItem loot in lootItems)
            {
                if (loot.chance >= Random.Range(0f, 1f))
                {
                    for (int i = 0; i < loot.amount; i++)
                    {
                        SpawnLoot(loot.itemPrefab);
                    }
                }
                else if (loot.getOneOnFailure)
                {
                    SpawnLoot(loot.itemPrefab);
                }
            }
            
            //TODO: animation
            Destroy(gameObject);
        }
    }

    void SpawnLoot(GameObject itemPrefab)
    {
        //random position +-1 m
        var x = Random.Range(-1f, 1f) + transform.position.x;
        var z = Random.Range(-1f, 1f) + transform.position.z;
        var pos = new Vector3(x, transform.position.y + 1, z);
        
        //random rotation
        var rot = new Vector3();
        rot.x = Random.Range(0f, 360f);
        rot.y = Random.Range(0f, 360f);
        rot.z = Random.Range(0f, 360f);
        
        Instantiate(itemPrefab, pos, Quaternion.Euler(rot));
    }
}