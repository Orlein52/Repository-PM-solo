using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class ShootingEnemy : MonoBehaviour

{
    NavMeshAgent agent;
    Transform player;
    float distance;
    public float StayAway = 5f;
    Ray maybe;
    Vector3 run;
    Vector3 perchance;
    public float RunAway = 5f;
    public float advance = 5f;
    public float shoot;
    public GameObject projectile;
    public Transform firePoint;
    Vector3 fire;
    public bool fired;
    public float rof = 1;
    public bool ranged = true;
    

    Rigidbody rb;
    Ray boomRay;
    public int MaxHealth = 10;
    public int Health = 10;
    Vector3 boom;
    Vector3 boomdir;
    Vector3 boomPower;
    Vector3 fireForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        
        distance = Vector3.Distance(player.transform.position, transform.position);
        rb = GetComponent<Rigidbody>();
        shoot = Random.Range(0, 1);
        firePoint = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
        distance = Vector3.Distance(player.transform.position, transform.position);
        perchance = transform.position - player.transform.position;
        maybe = new Ray(player.transform.position, perchance);
        shoot = Random.Range(0, 1000);


        if (distance > StayAway)
        {
            agent.isStopped = false;
            agent.destination = player.transform.position;
            agent.speed = advance;
        }
        if (distance == StayAway)
        {

            agent.isStopped = true;
        }
        if (distance < StayAway)
        {
            run = maybe.GetPoint(StayAway);
            agent.isStopped = false;
            agent.destination = run;
            agent.speed = RunAway;
        }
        if (ranged)
        {
            if (shoot > 1 && !fired)
            {
                
                fire = firePoint.transform.position - player.transform.position - transform.up;
                GameObject p = Instantiate(projectile, firePoint.position, firePoint.rotation);
                p.GetComponent<Rigidbody>().AddForce(fire * -1000);
                Destroy(p, 3);
                fired = true;
                StartCoroutine("fireCooldown");
            }
        }
        if (Health <= 0)
        {
            Destroy(gameObject);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boom")
        {
            Health -= 5;
        }

    }
    IEnumerator fireCooldown()
    {
        yield return new WaitForSeconds(rof);
        fired = false;
    }
}