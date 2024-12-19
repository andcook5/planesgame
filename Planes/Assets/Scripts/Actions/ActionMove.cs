using UnityEngine;
using UnityEngine.UIElements;

public class ActionMove : Action
{
    private Transform myTransform;
    private GameObject subject;
    private Vector2 targetPosition;
    private float speed;
    public ActionMove(GameObject subj, Vector2 tgtpos, float spd)
    {
        subject = subj;
        targetPosition = tgtpos;
        myTransform = subject.transform;
        speed = spd;
    }
    public override void actionStart()
    {
        inProgress = true;
        Vector3 displacement = myTransform.position - new Vector3(targetPosition.x, targetPosition.y, 0);
        float dispAngle = Vector3.Angle(myTransform.right, displacement.normalized);
        myTransform.Rotate(0, 0, 180-dispAngle);
    }
    public override void actionUpdate()
    {
        
        myTransform.position = Vector3.MoveTowards(myTransform.position,new Vector3(targetPosition.x,targetPosition.y,0),speed*Time.deltaTime); 
        if (myTransform.position == new Vector3(targetPosition.x,targetPosition.y,0))
        {
            inProgress = false;
        }
    }

}
