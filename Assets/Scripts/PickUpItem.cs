using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private MoveController moveController;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private float itemRadius = 1f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckForItems();
        }
    }
    
    private void CheckForItems()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, itemRadius, itemLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Item"))
            {
                PickupItem(collider.gameObject);
            }
        }
    }
    
    private void PickupItem(GameObject item)
    {
        
        Debug.Log("Подобран предмет: " + item.name);
        if (item.TryGetComponent(out Gun gun))
        {
            moveController.hasPistol = true;
            Destroy(item);
        } else if (item.TryGetComponent(out Door door))
        {
            door.GoTo(moveController.transform);
        }
        
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, itemRadius);
    }
}
