using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(end());
        //GetComponent<AudioSource>().Play();
    }

    IEnumerator end()
    {
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}
