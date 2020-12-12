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

    public Text jumpCounter;
    public Text dashCounter;

    public Button jumpButton;
    public Button dashButton;

    public int jumpCount;
    public int dashCount;

    private int currentJumpCount;
    private int currentDashCount;

    private ColorBlock activatedBlock;
    private ColorBlock deactivatedBlock;

    public void Start()
    {
        ResetCounter();
        activatedBlock = jumpButton.colors;
        deactivatedBlock = jumpButton.colors;
        
        Debug.Log(activatedBlock);
        Debug.Log(deactivatedBlock);

        PrintButtonCount();

        deactivatedBlock.normalColor = Color.grey;

        mainCamera = Camera.main;
    }

    void PrintButtonCount()
    {
        jumpCounter.text = "Jump x" + currentJumpCount; 
        dashCounter.text = "Dash x" + currentDashCount; 
    }

    void ResetCounter()
    {
        currentJumpCount = jumpCount;
        currentDashCount = dashCount;
    }

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
        if ((Action) i == Action.Jump)
        {
            currentJumpCount--;
            PrintButtonCount();
            if (currentJumpCount == 0)
            {
                jumpButton.enabled = false;
                jumpButton.colors = deactivatedBlock;
            }
        } 
        else if ((Action) i == Action.Dash)
        {
            currentDashCount--;
            PrintButtonCount();
            if (currentDashCount == 0)
            {
                Debug.Log("Dash is empty");
                dashButton.enabled = false;
                dashButton.colors = deactivatedBlock;
            }
        }
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

        jumpButton.enabled = true;
        jumpButton.colors = activatedBlock;
        dashButton.enabled = true;
        dashButton.colors = activatedBlock;
        ResetCounter();
        PrintButtonCount();
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

    public GameObject HelpText;
    private bool activity = false;
    public void Help()
    {
        HelpText.SetActive(!activity);
        activity = !activity;
    }

    [Header("This is so ugly but for camera")]
    public Camera currentCamera;
    public GameObject actionPanel;
    public Image stationPanelImage;
    public Image queuePanelImage;
    public GameObject backButton;
    
    private Camera mainCamera;
    private bool overviewEnabled = false;
    
    public void SwitchCameras()
    {
        mainCamera.enabled = overviewEnabled;
        actionPanel.SetActive(overviewEnabled);
        stationPanelImage.enabled = overviewEnabled;
        
        currentCamera.enabled = !overviewEnabled;
        queuePanelImage.enabled = !overviewEnabled;
        backButton.SetActive(!overviewEnabled);

        overviewEnabled = !overviewEnabled;
    }
}
