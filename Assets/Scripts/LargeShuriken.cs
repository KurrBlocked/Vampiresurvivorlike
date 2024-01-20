using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeShuriken : MonoBehaviour
{
    public float speed = 10f;
    public float duration = 4f;
    public float damage = 5f;

    private Vector2 targetDirection;
    public float cooldown = 5f;

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
}
