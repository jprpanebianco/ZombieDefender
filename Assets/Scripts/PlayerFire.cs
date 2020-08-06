using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject particle;
    public float particleSpeed = 6f;
    public GameObject pointOfGun;
    public GameObject shotgunBlast;

    Animator animator;
    //Animator shotgunAnimator;
    readonly int numberOfWeaponTypes = System.Enum.GetValues(typeof(WeaponType)).Length;
    WeaponType weaponType = WeaponType.machineGun;
    GameObject laser;


    void Start()
    {
        animator = GetComponent<Animator>();
        laser = GameObject.Find("Laser");
        laser.SetActive(false);
        //shotgunAnimator = GameObject.Find("ShotgunBlast").GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        var aimDirection = (Vector2) (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        aimDirection.Normalize();
        animator.SetFloat("aimX", aimDirection.x);
        animator.SetFloat("aimY", aimDirection.y);

        if (!Input.GetMouseButton(0) && Input.GetMouseButtonDown(1))
        {
            CycleWeaponsForward();
        }

        switch (weaponType)
        {
            case WeaponType.machineGun:
                {
                    if (Input.GetMouseButton(0))
                    {
                        Vector3 cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector2 direction = (Vector2)(cursorInWorldPos - transform.position);
                        direction.Normalize();
                        var bullet = Instantiate(particle, pointOfGun.transform.position + (Vector3)(direction * 0.1f), Quaternion.identity);
                        bullet.GetComponent<Rigidbody2D>().velocity = direction * particleSpeed;
                    }
                    break;
                }
            case WeaponType.shotGun:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        var blast = Instantiate(shotgunBlast, pointOfGun.transform.position, Quaternion.identity);
                        float deg = Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x);
                        Quaternion quat = Quaternion.Euler(shotgunBlast.transform.rotation.x, shotgunBlast.transform.rotation.y, deg);
                        blast.transform.rotation = quat;
                    }
                    break;
                }
            case WeaponType.laserGun:
                {
                    if (Input.GetMouseButton(0))
                    {
                        laser.SetActive(true);
                    }
                    else
                    {
                        laser.SetActive(false);
                    }
                    break;
                }
                    default:
                {
                    Debug.Log("Something broke with the WeaponType enum. Fix it.");
                    break;
                }
        }
    }

    void CycleWeaponsForward(){
        weaponType += 1;
        if ((int)weaponType == numberOfWeaponTypes)
        {
            weaponType = 0;
        }
    
    }

    void CycleWeaponsBackward()
    {
        weaponType -= 1;
        if ((int)weaponType == -1)
        {
            weaponType = (WeaponType)numberOfWeaponTypes - 1;
        }
    }
}
