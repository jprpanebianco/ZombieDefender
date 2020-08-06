using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float radius;
    float power;
    int damage;

    private void Update()
    {
        Vector2 explosionPos = (Vector2)transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb2d = hit.GetComponent<Rigidbody2D>();
            IDamageable damageable = rb2d.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage);
                float distanceFromCenter = Vector2.Distance(explosionPos, rb2d.transform.position);
                float forceMultiplier = radius - distanceFromCenter;
                Vector2 direction = (Vector2)rb2d.transform.position - explosionPos;
                direction.Normalize();
                rb2d.AddForce(direction * forceMultiplier * power, ForceMode2D.Impulse);
            }
        }
        Destroy(this.gameObject);
    }
    public void SetExplosion(float radius, float power, int damage)
    {
        SetRadius(radius);
        SetPower(power);
        SetDamage(damage);
    }

    public void SetRadius( float radius)
    {
        this.radius = radius;
    }

    public void SetPower(float power)
    {
        this.power = power;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
