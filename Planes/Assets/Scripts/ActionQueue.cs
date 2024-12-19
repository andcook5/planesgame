using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ActionQueue : MonoBehaviour
{
    private Queue<Action> actions = new Queue<Action>();
    [SerializeField]
    private bool doActions = false;
    private Action activeAction;
    public int numActions = 0;

    void Update()
    {
        if (!doActions)
        {
            return;
        }
        if (activeAction == null)
        {
            nextAction();
            return;
        }
        if (activeAction.inProgress)
        {
            activeAction.actionUpdate();
        } else
        {
            nextAction();
        }
    }
    public void addAction(Action action)
    {
        actions.Enqueue(action);
        numActions++;
    }
    public void pauseQueue()
    {
        doActions = false;
    }
    public void unpauseQueue()
    {
        doActions = true;
    }
    private void nextAction()
    {
        if (actions.Count == 0)
        {
            return;
        }
        activeAction = actions.Dequeue();
        numActions--;
        activeAction.actionStart();
    }
}

public abstract class Action
{
    public bool inProgress;
    public Action()
    {
        inProgress = false;
    }
    public virtual void actionStart()
    {
        inProgress = true;
    }
    public virtual void actionUpdate()
    {
        inProgress = false;
    }
}