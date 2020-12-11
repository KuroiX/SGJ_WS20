using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStation : MonoBehaviour
{
    public Player player;

    private List<Action> currentActions = new List<Action>();
    
    public void AddAction(Action action)
    {
        currentActions.Add(action);
    }

    public void ResetActions()
    {
        currentActions = new List<Action>();
    }

    public void UpdateUI()
    {
        
    }

    public void CommitQueue()
    {
        for (int i = 0; i < currentActions.Count; i++)
        {
            player.ActionQueue.Enqueue(currentActions[i]);
        }
    }
}
