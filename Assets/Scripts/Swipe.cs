using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool _isDragActive = false;
    private Vector2 _startTouchPosition;
    private Vector3 _worldPosition;
    private Draggable _lastDragged;

    // 9/19/25 - Claude AI Fix: Added reference to ItemMovement to pause and resume movement when dragging
    // when the item positions are being updated in ItemMovement.cs, the touch controls have trouble keeping up so Claude suggested pausing the movement of the items when touch control starts
    private ItemMovement _itemMovement;

    private void Awake()
    {
        // make sure there's only one swipe controller -- realistically this check shouldnt be needed because the swipe controller is on the GameManager object but just in case :)
        Swipe[] controller = FindObjectsByType<Swipe>(FindObjectsSortMode.None);
        if(controller.Length > 1)
            Destroy(gameObject);

    }
    void Update()
    {
        if(_isDragActive && (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)) // drop item when touch ends
        {
            Drop(); 
            return;
        }
        if (Input.touchCount > 0) // touchy codey
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) // prevents item movement when touching UI elements
                return;
            _startTouchPosition = Input.GetTouch(0).position;
        }
        else // no touchy no codey
            return;

        _worldPosition = Camera.main.ScreenToWorldPoint(_startTouchPosition); 
        _worldPosition.z = 0; // on touch the object gets moved to like -4000 z so set it back to 0 to fix that

        if (_isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
            if(hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>(); // check if the object touched is draggable 
                if(draggable != null)
                {
                    _lastDragged = draggable;
                    _itemMovement = hit.transform.GetComponent<ItemMovement>();
                    if (_itemMovement != null)
                    {
                        _itemMovement.PauseMovement();
                    }
                    InitDrag(); // drag that thing around twin
                }
            }
        }
    }

    void InitDrag()
    {
        _isDragActive = true;
    }

    void Drag()
    {
        if(_lastDragged != null)
        _lastDragged.transform.position = new Vector3(_worldPosition.x, _worldPosition.y, 0f); // move!! that!! bus!!!
    }
    
    void Drop()
    {
        _isDragActive = false; // stop!! that!! bus!!!
        if (_itemMovement != null)
        {
            _itemMovement.ResumeMovement();
            _itemMovement = null;
        }
    }
}
