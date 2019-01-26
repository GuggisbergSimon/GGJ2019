using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public void Yes()
	{
		LoadScene("LucaScene");
	}

	public void LoadScene(string scene)
	{
		GameManager.Instance.LoadScene(scene);
	}

	public void QuitGame()
	{
		GameManager.Instance.QuitGame();
	}

	public void No()
	{
		QuitGame();
	}
}