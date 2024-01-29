using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    public float duration = 1f;
    public float floatSpeed = 0.4f;
    public bool isTemporary = true;
    // Start is called before the first frame update
    void Start()
    {
        if (isTemporary)
        {
            Destroy(gameObject, duration);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTemporary)
        {
            transform.Translate(Vector2.up * floatSpeed, Space.World);
        }
    }
}
