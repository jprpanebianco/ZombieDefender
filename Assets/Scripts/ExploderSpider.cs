using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderSpider : Spider
{
    
    public float explosionRadius = 20.0F;
    public float explosionPower = 100.0F;
    public int explosionDmg = 1000;
    public GameObject explosion;

    void SpiderExplode()
    {
        rb2d.isKinematic = true;
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        exp.GetComponent<Explosion>().SetExplosion(explosionRadius, explosionPower, explosionDmg);
        
    }
}
