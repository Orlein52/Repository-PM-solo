using UnityEngine;
using UnityEngine.AI;
public class BasicEnemy : MonoBehaviour

{
    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = GameObject.Find("Player").transform.position;
    }
}
