using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    PlayerController player;

    public Explosion Explosion;

    public GameObject projectile;
    public GameObject scatter1;      
    public GameObject scatter2;
    public GameObject scatter3;
    public GameObject scatter4;
    public GameObject scatter5;
    public GameObject scatter6;
    public GameObject scatter7;
    public GameObject scatter8;
    public AudioSource weaponSpeaker;
    public Transform firePoint;
    public Camera firingDirection;
    Vector3 fireforce;
    Vector3 arcdir;

    [Header("Meta Attributes")]
    public bool canFire = true;
    public bool holdToAttack = true;
    public bool reloading = false;
    public bool scattershot = false;
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
    public int fireModes;
    public int currentFireMode;
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
        scatter = Random.Range(1, 8);
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

            if (!scattershot && arc)
            {
                GameObject p = Instantiate(projectile, firePoint.position, firePoint.rotation);
                p.GetComponent<Rigidbody>().AddForce(fireforce  * projVelocity);
                p.GetComponent<Rigidbody>().AddForce(transform.up * arcup, ForceMode.Impulse);
                Explosion.explosionWait(projLifespan, p);
                Debug.Log("yes");
            }
            if (!scattershot && !arc)
            {
                GameObject p = Instantiate(projectile, firePoint.position, firePoint.rotation);
                p.GetComponent<Rigidbody>().AddForce(fireforce * projVelocity);

                Destroy(p, projLifespan);
            }
            if (scattershot && !arc)
            {
                if (scatter == 1)
                {
                    GameObject p = Instantiate(scatter1, firePoint.position, transform.rotation);
                    p.GetComponent<Rigidbody>().AddForce(fireforce * projVelocity);
                }
                if (scatter == 2)
                {
                    GameObject p = Instantiate(scatter2, firePoint.position, transform.rotation);
                    p.GetComponent<Rigidbody>().AddForce(fireforce * projVelocity);
                }
                if (scatter == 3)
                {
                    GameObject p = Instantiate(scatter3, transform.position, transform.rotation);
                    p.GetComponent<Rigidbody>().AddForce(fireforce * projVelocity);
                }
                if (scatter == 4)
                {
                    GameObject p = Instantiate(scatter4, firePoint.position, transform.rotation);
                    p.GetComponent<Rigidbody>().AddForce(fireforce * projVelocity);
                }
                if (scatter == 5)
                {
                    GameObject p = Instantiate(scatter5, firePoint.position, transform.rotation);
                    p.GetComponent<Rigidbody>().AddForce(fireforce * projVelocity);
                }
                if (scatter == 6)
                {
                    GameObject p = Instantiate(scatter6, firePoint.position, transform.rotation);
                    p.GetComponent<Rigidbody>().AddForce(fireforce * projVelocity);
                }
                if (scatter == 7)
                {
                    GameObject p = Instantiate(scatter7, firePoint.position, transform.rotation);
                    p.GetComponent<Rigidbody>().AddForce(fireforce * projVelocity);
                }
                if (scatter == 8)
                {
                    GameObject p = Instantiate(scatter8, firePoint.position, transform.rotation);
                    p.GetComponent<Rigidbody>().AddForce(fireforce * projVelocity);
                }

            }

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