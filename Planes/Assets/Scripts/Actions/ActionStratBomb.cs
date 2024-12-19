using UnityEngine;

public class ActionStratBomb : Action
{
    CanStratBomb subject;
    IBombable target;
    Collider subjectCollider;
    public ActionStratBomb(CanStratBomb subj, IBombable targ)
    {
        subject = subj;
        target = targ;
    }
    public override void actionStart()
    {
        inProgress = true;
        PlaneInventory planes = subject.getInventory();
        foreach(Plane plane in planes.getPlanes())
        {
            if (Random.value < plane.bombAccuracy)
            {
                target.takeBombDamage(plane.bombCapacity);
            }
        }
    }
}
