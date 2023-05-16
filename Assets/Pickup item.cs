using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemPickup : MonoBehaviour
{
    public float pickupRadius = 3f; // Radius within which items can be picked up
    public KeyCode pickupKey = KeyCode.E; // Button to pick up items

    private List<Item> nearbyItems = new List<Item>(); // List to store nearby items

    private void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            // Iterate through the nearby items and pick them up
            foreach (Item item in nearbyItems)
            {
                PickUpItem(item);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            nearbyItems.Add(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            nearbyItems.Remove(item);
        }
    }

    private void PickUpItem(Item item)
    {
        // Perform item pickup logic here
        Debug.Log("Picked up item: " + item.name);
        Destroy(item.name);
    }

    private void Destroy(string name)
    {
        throw new NotImplementedException();
    }
}
