using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "ScriptableObjects/RecipeData")]
public class RecipeData : ScriptableObject
{
    public string recipeName;
    public GameObject structurePrefab;
    public List<InventoryItem> requirements;
}
