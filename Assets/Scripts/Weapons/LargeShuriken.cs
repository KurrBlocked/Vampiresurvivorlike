using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeShuriken : MonoBehaviour
{
    public float speed = 10f;
    public float duration = 4f;
    private Vector2 targetDirection;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3 (0,0,10));
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
}
