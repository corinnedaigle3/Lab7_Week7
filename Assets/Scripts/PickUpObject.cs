using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject pickupObject;
    public float rotationSpeed;
    void Start()
    {
        rotationSpeed = .001f;
        pickupObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    void Update()
    {
        pickupObject.transform.Rotate(1f * Time.deltaTime * rotationSpeed, 1f, 0f, Space.Self);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.gameObject);
            Debug.Log("Keys collected!");
        }
    }
}
