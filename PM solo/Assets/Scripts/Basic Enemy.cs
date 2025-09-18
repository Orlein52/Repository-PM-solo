using UnityEngine;
using UnityEngine.AI;
public class BasicEnemy : MonoBehaviour

{
    NavMeshAgent agent;
    public float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = GameObject.Find("Player").transform.position;
        agent.speed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
        agent.destination = GameObject.Find("Player").transform.position;
    }
}
