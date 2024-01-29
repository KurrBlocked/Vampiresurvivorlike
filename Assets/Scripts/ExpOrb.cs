using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    private PlayerLevelManager levelManager;
    public int expAmount = 10;
    private void Start()
    {
        levelManager = FindAnyObjectByType<PlayerLevelManager>();
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
