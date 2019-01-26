using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject nextRoom;
    [SerializeField] private GameObject actualRoom;

    private BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.UIManager.FuryGauge.Fury < 50)
        {
            boxCollider2D.isTrigger = true;
        }
        else
        {
            boxCollider2D.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || GameManager.Instance.UIManager.FuryGauge.Fury < 50)
        {
            GameManager.Instance.Camera.MoveRoomFunction(nextRoom.transform.position);
            GameManager.Instance.Player.transform.position += Vector3.up*2*(Mathf.Sign(nextRoom.transform.position.y-actualRoom.transform.position.y));
            nextRoom.SetActive(true);
            actualRoom.SetActive(false);
            GameManager.Instance.UIManager.FuryGauge.Fury = 100;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Rage");
    }
}
