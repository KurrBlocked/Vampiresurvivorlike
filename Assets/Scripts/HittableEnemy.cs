using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableEnemy : MonoBehaviour
{
    private DamageNumberSpawner damageNumber;
    public float iFrames = 1.0f;
    private float iFrameTimer;

    public int health = 99999;

    private List<GameObject> attacksAlreadyAccountedFor;
    private GameObject expOrb;
    public float expDropProbability = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        damageNumber = FindAnyObjectByType<DamageNumberSpawner>();
        attacksAlreadyAccountedFor = new List<GameObject>();
        expOrb = Resources.Load("ExpOrb") as GameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (iFrameTimer >= 0f)
        {
            iFrameTimer -= 0.01f;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            float rand = Random.Range(0.0f, 1.0f);
            if (rand <= expDropProbability)
            {
                Instantiate(expOrb, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Attack" && iFrameTimer <= 0f)
        {
            if (!attacksAlreadyAccountedFor.Contains(collision.gameObject))
            {
                int damage = collision.gameObject.GetComponent<WeaponStats>().damage;
                damageNumber.SpawnDamageNumbers(damage, gameObject.transform.position);
                health -= damage;
                iFrameTimer = iFrames;
                attacksAlreadyAccountedFor.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (attacksAlreadyAccountedFor.Contains(collision.gameObject))
        {
            attacksAlreadyAccountedFor.Remove(collision.gameObject);
        }
    }
}
