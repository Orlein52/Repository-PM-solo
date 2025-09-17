using UnityEngine;
using UnityEngine.AI;
public class ShootingEnemy : MonoBehaviour

{
    NavMeshAgent agent;
    Vector3 playerpos;
    float distance;
    public float StayAway = 5;
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
        agent.destination = GameObject.Find("Player").transform.position;
        distance = Vector3.Distance(agent.destination, transform.position);
        Debug.Log(distance);

        if (distance < StayAway)
        {
            agent.isStopped = false;

        }
        else
        {

            agent.isStopped = true;
        }
            
    }
}