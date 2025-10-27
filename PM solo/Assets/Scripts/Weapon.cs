using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    PlayerController player;

    public Explosion Explosion;

    public GameObject projectile;
    public AudioSource weaponSpeaker;
    public Transform firePoint;
    public Camera firingDirection;
    Vector3 fireforce;
    Vector3 arcdir;

    [Header("Meta Attributes")]
    public bool canFire = true;
    public bool holdToAttack = true;
    public bool reloading = false;
    public bool arc = false;
    public int weaponID;
    public string weaponName;

    [Header("Weapon Stats")]
    public float projLifespan;
    public float projVelocity;
    public float reloadCooldown;
    public float rof;
    public float arcup;
    public float scatter;
    public int clip;
    public int clipSize;

    [Header("Ammo Stats")]
    public int ammo;
    public int maxAmmo;
    public int ammoRefill;


    void Start()
    {
        weaponSpeaker = GetComponent<AudioSource>();
        firePoint = transform.GetChild(0);
        fireforce = firingDirection.transform.forward;
        arcdir = fireforce + (transform.up * arcup);
        scatter = Random.Range(1, 5);
        Mathf.RoundToInt (scatter);
        Explosion = GetComponent<Explosion>();
    }

    void Update()
    {
        

    }

    public void fire()
    {
        if (canFire && !reloading && clip > 0 && weaponID > -1)
        {
            weaponSpeaker.Play();

            fireforce = firingDirection.transform.forward;
            arcdir = fireforce + (transform.up * arcup);

            
            
            GameObject p = Instantiate(projectile, firePoint.position, firePoint.rotation);
            p.GetComponent<Rigidbody>().AddForce(fireforce * projVelocity);
            p.GetComponent<Rigidbody>().AddForce(transform.up * arcup, ForceMode.Impulse);
            Explosion.explode(projLifespan, p);





            clip--;
            canFire = false;
            StartCoroutine("cooldownFire");
        }
    }

    public void reload()
    {
        if (clip >= clipSize)
            return;

        else
        {
            int reloadCount = clipSize - clip;

            if (ammo < reloadCount)
            {
                clip += ammo;
                ammo = 0;
            }

            else
            {
                clip += reloadCount;
                ammo -= reloadCount;
            }

            reloading = true;
            canFire = false;
            StartCoroutine("reloadingCooldown");
            return;
        }
    }

    public void equip(PlayerController player)
    {
        player.currentWeapon = this;

        transform.SetPositionAndRotation(player.weaponSlot.position, player.weaponSlot.rotation);
        transform.SetParent(player.weaponSlot);

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;

        firingDirection = Camera.main;
        this.player = player;
    }

    public void unequip()
    {
        player.currentWeapon = null;

        transform.SetParent(null);

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().isTrigger = false;

        firingDirection = null;
        this.player = null;
    }

    IEnumerator cooldownFire()
    {
        yield return new WaitForSeconds(rof);

        if (clip > 0)
        {
            canFire = true;
        }
        scatter = Random.Range(1, 8);
        Mathf.RoundToInt(scatter);
    }

    IEnumerator reloadingCooldown()
    {
        yield return new WaitForSeconds(reloadCooldown);
        reloading = false;
        canFire = true;
    }
}