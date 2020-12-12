using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHorziontal : MonoBehaviour
{
    public float maxX;
    public float minX;
    public float waitTime;
    public float startAfter;
    public float speed;
    public int direction;

    private bool canStart = false;
    private bool waiting = false;
    //private int direction = 1;
    
    private void Start()
    {
        StartCoroutine(StartAfter());
        if(direction == -1)
        {
            transform.position = new Vector3(minX, transform.position.y, 0);
        }

        if (direction == 1)
        {
            transform.position = new Vector3(maxX, transform.position.y,0);
        }
    }

    private void Update()
    {
        if (waiting == false && canStart)
        {
            if (transform.position.x < maxX && direction == 1)
            {
                transform.Translate(Vector3.right*speed*Time.deltaTime);
            }

            if (transform.position.x >= maxX && direction == 1)
            {
                transform.position=new Vector3(maxX, transform.position.y,0);
                StartCoroutine(wait());
            }
            
            if (transform.position.x > minX && direction == -1)
            {
               //rb.velocity = new Vector2(0, speed * direction); 
               transform.Translate(Vector3.left*speed*Time.deltaTime);
            }

            if (transform.position.x <= minX && direction == -1)
            {
                //rb.velocity = Vector2.zero;
                transform.position=new Vector3(minX, transform.position.y,0);
                StartCoroutine(wait());
            }
        }
    }

    IEnumerator StartAfter()
    {
        yield return new WaitForSeconds(startAfter);
        canStart = true;
    }

    IEnumerator wait()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        if (direction == 1)
            direction = -1;
        else if (direction == -1)
            direction = 1;
        waiting = false;
    }
}