using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refresher : MonoBehaviour
{
    public Color colorActivated;

    public static List<Refresher> refreshers;

    private void Start()
    {
        if (refreshers != null)
        {
            
        }
        else
        {
            refreshers = new List<Refresher>();
        }
        
        refreshers.Add(this);
    }
    
    private bool isEnabled = true;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnabled)
        {
            other.gameObject.GetComponent<Player>().ResetQueue();
            Disable();
        }
    }

    private void Enable()
    {
        isEnabled = true;
        GetComponent<SpriteRenderer>().color = colorActivated;
    }

    private void Disable()
    {
        isEnabled = false;
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    // Funny name lol
    public static void RefreshRefreshers()
    {
        if (refreshers != null)
        {
            foreach (var v in refreshers)
            {
                v.Enable();
            }
        }
    }
}
