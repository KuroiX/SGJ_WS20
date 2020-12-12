using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionStation : MonoBehaviour
{
    public Player player;

    public GameObject canvas;

    public void ActivateActionStation(Player player)
    {
        this.player = player;
        canvas.SetActive(true);
    }

    public Image[] images;
    public Sprite[] actionSprites;

    private List<Action> currentActions = new List<Action>();
    
    public void AddAction(Action action)
    {
        images[currentActions.Count].sprite = actionSprites[currentActions.Count];
        images[currentActions.Count].enabled = true;
        currentActions.Add(action);
    }
    
    public void AddAction(int i)
    {
        if (currentActions.Count >= images.Length)
        {
            Debug.Log("Queue is full!");
            return;
        }
        images[currentActions.Count].sprite = actionSprites[i];
        images[currentActions.Count].enabled = true;
        currentActions.Add((Action)i);
        Debug.Log("Action added: " + (Action) i);
    }

    public void ResetActions()
    {
        currentActions = new List<Action>();
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = null;
            images[i].enabled = false;
        }
        Debug.Log("Actions reset");
    }

    public void UpdateUI()
    {
        
    }

    public void CommitQueue()
    {
        for (int i = 0; i < currentActions.Count; i++)
        {
            //player.ActionQueue.Enqueue(currentActions[i]);
        }
        Debug.Log("Queue committed");
        canvas.SetActive(false);
    }
}
