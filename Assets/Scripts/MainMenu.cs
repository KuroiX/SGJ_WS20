using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private GameObject settingsPanel;

    public void StartGame()
    {
        // TODO: load scene
        Debug.Log("Start game");
        SceneManager.LoadScene(1);
    }
    
    public void ExitGame()
    {
        // TODO: exit game
        Debug.Log("Exit game");
        Application.Quit();
    }

    public void SetMenu(bool value)
    {
        menuPanel.SetActive(value);
    }

    public void SetControlPanel(bool value)
    {
        //string path = "Assets/test.txt";
        
        //StreamReader reader = new StreamReader(path);
        
        //Debug.Log(reader.ReadToEnd());
    
        controlPanel.SetActive(value);
    }

    public void SetSettingsPanel(bool value)
    {
        settingsPanel.SetActive(value);
    }
}
