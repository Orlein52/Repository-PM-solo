using UnityEngine;
using UnityEngine.AI;
public class ShootingEnemy : MonoBehaviour

{
    NavMeshAgent agent;
    Vector3 playerpos;
    float distance;
    public float StayAway = 5f;
    Ray maybe;
    Vector3 run;
    Vector3 perchance;
    public float RunAway = 5f;
    public float advance = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        playerpos = GameObject.Find("Player").transform.position;
        distance = Vector3.Distance(playerpos, transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        distance = Vector3.Distance(playerpos, transform.position);
        perchance = transform.position - playerpos;
        maybe = new Ray(playerpos, perchance);
        playerpos = GameObject.Find("Player").transform.position;


        
        if (distance > StayAway)
        {
            agent.isStopped = false;
            agent.destination = playerpos;
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
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boom")
        {
            
            Destroy(gameObject);

        }
    }
}