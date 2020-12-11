using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(camera.transform.forward * speed * Time.deltaTime * Input.GetAxis("Vertical"));
        rb.AddForce(camera.transform.right * speed * Time.deltaTime * Input.GetAxis("Horizontal"));
    }
}
