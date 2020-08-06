using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterBody : MonoBehaviour
{
    public float fireTime = 4.0f;
    public bool firing = false;
    public float bulletSpeed = 2f;
    public GameObject bullet;

    GameObject player;
    Floater floater;
    private IEnumerator coroutine;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        floater = GetComponentInParent<Floater>();

        coroutine = FireBullet(fireTime);
        StartCoroutine(coroutine);
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void Fire()
    {
        Vector3 playerPos = player.transform.position;
        Vector2 fireDirection = (Vector2)(playerPos - transform.position);
        fireDirection.Normalize();
        var newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = fireDirection * bulletSpeed;
    }

    private IEnumerator FireBullet(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            animator.SetTrigger("Fire");
        }
    }

    public void ToggleFiring()
    {
        firing = !firing;
    }

    public void ToggleInvincibilty()
    {
        floater.invincible = !floater.invincible;
    }

    public void FloaterDie()
    {
        Destroy(floater.gameObject);
    }
}
