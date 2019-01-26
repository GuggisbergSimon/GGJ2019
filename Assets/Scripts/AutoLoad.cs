using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoad : MonoBehaviour
{
	[SerializeField] private float minTime = 5.0f;
	[SerializeField] private float maxTime = 10.0f;
	[SerializeField] private string sceneToLoad = null;

	private void Start()
	{
		Invoke("LoadScene", Random.Range(minTime, maxTime));
	}

	void LoadScene()
	{
		SceneManager.LoadScene(sceneToLoad);
	}
}