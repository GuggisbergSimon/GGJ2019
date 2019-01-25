using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class playertest : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x+(Input.GetAxis("Horizontal")*speed), transform.position.y + (Input.GetAxis("Vertical")*speed), 0);
    }
}
