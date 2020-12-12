using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance;

    public GameObject playerUI;
    
    public Image[] images;
    public Sprite[] sprites;
    public Sprite[] usedSprites;
    
    private void Awake()
    {
        Instance = this;
    }

    public void SetUI(List<Action> actions)
    {
        ResetUI();
        
        for (int i = 0; i < actions.Count; i++)
        {
            images[i].sprite = sprites[(int)actions[i]];
            images[i].enabled = true;
        }

        current = 0;
        Debug.Log("UI set");
    }

    private int current;
    
    public void GoNext(Action action)
    {
        // TODO: use the "used" sprite instead of null
        if (current < images.Length)
        {
            images[current].sprite = usedSprites[(int)action];
            current++;
        }
    }

    public void ResetUI()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = null;
            images[i].enabled = false;
        }
    }
    
}
