using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	[SerializeField] private float timeBetweenRoom = 1;
	private CinemachineVirtualCamera vcam;
	private CinemachineBasicMultiChannelPerlin noise;
	private int numberShaking = 0;

	private float constantAmplitude = 10;
	private float constantFrequency = 10;
	private bool temporaryNoise = false;

	void Start()
	{
		vcam = GetComponent<CinemachineVirtualCamera>();
		noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		ConstantNoise();
	}

	void Update()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}

	public void Noise(float amplitudeGain, float frequencyGain)
	{
		noise.m_AmplitudeGain = amplitudeGain + constantAmplitude;
		noise.m_FrequencyGain = frequencyGain + constantFrequency;
		temporaryNoise = true;
	}

	public void ConstantNoise(float amplitudeGain, float frequencyGain)
	{
		constantAmplitude = amplitudeGain;
		constantFrequency = frequencyGain;
		if (!temporaryNoise)
		{
			ConstantNoise();
		}
	}

	public void ConstantNoise()
	{
		temporaryNoise = false;
		noise.m_AmplitudeGain = constantAmplitude;
		noise.m_FrequencyGain = constantFrequency;
	}

	public void StopShakingCam(float time)
	{
		numberShaking++;
		StartCoroutine(StopShake(time));
	}

	private IEnumerator StopShake(float time)
	{
		yield return new WaitForSeconds(time);
		numberShaking--;
		if (numberShaking <= 0)
		{
			GameManager.Instance.Camera.ConstantNoise();
		}
	}
}