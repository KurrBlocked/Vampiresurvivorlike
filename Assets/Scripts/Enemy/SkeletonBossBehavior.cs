using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBossBehavior : MonoBehaviour
{
    private PlayerController player;
    public EnemyScriptableObject enemyData;
    public float followDistance = 7f;
    public float baseSpeed = 0.125f;
    public float catchupSpeed = 0.5f;
    public float catchUpMinimumDistance = 15f;
    public float switchRandomLocationTime = 1f;
    private float randomLocationTimer;
    private Vector3 randomLocation;



    public GameObject projectileReference;
    public int maxProjectiles = 2;
    public float timeBetweenFiring = 20f;
    public float fireTimer;
    private List<GameObject> projectiles;


    //Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        randomLocationTimer = 0;
        randomLocation = new Vector3(Random.Range(player.transform.position.x - 100f, player.transform.position.x + 100f), Random.Range(player.transform.position.y - 100f, player.transform.position.y + 100f));
        fireTimer = 0;
        projectiles = new List<GameObject>();
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        //distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distToPlayer <= followDistance)
        {
            MoveAroundPlayer(baseSpeed);
        }
        else if (distToPlayer < catchUpMinimumDistance)
        {
            ChasePlayer(baseSpeed);
        }
        else
        {
            ChasePlayer(catchupSpeed);
        }

        fireTimer -= 0.01f;
        if (fireTimer <= 0 && projectiles.Count < maxProjectiles)
        {
            Fire();
            fireTimer = timeBetweenFiring;
        }
    }

    void ChasePlayer(float speed)
    {
        FacePlayer();
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
    }

    void FacePlayer()
    {
        //change direction skeleton is looking
        if (transform.position.x < player.transform.position.x)
        {
            //enemy is to the left of the player, so look right
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x >= player.transform.position.x)
        {
            //enemy is to the right of the player, so look left
            transform.localScale = new Vector2(-1, 1);
        }

    }
    void MoveAroundPlayer(float speed)
    {
        FacePlayer();
        
        transform.position = Vector2.MoveTowards(transform.position, randomLocation, speed);
        randomLocationTimer -= 0.01f;
        if (randomLocationTimer <= 0)
        {
            randomLocationTimer = switchRandomLocationTime;
            randomLocation = new Vector3(Random.Range(player.transform.position.x - 14f, player.transform.position.x + 14f), Random.Range(player.transform.position.y - 9f, player.transform.position.y + 9f));
        }
    }

    void Fire()
    {
        GameObject projectile = Instantiate(projectileReference, transform.position, Quaternion.identity);
        projectiles.Add(projectile);
    }
    private void OnDestroy()
    {
        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }
    }
}