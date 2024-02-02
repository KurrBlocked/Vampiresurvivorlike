using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleBossProjectileBehavior : MonoBehaviour
{
    private PlayerController player;
    public float startingProjectileSpeed = 0.125f;
    public float rotatingSpeed = 0.125f;
    public float distanceFromPlayer = 3f;


    public GameObject projectile1;
    public GameObject projectile2;

    public float delayBeforeFiring = 0.4f;
    private float delayTimer;
    public float rotationTime = 1.0f;
    private float rotationTimer;

    public float fireDuration = 0.4f;
    public float fireTime = 0.2f;
    private float fireTimer;
    private int currentAction;

    public GameObject projectileReference;
    private GameObject projectile;
    private bool hasFired;

    //Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        delayTimer = 0;
        rotationTimer = 0;
        currentAction = 0;
        fireTimer = 0;
        hasFired = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentAction)
        {
            case 0:
                LockOn();
                break;
            case 1:
                SetRotatingStartPositions();
                break;
            case 2:
                Rotate();
                break;
            case 3:
                DelayBeforeFire();
                break;
            case 4:
                Fire();
                break;
            case 5:
                ResetPosition();
                break;
        }
    }

    private void LockOn()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, startingProjectileSpeed);
        if (transform.position == player.transform.position)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.zero;
            currentAction++;
        }

    }
    void SetRotatingStartPositions()
    { 
        projectile1.transform.position = Vector2.MoveTowards(projectile1.transform.position, player.transform.position + Vector3.up * distanceFromPlayer, startingProjectileSpeed);
        projectile2.transform.position = Vector2.MoveTowards(projectile2.transform.position, player.transform.position + Vector3.down * distanceFromPlayer, startingProjectileSpeed);

        if (projectile1.transform.position == player.transform.position + Vector3.up * distanceFromPlayer)
        {
            currentAction++;
            rotationTimer = rotationTime + Random.Range(0f, 1f);
        }
    }
    void Rotate()
    {
        if (rotationTimer > 0)
        {
            rotationTimer -= 0.01f;
            transform.Rotate(new Vector3(0, 0, rotatingSpeed));
            projectile1.transform.rotation = Quaternion.identity;
            projectile2.transform.rotation = Quaternion.identity;
        }
        else
        {
            currentAction++;
            delayTimer = delayBeforeFiring;
        }
    }
    void DelayBeforeFire()
    {
        delayTimer -= 0.01f;
        if (delayTimer <= 0)
        {
            currentAction++;
            fireTimer = fireDuration; 
            transform.SetParent(null);
            hasFired = false;
        }
    }

    void Fire()
    {
        fireTimer -= 0.01f;
        if (fireTimer < fireTime)
        {
            if (!hasFired)
            {
                Vector3 direction = transform.position - projectile1.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle + 90f);
                projectile = Instantiate(projectileReference, transform.position, targetRotation);
                hasFired = true;
            }
        }
        if (fireTimer <= 0)
        {
            currentAction++;
        }
    }
    void ResetPosition()
    {
        projectile1.transform.position = Vector2.MoveTowards(projectile1.transform.position, transform.position, startingProjectileSpeed);
        projectile2.transform.position = Vector2.MoveTowards(projectile2.transform.position, transform.position, startingProjectileSpeed);

        if (projectile1.transform.position == transform.position)
        {
            currentAction = 0;
        }
    }
}
