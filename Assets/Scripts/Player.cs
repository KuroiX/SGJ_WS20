using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Queue<Action> ActionQueue;
    public float speed;
    public float jumpHeight;
    public float dashDistance;
    public float fallMultiplier;
    private Rigidbody2D rb;
    private bool action = false;
    private int direction = 1;
    private SpriteRenderer sr;
    private ActionStation aStation;
    private bool hasStation = false;
    private bool isInStation = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        ActionQueue = new Queue<Action>();
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
        ActionQueue.Enqueue(Action.Jump);
        ActionQueue.Enqueue(Action.Dash);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInStation)
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

                amtToMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

                //if (!IsGrounded()) amtToMove *= 0.8f;
                //rb.AddForce(Vector3.right * amtToMove);
                
                //rb.MovePosition(rb.transform.position + (Vector3.right * amtToMove));
                //transform.Translate(Vector3.right * amtToMove);
            }

            if (Input.GetButtonDown("Jump") && action == false)
            {
                if (hasStation)
                {
                    aStation.ActivateActionStation(this);
                    isInStation = true;
                }
                else
                {
                    //jump();
                    
                    
                    if (ActionQueue.Count != 0)
                    {
                        Action action = ActionQueue.Dequeue();
                        UIManager.Instance.GoNext();

                        if (action == Action.Jump)
                            jumpActivated = true;
                        if (action == Action.Dash)
                            dashActivated = true;
                    }
                    
                }
            }
        }

        //jumpTimer += Time.deltaTime;
    }

    private bool jumpActivated;
    private bool dashActivated;
    private float amtToMove;
    void FixedUpdate()
    {
        if (jumpActivated)
        {
            jump();
            jumpActivated = false;
        }
        if (dashActivated)
        {
            dash();
            dashActivated = false;
        }

        if (!action)
        {
            rb.velocity = new Vector2(amtToMove, rb.velocity.y);
        }
        
    }

    bool IsGrounded()
    {
        if (rb.velocity.y == 0f)
        {
            Debug.Log("Grounded");
            return true;
        }
        return false;
    }

    //private float jumpTimer = 0.5f;
    void jump()
    {
        Debug.Log("Jumped");
        //jumpTimer = 0f;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        //StartCoroutine(jumpTime());
        rb.AddForce(new Vector2(0, jumpHeight));
    }

    void dash()
    {
        Debug.Log("Dashed");
        StartCoroutine(dashTime());
    }

    public void OnDeath()
    {
        transform.position = aStation.transform.position;
        rb.velocity = new Vector2(0, 0);
        StartCoroutine(WaitAfterDeath());
    }

    IEnumerator WaitAfterDeath()
    {
        action = true;
        yield return new WaitForSeconds(0.5f);
        action = false;
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

        isInStation = false;
        // This function is called when the new queue gets confirmed
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        aStation = other.GetComponent<ActionStation>();
        hasStation = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //aStation = null;
        hasStation = false;
    }
    
    #endregion
}
