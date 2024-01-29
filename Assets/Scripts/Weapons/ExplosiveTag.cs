using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTag : MonoBehaviour
{
    public float fuseTime = 5f;
    private float fuseTimer;
    public GameObject explosion;
    public int explosionSize = 0;
    private GameObject initializedExplosion;
    public bool isTriggerable = true;
    private void Start()
    {
        fuseTimer = fuseTime;
    }

    private void FixedUpdate()
    {
        fuseTimer -= 0.01f;
        if (fuseTimer < 0)
        {
            Destroy(gameObject, 0.01f);
            initializedExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            initializedExplosion.GetComponent<Explosion>().tier = explosionSize;
            initializedExplosion.AddComponent<WeaponInformation>();
            initializedExplosion.GetComponent<WeaponInformation>().CloneStats(gameObject.GetComponent<WeaponInformation>());
        }
    }
    public void SetScale(float scale)
    {
        explosionSize = (int)scale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.tag == "Enemy" || collision.gameObject.GetComponent<Explosion>() != null) && fuseTimer > 0 && isTriggerable)
        {
            Destroy(gameObject, 0.01f);
            initializedExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            initializedExplosion.GetComponent<Explosion>().tier = explosionSize;
            initializedExplosion.AddComponent<WeaponInformation>();
            initializedExplosion.GetComponent<WeaponInformation>().CloneStats(gameObject.GetComponent<WeaponInformation>());
        }
    }
}
