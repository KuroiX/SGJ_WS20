using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refresher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>().ResetQueue();
    }
}
