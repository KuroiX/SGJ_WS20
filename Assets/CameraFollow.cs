using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraFollow : MonoBehaviour
{

    public Transform player;

    public float rotationSpeed;

    public Vector3 relativePosition;

    // Update is called once per frame
    void Update()
    {
        var mousex = Input.GetAxis("Mouse X");
        var mousey = Input.GetAxis("Mouse Y");
        Vector3 rotation = transform.rotation.eulerAngles + new Vector3(-mousey, mousex, 0) * Time.deltaTime * rotationSpeed;
        
        transform.rotation = Quaternion.Euler(rotation);

        transform.position = player.position - transform.forward * relativePosition.x - transform.right * relativePosition.y - transform.up * relativePosition.z;

        transform.LookAt(player);
    }
}
