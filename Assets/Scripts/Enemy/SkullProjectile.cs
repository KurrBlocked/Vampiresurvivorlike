using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullProjectile : MonoBehaviour
{
    public float duration = 0.2f;
    public float damage = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duration);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(damage);   //Make sure to use currentDamage instead of weaponData.damage in case any damage multipliers in the future
        }
    }
}
