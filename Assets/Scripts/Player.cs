using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Queue<Action> ActionQueue;
    public float speed;
    public float jumpHeight;
    public float dashDistance;
    private Rigidbody2D rb;
    private bool action = false;
    private int direction = 1;
    private SpriteRenderer sr;
    private ActionStation aStation;
    private bool hasStation = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (action == false)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                direction = 1;
                sr.flipX = false;
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                direction = -1;
                sr.flipX = true;
            }

            float amtToMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(Vector3.right * amtToMove);
        }

        if (Input.GetButtonDown("Jump") && action == false)
        {
            if (hasStation)
                aStation.ActivateActionStation(this);
            else
            {
                Action action = ActionQueue.Dequeue();
                if (action == Action.Jump)
                    jump();
                if (action == Action.Dash)
                    dash();
            }
            
        }
        
    }

    void jump()
    {
        //StartCoroutine(jumpTime());
        rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
    }

    void dash()
    {
        StartCoroutine(dashTime());
    }

    IEnumerator jumpTime()
    {
        action = true;
        rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        Debug.Log("Jump start");
        yield return new WaitForSeconds(0.3f);
        Debug.Log("jump end");
        action = false;
    }

    IEnumerator dashTime()
    {
        action = true;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(dashDistance * direction, 0), ForceMode2D.Impulse);
        rb.gravityScale = 0;
        Debug.Log("Geht");
        yield return new WaitForSeconds(0.3f);
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 1;
        action = false;
    }

    #region ActionStation

    public void ConfirmQueue(List<Action> actions)
    {
        ActionQueue.Clear();
        for (int i = 0; i < actions.Count; i++)
        {
            ActionQueue.Enqueue(actions[i]);
        }
        
        // TODO: Daki
        // This function is called when the new queue gets confirmed
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        aStation = other.GetComponent<ActionStation>();
        hasStation = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        aStation = null;
        hasStation = false;
    }
    
    #endregion
}
