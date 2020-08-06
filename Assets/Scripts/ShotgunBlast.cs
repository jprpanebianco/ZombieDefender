using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBlast : MonoBehaviour, IDamaging
{
    public int damageDealt = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(damageDealt);
        }
    }

    public int GetDamageDealt()
    {
        return damageDealt;
    }

    public void DestoryThis()
    {
        Destroy(this.gameObject);
    }
}
