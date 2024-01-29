using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 1f;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * playerSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * playerSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * playerSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * playerSpeed;
        }
    }
}
