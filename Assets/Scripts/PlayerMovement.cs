using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    public Rigidbody2D rb2d;
    BoxCollider2D[] coll2D;

    private void FixedUpdate()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        move = move * moveSpeed * Time.deltaTime;
        rb2d.MovePosition(move + transform.position);
        coll2D = GetComponentsInChildren<BoxCollider2D>();
    }

    public void toggleBulletCollider()
    {
        coll2D[1].enabled = !coll2D[1].enabled;
    }
}
