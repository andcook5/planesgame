using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    public InputAction selectAction;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        selectAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectAction.WasPerformedThisFrame())
        {
            Ray mouseRay = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.GetRayIntersection(mouseRay);
            if (hit.collider!=null)
            {
                GameObject hitObject = hit.collider.gameObject;
                ISelectable[] selectables = hitObject.GetComponents<ISelectable>();
                foreach (ISelectable selectable in selectables)   
                {
                    if (selectable.IsSelected())
                    {
                        selectable.OnDeselected();
                    }
                    else
                    {
                        selectable.OnSelected();
                    }
                }
                
            }    
        }
    }
}
public interface ISelectable
{
    void OnSelected();
    void OnDeselected();
    bool IsSelected();
}