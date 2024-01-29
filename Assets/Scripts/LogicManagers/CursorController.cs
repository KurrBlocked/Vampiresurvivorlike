using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public GameObject cursor;
    public GameObject pointer;

    private PlayerController player;

    public Quaternion pointerRotation;
    public Vector3 throwingDirection;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        pointer.transform.parent = player.gameObject.transform;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        throwingDirection = player.transform.position - cursor.transform.position;
        float angle = Mathf.Atan2(throwingDirection.y, throwingDirection.x) * Mathf.Rad2Deg;
        angle += 90;
        pointerRotation = Quaternion.Euler(0f, 0f, angle);
        pointer.transform.rotation = pointerRotation;

        if (angle >= 0 && angle <= 180)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
        }

    }
    private void FixedUpdate()
    {
        Vector3 cursorLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = new Vector3(cursorLocation.x, cursorLocation.y, 0f);
    }
}
