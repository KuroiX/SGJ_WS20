using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float speed;
    public float time;
    private int direction = -1;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(move());
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == true)
        {
            float amtToMove = speed * Time.deltaTime * direction;
            transform.Translate(Vector3.right*amtToMove);
        }

        if (isMoving == false)
        {
            StartCoroutine(move());
        }
    }

    IEnumerator move()
    {
        isMoving = true;
        float formerDirection = direction;
        direction = 0;
        yield return new WaitForSeconds(1f);
        if (formerDirection == 1)
        {
            direction = -1;
        }
        if (formerDirection == -1)
        {
            direction = 1;
        }
        yield return  new WaitForSeconds(time);
        isMoving = false;
    }
}
