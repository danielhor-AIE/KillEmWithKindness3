using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                NPCCharacter npcCharacter = hit.collider.GetComponentInParent<NPCCharacter>();
                Debug.Log(hit.collider.gameObject.name);
                if (npcCharacter != null)
                {
                    npcCharacter.Interact();
                }
            }
        }
    }
}

