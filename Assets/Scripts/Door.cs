using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject nextRoom;
    [SerializeField] private GameObject actualRoom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Camera.MoveRoomFunction(nextRoom.transform.position);
            GameManager.Instance.Player.transform.position += Vector3.up*2*(Mathf.Sign(nextRoom.transform.position.y-actualRoom.transform.position.y));
            nextRoom.SetActive(true);
            actualRoom.SetActive(false);
        }
    }
}
