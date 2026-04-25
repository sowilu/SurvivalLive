using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public RecipeData currentRecipe;
    public float buildDistance = 5;
    public LayerMask buildMask;

    private GameObject preview;
    private Inventory inventory;
    private Camera cam;
    
    void Start()
    {
        inventory = GetComponent<Inventory>();
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && preview != null)
        {
            preview.SetActive(!preview.activeSelf);
        }
        else if (Input.GetMouseButtonUp(0) && preview.activeSelf)
        {
            TryBuild();
        }
        HandlePreview();
    }

    void TryBuild()
    {
        if (inventory.HasItems(currentRecipe.requirements))
        {
            inventory.RemoveItems(currentRecipe.requirements);
            Instantiate(currentRecipe.structurePrefab, preview.transform.position, preview.transform.rotation);
        }
        else
        {
            print("Not enough resources to build");
        }
    }

    void HandlePreview()
    {
        if (preview == null)
        {
            
            preview = Instantiate(currentRecipe.structurePrefab);
            preview.SetActive(false);
        }
        
        var position = transform.position + transform.forward * buildDistance;
        position.y = 20;
        //print(position);
        
        //preview.transform.position = position;

        if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, 30, buildMask))
        {
            print("test");
            preview.transform.position = hit.point;
        }
    }
}
