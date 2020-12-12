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
    
    private void Awake()
    {
        Instance = this;
    }

    public void SetUI(List<Action> actions)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            images[i].sprite = sprites[(int)actions[i]];
        }

        current = 0;
    }

    private int current;
    
    public void GoNext()
    {
        // TODO: use the "used" sprite instead of null
        images[current].sprite = null;
        current++;
    }
    
}
