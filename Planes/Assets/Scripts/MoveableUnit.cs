using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

// When selected, sends ActionMove actions to the action queue on right click
public class MoveableUnit : MonoBehaviour, ISelectable
{
    private Transform myTransform;
    private Vector2 targetPosition;
    private bool selected;
    public float speed;
    public InputAction moveInput;
    public Camera mainCamera;
    private ActionQueue actionQueue;
    public float turnSpeed;
    
    void Start()
    {
        myTransform = GetComponent<Transform>();
        targetPosition = myTransform.position;
        actionQueue = GetComponent<ActionQueue>();
        moveInput.Enable();
    }

    void Update() {
        if (moveInput.WasPerformedThisFrame() & selected)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Ray mouseRay = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            IBombable bombable = Physics2D.GetRayIntersection(mouseRay).collider?.gameObject.GetComponent<IBombable>();
            if ((bombable != null) && gameObject.GetComponent<CanStratBomb>() != null)
            {
                return;//let the CanStratBomb component handle it
            }
            targetPosition = new Vector2(mousePos.x, mousePos.y);
            ActionMove move = new ActionMove(gameObject, targetPosition, speed);
            actionQueue.addAction(move);
        }
    }

    public void OnSelected()
    {
        selected = true;
    }
    public bool IsSelected()
    {
        return selected;
    }
    public void OnDeselected()
    {
        selected = false;
    }
}
