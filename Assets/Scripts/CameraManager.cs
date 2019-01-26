using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float timeBetweenRoom = 1;
    private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    public void MoveRoomFunction(Vector3 newPosition)
    {
        StartCoroutine(GameManager.Instance.Camera.MoveRoom(newPosition));
    }

    public IEnumerator MoveRoom(Vector3 newPosition)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        newPosition = new Vector3(newPosition.x, newPosition.y, -10);
        while (time < timeBetweenRoom)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, newPosition, time/timeBetweenRoom);
            yield return null;
        }

        yield return 0;
    }
    
    public void Noise(float amplitudeGain, float frequencyGain)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
    }
}
