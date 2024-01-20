using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public float speed = 10f;
    public float duration = 4f;
    public float damage = 5f;

    private Vector2 targetDirection;

    public bool isActive;

    // KeyValuePair<level requirement, value>
    public int cooldownUpgradeLevel;
    public List<KeyValuePair<int, int>> cooldownUpgrades = new List<KeyValuePair<int, int>>();
    public int damageUpgradeLevel;
    public List<KeyValuePair<int, int>> damageUpgrades = new List<KeyValuePair<int, int>>();
    public int numProjectileUpgradeLevel;
    public List<KeyValuePair<int, string>> numProjectileUpgrades = new List<KeyValuePair<int, string>>();

    private void Start()
    {
        Destroy(gameObject, duration);
        isActive = true;

        cooldownUpgradeLevel = 0;
        cooldownUpgrades.Add(new KeyValuePair<int, int> (1,0));
    }

    private void FixedUpdate()
    {
        transform.Translate(targetDirection * speed, Space.World);
    }

    public void SetTarget(Vector2 v)
    {
        targetDirection = v;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "TestHittable")
        {
            Destroy(gameObject, 0.01f);
            isActive = false;
        }
    }
}
