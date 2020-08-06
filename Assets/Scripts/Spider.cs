using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour, IDamageable, IBarrierDamaging
{
    public int damageToKill = 3;
    public int damageTaken = 0;
    public int moveSpeed = 4;
    public bool invincible = false;

    Animator animator;
    GameObject player;
    protected Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            Vector2 directionToMove = (Vector2)(player.transform.position - transform.position);
            directionToMove = directionToMove.normalized * moveSpeed * Time.deltaTime;
            rb2d.MovePosition(directionToMove + (Vector2)(transform.position));
        }
    }

    void SpiderDie()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        IDamaging dmg = other.gameObject.GetComponent<IDamaging>();
        if (dmg != null && !invincible)
        {
            Damage(dmg.GetDamageDealt());
        }

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Barrier"))
        {
            DamageBarrier(other);
        }
    }

    public void Damage(int damage)
    {
        damageTaken += damage;
        if (damageTaken < damageToKill)
        {
            //if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            //{
            animator.SetTrigger("Hit");
            //}
        }
        else
        {
            animator.SetTrigger("Die");
            spriteRenderer.sortingLayerName = "Ground";
        }
    }

    public void ToggleInvincibility()
    {
        invincible = !invincible;
    }

    public void DeactivatePhysics()
    {
        rb2d.isKinematic = true;
        rb2d.velocity = Vector2.zero;
    }

    public void MakeLaserPassThrough()
    {
        this.gameObject.layer = 2;
    }

    public void DamageBarrier(Collision2D other)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            other.gameObject.GetComponent<Animator>().SetTrigger("IsDamaged");
    }
}
