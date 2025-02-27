using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform patrolRoute; //waypoints
    public Transform pills; //Player
    //public Vector3 startPoints;
    //public Vector3 endPoints;

    public float speed = 2f;
    public int t = 0;
    private NavMeshAgent agent;
    private Transform[] locations;
    private bool chasingPlayer = false;

    void Start()
    {

        // Define start and end points
        //startPoints = new Vector3(-5, 1, -1);
        //endPoints = new Vector3(-5, 1, -8.5f);
        agent = GetComponent<NavMeshAgent>();
        // Set different colors for each object
        pills.GetComponent<Renderer>().material.color = Color.blue;
        InitializePatrolRoute();
        MoveToNextPatrolLocation();


    }

    void Update()
    {

        // MoveTowards for second pill
       // pills.transform.position = Vector3.MoveTowards(pills.transform.position, endPoints, speed * Time.deltaTime);
       // if (Vector3.Distance(pills.transform.position, endPoints) < 0.1f)
        {
            //(startPoints, endPoints) = (endPoints, startPoints);
        }
        //locate player and chase if within close proximity
        if (!chasingPlayer && !agent.pathPending && agent.remainingDistance < 0.2f)
        {
            MoveToNextPatrolLocation();
        }
        
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Length == 0) return;
        {
            agent.SetDestination(locations[t].position);
            t = (t + 1) % locations.Length;
        }
    }

    void InitializePatrolRoute()
    {
        locations = new Transform[patrolRoute.childCount];
        for (int i = 0; i < patrolRoute.childCount; i++)
        {
            locations[i] = patrolRoute.GetChild(i);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Hey! I'm walking here!");
            pills.GetComponent<Renderer>().material.color = Color.red;
        }

        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Player detected - start chasing!");
            chasingPlayer = true;
            agent.SetDestination(pills.position);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Player out of range - resume patrol.");
            chasingPlayer = false;
            MoveToNextPatrolLocation();
        }
    }
}
