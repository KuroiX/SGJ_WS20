using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionStation : MonoBehaviour
{
    [HideInInspector]
    public Player player;

    [SerializeField]
    private GameObject canvas;

    public void ActivateActionStation(Player player)
    {
        ResetActions();
        UIManager.Instance.playerUI.SetActive(false);
        this.player = player;
        canvas.SetActive(true);
    }

    public Image[] images;
    //public Sprite[] actionSprites;

    private List<Action> currentActions = new List<Action>();
    
    public void AddAction(Action action)
    {
        AddAction((int) action);
    }
    
    
    public void AddAction(int i)
    {
        if (currentActions.Count >= images.Length)
        {
            Debug.Log("Queue is full!");
            return;
        }
        //Debug.Log("Current index: " + i);
        images[currentActions.Count].sprite = UIManager.Instance.sprites[i];
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
        player.ConfirmQueue(currentActions);
        UIManager.Instance.playerUI.SetActive(true);
        UIManager.Instance.SetUI(currentActions);
        Debug.Log("Queue committed");
        canvas.SetActive(false);
    }

    public GameObject child;

    private void OnTriggerEnter2D(Collider2D other)
    {
        child.SetActive(true);
        other.GetComponent<Player>().ArriveAtStation(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        child.SetActive(false);
        other.GetComponent<Player>().LeaveStation();
    }
}
