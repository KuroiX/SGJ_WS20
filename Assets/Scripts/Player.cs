using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Queue<Action> ActionQueue;
    private List<Action> ActionList = new List<Action>();
    public float speed;
    public float jumpHeight;
    public float dashDistance;
    public float fallMultiplier;
    private Rigidbody2D rb;
    private bool action = false;
    private int direction = 1;
    private SpriteRenderer sr;
    private ActionStation aStation;
    private Vector3 startPosition;
    private bool hasStation = false;
    private bool isInStation = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        ActionQueue = new Queue<Action>();
        startPosition = transform.position;
        /*
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
        */
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

                amtToMove = Input.GetAxis("Horizontal") * speed;

                //if (!IsGrounded()) amtToMove *= 0.8f;
                //rb.AddForce(Vector3.right * amtToMove);
                
                //rb.MovePosition(rb.transform.position + (Vector3.right * amtToMove));
                //transform.Translate(Vector3.right * amtToMove);
            }

            if (Input.GetButtonDown("Jump") && action == false)
            {
                if (hasStation)
                {
                    rb.velocity = Vector2.zero;
                    amtToMove = 0;
                    aStation.ActivateActionStation(this);
                    isInStation = true;
                }
                else
                {
                    //jump();
                    
                    
                    if (ActionQueue.Count != 0)
                    {
                        Action action = ActionQueue.Dequeue();
                        UIManager.Instance.GoNext(action);

                        if (action == Action.Jump)
                            jumpActivated = true;
                        if (action == Action.Dash)
                            dashActivated = true;
                    }
                    
                }
            } else if (Input.GetButtonDown("Jump") && isDashing && ActionQueue.Peek() == Action.Jump)
            {
                StopDash();
                Action action = ActionQueue.Dequeue();
                UIManager.Instance.GoNext(action);
                jumpActivated = true;
            }
        }

        //jumpTimer += Time.deltaTime;
        if (isDashing && dashTimer < 0.3f)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= 0.3f)
            {
                StopDash();
            }
        }
        
    }

    private bool jumpActivated;
    private bool dashActivated;
    private float amtToMove;
    public float startValue;
    public float newFallSpeed;
    public float maxFallSpeed;
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

        // Async jump
        if (rb.velocity.y < 0f)
        {
            Vector2 newVel = rb.velocity + Physics2D.gravity * newFallSpeed * Time.fixedDeltaTime;
            if (newVel.y < maxFallSpeed)
            {
                newVel = new Vector2(newVel.x, maxFallSpeed);
                Debug.Log("Max speed reached");
            }

            Debug.Log("Set new Velocity");
            rb.velocity = newVel;
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
        StartDash();
        //StartCoroutine(dashTime());
    }

    public void OnDeath()
    {
        // TODO:
        if (aStation == null)
        {
            transform.position = startPosition;
        }
        else
        {
            transform.position = aStation.transform.position;
        }
       
        rb.velocity = new Vector2(0, 0);
        ResetQueue(ActionList);
        UIManager.Instance.SetUI(ActionList);
        StartCoroutine(WaitAfterDeath());
    }

    IEnumerator WaitAfterDeath()
    {
        action = true;
        yield return new WaitForSeconds(0.3f);
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

    private bool isDashing;
    private float dashTimer;
    IEnumerator dashTime()
    {
        StartDash();
        //Debug.Log("Geht");
        yield return new WaitForSeconds(0.3f);
        if (isDashing)
        {
            StopDash();
        }
        
    }

    void StartDash()
    {
        dashTimer = 0f;
        isDashing = true;
        action = true;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(dashDistance * direction, 0), ForceMode2D.Impulse);
        rb.gravityScale = 0;
    }

    void StopDash()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1;
        action = false;
        isDashing = false;
        Debug.Log(dashTimer);
    }

    #region ActionStation

    public void ConfirmQueue(List<Action> actions)
    {
        ActionQueue.Clear();
        ActionList.Clear();
        for (int i = 0; i < actions.Count; i++)
        {
            ActionList.Add(actions[i]);
            ActionQueue.Enqueue(actions[i]);
        }
        isInStation = false;
    }
    
    public void ResetQueue(List<Action> actions)
    {
        ActionQueue.Clear();
        for (int i = 0; i < actions.Count; i++)
        {
            ActionQueue.Enqueue(actions[i]);
        }
    }

    public void ArriveAtStation(ActionStation station)
    {
        aStation = station;
        hasStation = true;
    }

    public void LeaveStation()
    {
        hasStation = false;
    }
    
    
    #endregion
}
