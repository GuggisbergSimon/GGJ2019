using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float timeBetweenRoom = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
}
