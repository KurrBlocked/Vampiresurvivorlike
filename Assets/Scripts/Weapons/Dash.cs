using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private PlayerController player;
    public float dashDistance = 1f;
    public float dashSpeed = 0.5f;
    public float dashDistanceRemaining;
    private Vector2 dashDirection;
    public bool canDash;
    public bool dashWasPressed;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        dashDirection = Vector2.zero;
        dashDistanceRemaining = 0;
        canDash = false;
        dashWasPressed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DashIfPrompted();
    }

    public void DashIfPrompted()
    {
        if (Input.GetKey(KeyCode.Space) && canDash)
        {
            if (Input.GetKey(KeyCode.A))
            {
                dashDistanceRemaining = dashDistance;
                dashDirection = Vector2.left;
                canDash = false;
                dashWasPressed = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dashDistanceRemaining = dashDistance;
                dashDirection = Vector2.right;
                canDash = false;
                dashWasPressed = true;
            }
            if (Input.GetKey(KeyCode.W))
            {
                dashDistanceRemaining = dashDistance;
                dashDirection += Vector2.up;
                canDash = false;
                dashDirection.Normalize();
                dashWasPressed = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dashDistanceRemaining = dashDistance;
                dashDirection += Vector2.down;
                canDash = false;
                dashDirection.Normalize();
                dashWasPressed = true;
            }
        }
        if (dashDistanceRemaining > 0)
        {
            player.transform.Translate(dashSpeed * dashDirection);
            dashDistanceRemaining -= dashSpeed;
        }
        else
        {
            dashDistanceRemaining = 0;
            dashDirection = Vector2.zero;
            dashWasPressed = false;
        }
    }
}
