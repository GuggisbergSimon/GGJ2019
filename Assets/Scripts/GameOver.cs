using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Yes()
    {
        GameManager.Instance.LoadScene("LucaScene");
    }

    public void No()
    {
        GameManager.Instance.QuitGame();

    }
}
