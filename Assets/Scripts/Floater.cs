using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour, IDamageable
{
    public int damageToKill = 3;
    public int damageTaken = 0;
    public int moveSpeed = 4;
    public bool invincible = false;
    public float distanceFromTarget = 4;
    Vector3 target = new Vector3(0, 0);
   
    
    Rigidbody2D rb2d;
    FloaterBody body;
    Transform bodyTrans;
    bool frozen = false;
    Animator animator;

    void Start()
    {
        body = GetComponentInChildren<FloaterBody>();
        bodyTrans = body.gameObject.transform;
        animator = body.gameObject.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.velocity = Vector2.zero;
        Vector2 directionToMove = (Vector2)(target - transform.position);
        //if (directionToMove.magnitude < distanceFromTarget)
        //{
         //   frozen = true;
        //}
        //else frozen = false;

        //if (!(frozen || body.firing))
        if(directionToMove.magnitude > distanceFromTarget && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            directionToMove = directionToMove.normalized * moveSpeed * Time.deltaTime;
            rb2d.MovePosition(directionToMove + (Vector2)(transform.position));
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        IDamaging dmg = other.gameObject.GetComponent<IDamaging>();
        if (dmg != null && !invincible)
        {
            Damage(dmg.GetDamageDealt());
        }

    }

    public void Damage(int damage)
    {
        damageTaken += damage;
        if (damageTaken < damageToKill)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            animator.SetTrigger("Die");
            //spriteRenderer.sortingLayerName = "Ground";
        }
    }
}