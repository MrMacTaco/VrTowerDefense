using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //Simple function that takes scene name and loads it when button is pressed
    //Scene names hard coded into Canvas UI Buttons
    public void LoadScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
