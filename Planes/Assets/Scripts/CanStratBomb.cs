using UnityEngine;
using UnityEngine.InputSystem;

public class CanStratBomb : MonoBehaviour, ISelectable
{
    private PlaneInventory planes;
    [SerializeField]
    private bool selected = false;
    public InputAction attackInput;
    public Camera mainCamera;
    private ActionQueue actions;

    public PlaneInventory getInventory()
    {
        return planes;
    }
    public bool IsSelected()
    {
        return selected;
    }

    public void OnDeselected()
    {
        selected = false;
    }

    public void OnSelected()
    {
        selected=true;
    }

    void Start()
    {
        planes = GetComponent<PlaneInventory>();
        actions = GetComponent<ActionQueue>();
        attackInput.Enable();
    }

    void Update()
    {
        if(selected & attackInput.WasPerformedThisFrame())
        {
            Ray mouseRay = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            GameObject hitObject = Physics2D.GetRayIntersection(mouseRay).collider?.gameObject;
            if (hitObject!=null)
            {
                IBombable[] targets = hitObject.GetComponents<IBombable>();
                if (targets.Length > 0)
                {
                    Vector2 targetPos = new Vector2(hitObject.transform.position.x,hitObject.transform.position.y);
                    actions.addAction(new ActionMove(gameObject, targetPos, gameObject.GetComponent<MoveableUnit>().speed));
                }
                foreach(IBombable target in targets)
                {
                    actions.addAction(new ActionStratBomb(this, target));

                }
            }
        }
    }
}
public interface IBombable
{
    public void takeBombDamage(float bombWeight);
}