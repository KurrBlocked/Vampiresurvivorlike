using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSickle : MonoBehaviour
{
    public Sprite sickle;
    public Sprite ball;
    private SpriteRenderer spriteRenderer;
    public bool isSwingingSickle;
    private PlayerController player;
    public float ballDamageModifier = 10f;
    public float rotationSpeed = 4f;
    public float rotationTime = 4f;
    private float rotationTimer;

    public PolygonCollider2D sickleCollider;
    public BoxCollider2D ballCollider;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        isSwingingSickle = true;
        spriteRenderer.sprite = sickle;
        player = FindAnyObjectByType<PlayerController>();
        rotationTimer = rotationTime;
        transform.GetChild(0).localPosition = new Vector3(-2, 0, 0);
        sickleCollider.enabled = true;
        ballCollider.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotationTimer -= 0.01f;
        transform.position = player.transform.position;
        if (isSwingingSickle)
        {
            transform.Rotate(new Vector3(0, 0, -rotationSpeed));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed / 2f));
        }
        if (isSwingingSickle && rotationTimer < 0)
        {
            rotationTimer = rotationTime * 2;
            isSwingingSickle = false;
            spriteRenderer.sprite = ball;
            transform.GetChild(0).localPosition = new Vector3(-1, 0, 0);
            sickleCollider.enabled = false;
            ballCollider.enabled = true;
        }
        if (!isSwingingSickle && rotationTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
