using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    //[SerializeField] private string sceneToPlay;
    public void Play(string sceneToPlay)
    {
        SceneManager.LoadScene(sceneToPlay);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
