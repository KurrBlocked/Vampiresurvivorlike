using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonProjectile : MonoBehaviour
{
    private PolygonCollider2D polygonCollider;
    public float maxSize = 2f;
    public float duration = 2f;
    public float sizeIncreaseRate = 0.1f;
    public Vector2[] pts;
    private Animator animator;
    public float damage = 5f;
    // Start is called before the first frame update
    void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        Destroy(gameObject, duration);
        pts = polygonCollider.points;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetBool("IsExploding", true);
        if (polygonCollider.points[1].y > -maxSize)
        {
            pts[1] -= new Vector2(0, sizeIncreaseRate);
            pts[3] += new Vector2(sizeIncreaseRate, 0);
            pts[5] += new Vector2(0, sizeIncreaseRate);
            pts[7] -= new Vector2(sizeIncreaseRate, 0);
            polygonCollider.SetPath(0, pts);
        }
        
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
