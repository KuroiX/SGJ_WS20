using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float maxHeight;
    public float minHeight;
    public float waitTime;
    public float startAfter;
    public float speed;
    public int direction;

    private bool canStart = false;
    private bool waiting = false;
    //private int direction = 1;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(StartAfter());
        if(direction == 1)
        {
            transform.position = new Vector3(transform.position.x, minHeight, 0);
        }

        if (direction == -1)
        {
            transform.position = new Vector3(transform.position.x, maxHeight, 0);
        }
    }

    private void Update()
    {
        if (waiting == false && canStart)
        {
            if (transform.position.y < maxHeight && direction == 1)
            {
               rb.velocity = new Vector2(0, speed * direction);
            }

            if (transform.position.y >= maxHeight && direction == 1)
            {
                rb.velocity = Vector2.zero;
                transform.position=new Vector3(transform.position.x, maxHeight, 0);
                StartCoroutine(wait());
            }
            
            if (transform.position.y > minHeight && direction == -1)
            {
               rb.velocity = new Vector2(0, speed * direction); 
            }

            if (transform.position.y <= minHeight && direction == -1)
            {
                rb.velocity = Vector2.zero;
                transform.position=new Vector3(transform.position.x, minHeight, 0);
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
            other.transform.parent = gameObject.transform;
    }
   
}
