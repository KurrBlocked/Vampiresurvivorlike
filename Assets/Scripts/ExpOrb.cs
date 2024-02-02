using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    private PlayerLevelManager levelManager;
    private PlayerController player;
    public int expAmount = 10;
    public float distanceTillMagnet = 5f;
    public float travelSpeed = 2f;
    private void Start()
    {
        levelManager = FindAnyObjectByType<PlayerLevelManager>();
        player = FindAnyObjectByType<PlayerController>();
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < distanceTillMagnet)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, travelSpeed);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelManager.experience += expAmount;
            Destroy(gameObject);
        }
    }
}
