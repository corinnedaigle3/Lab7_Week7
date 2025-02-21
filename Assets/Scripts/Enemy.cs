using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject pills;
    public Vector3 startPoints;
    public Vector3 endPoints;
    public float speed = 2f;
    public float t = 0;

    void Start()
    {

        // Define start and end points
        startPoints = new Vector3(-5, 1, -1);
        endPoints = new Vector3(-5, 1, -8.5f);

        // Set different colors for each object
        pills.GetComponent<Renderer>().material.color = Color.blue;
    }

    void Update()
    {

        // MoveTowards for second pill
        pills.transform.position = Vector3.MoveTowards(pills.transform.position, endPoints, speed * Time.deltaTime);
        if (Vector3.Distance(pills.transform.position, endPoints) < 0.1f)
        {
            (startPoints, endPoints) = (endPoints, startPoints);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Hey! I'm walking here!");
            pills.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
