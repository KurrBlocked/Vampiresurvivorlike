using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private PlayerController _player;
    public EnemyScriptableObject enemyData;

    Rigidbody2D rb2d;
    //Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _player = FindAnyObjectByType<PlayerController>();
        
    }

    //Update is called once per frame
    void Update()
    {
        //distance to player
        float distToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        
        if(distToPlayer < enemyData.AttackRange)
        {
            //code to attack player
            AttackPlayer();
        }
        else
        {
            //code to chase player
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        FacePlayer();
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, enemyData.MoveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        FacePlayer();
        rb2d.velocity = Vector2.zero;
    }

    void FacePlayer()
    {
        //change direction skeleton is looking
        if (transform.position.x < _player.transform.position.x)
        {
            //enemy is to the left of the player, so look right
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x >= _player.transform.position.x)
        {
            //enemy is to the right of the player, so look left
            transform.localScale = new Vector2(-1, 1);
        }

    }
}
