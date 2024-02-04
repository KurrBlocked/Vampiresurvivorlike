using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableEnemy : MonoBehaviour
{
    private DamageNumberSpawner damageNumber;
    public float iFrames = 0f;
    private float iFrameTimer;

    private List<GameObject> attacksAlreadyAccountedFor;
    private GameObject expOrb;
    public float expDropProbability = 0.8f;
    public int expAmount = 1;

    private EnemyStats enemyStats;
    // Start is called before the first frame update
    void Start()
    {
        attacksAlreadyAccountedFor = new List<GameObject>();
        expOrb = Resources.Load("ExpOrb") as GameObject;
        damageNumber = gameObject.AddComponent<DamageNumberSpawner>();
        enemyStats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (iFrameTimer >= 0f)
        {
            iFrameTimer -= 0.01f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Attack" && iFrameTimer <= 0f)
        {
            if (!attacksAlreadyAccountedFor.Contains(collision.gameObject))
            {
                int damage;
                if (collision.gameObject.name == "ChainAttack")
                {
                    damage = (int)collision.gameObject.GetComponentInParent<WeaponInformation>().damage.currentValue;
                    if (!collision.gameObject.GetComponentInParent<ChainSickle>().isSwingingSickle)
                    {
                        damage *= (int) collision.gameObject.GetComponentInParent<ChainSickle>().ballDamageModifier;
                    }
                }
                else
                {
                    damage = (int)collision.gameObject.GetComponent<WeaponInformation>().damage.currentValue;
                }
                damageNumber.SpawnDamageNumbers(damage, gameObject.transform.position);
                enemyStats.TakeDamage(damage);
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

    public void DropEXP()
    {
        float rand = Random.Range(0.0f, 1.0f);
        if (rand <= expDropProbability)
        {
            GameObject exp = Instantiate(expOrb, gameObject.transform.position, Quaternion.identity);
            exp.GetComponent<ExpOrb>().SetExp(expAmount);
        }
    }
}
