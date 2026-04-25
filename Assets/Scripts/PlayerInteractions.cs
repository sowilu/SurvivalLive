using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public float interactDistance = 3;
    public LayerMask interactLayer;

    private Camera cam;
    private SurvivalStats stats;
    private Inventory inventory;
    
    void Start()
    {
        cam = Camera.main;
        stats = GetComponentInParent<SurvivalStats>();
        inventory = GetComponentInParent<Inventory>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
            {
                if (hit.collider.CompareTag("Water"))
                {
                    stats.Drink(30);
                }
                else if (hit.collider.CompareTag("Food"))
                {
                    stats.Eat(30);
                    Destroy(hit.collider.gameObject);
                }
                else
                {
                    var node = hit.collider.gameObject.GetComponent<ResourceNode>();
                    
                    if (node != null)
                    {
                        node.Harvest(inventory);
                    }
                }
            }
        }
    }
}
