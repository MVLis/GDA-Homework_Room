using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _inventory;
    
    private InteractableItem _lastInteractableItem;
    private InteractableItem _lastPickedItem;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HighlightSelectedObject();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUpItem();

            var door = GetSelected<Door>();
            if (door!=null)
            {
                door.SwitchDoorState();
            }
        }

        if (Input.GetMouseButtonDown(0)&&_lastPickedItem!=null)
        {
            _lastPickedItem.ThrowAway(transform.forward);
            _lastPickedItem = null;
        }
    }

    private void TryPickUpItem()
    {
        var interactableObject = GetSelected<InteractableItem>();
        if (interactableObject != _lastPickedItem)
        {
            if (_lastPickedItem != null)
            {
                _lastPickedItem.Drop();
            }

            if (interactableObject!=null)
            {
               interactableObject.PickUp(_inventory);
            }
            
            _lastPickedItem = interactableObject;
        }
    }

    private void HighlightSelectedObject()
    {
        var interactableObject = GetSelected<InteractableItem>();

        if (_lastInteractableItem != interactableObject)
        {
            if (_lastInteractableItem != null)
            {
                _lastInteractableItem.RemoveFocus();
            }

            if (interactableObject != null)
            {
                interactableObject.SetFocus();
            }

            _lastInteractableItem = interactableObject;
        }
    }

    private T GetSelected<T>() where T : MonoBehaviour
    {
        var mousePosition = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out var hitInfo))
        {
            var selected = hitInfo.collider.gameObject.GetComponent<T>();
            return selected;
        }

        return null;
    }
}
