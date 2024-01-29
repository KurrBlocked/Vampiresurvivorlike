using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int tier = 0;

    public BoxCollider2D boxCollider;
    public float explosionSize;
    public float duration = 4f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, duration);
        switch (tier)
        {
            case 0:
                explosionSize = 2.5f;
                animator.SetInteger("ExplosionTier", 0);
                Destroy(gameObject, duration);
                break;
            case 1:
                explosionSize = 3.5f;
                animator.SetInteger("ExplosionTier", 1);
                Destroy(gameObject, duration + 0.1f);
                break;
            case 2:
                explosionSize = 4.5f;
                animator.SetInteger("ExplosionTier", 2);
                Destroy(gameObject, duration + 0.2f);
                break;
            default:
                explosionSize = 2.5f;
                Debug.Log("Unknown explosion tier");
                Destroy(gameObject, duration + 0.2f);
                break;
        }
    }
    private void FixedUpdate()
    {
        if (boxCollider.size.x < explosionSize)
        {
            boxCollider.size = boxCollider.size + new Vector2(0.25f, 0.25f);
        }
    }
}
