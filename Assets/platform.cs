using JetBrains.Annotations;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class platform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreateGround();
    }
    void CreateGround()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = new Vector3(10, 1, 10); //enlarges plane

        Renderer renderer = plane.GetComponent<Renderer>();
        renderer.material.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
