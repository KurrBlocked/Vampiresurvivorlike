using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public float speed = 10f;
    public float duration = 4f;
    private Vector2 targetDirection;

    private void Awake()
    {
        Destroy(gameObject, duration);
    }

    private void FixedUpdate()
    {
        transform.Translate(targetDirection * speed, Space.World);
    }

    public void SetTarget(Vector2 v)
    {
        targetDirection = v;
    }
    public void SetScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "TestHittable")
        {
            Destroy(gameObject, 0.01f);
        }
    }
}
