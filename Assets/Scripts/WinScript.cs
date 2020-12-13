using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(end());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator end()
    {
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}
