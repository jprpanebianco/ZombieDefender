using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletHitbox : MonoBehaviour
{
    Animator animator;
    Barrier barrier;

    public bool isBulletInvincible = false;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        barrier = FindObjectOfType<Barrier>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamaging dmg = collision.gameObject.GetComponent<IDamaging>();
        if(dmg != null)
        {
            animator.SetTrigger("Hit");
            for(int i = 0; i <= dmg.GetDamageDealt(); i++)
            {
                barrier.TakeDamage();
            }
        }
    }

}
