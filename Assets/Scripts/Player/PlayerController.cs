using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer sprite;

    public float timeModifier = 1f;

    public CharacterScriptableObject characterData;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

   
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * characterData.MoveSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * characterData.MoveSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * characterData.MoveSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * characterData.MoveSpeed;
        }
    }
}
