using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavior : MonoBehaviour
{
    public GameObject objectThatLooks;
    public Vector3 objectToLook;
    public float yPos;
    // Start is called before the first frame update
    private Vector3 objectToLookPosition;
    public Bezier objectsToLook;
    private int counter;
    void Start()
    {
        counter = 0;
        objectToLook = objectsToLook.movementPoints[counter];
    }

    void Update()
    {
        //Debug.Log(Time.frameCount);
        if(Time.frameCount % 1000 == 0)
        {
            counter++;
            objectToLook = objectsToLook.movementPoints[counter];
            
        }
    }

    private void FixedUpdate()
    {
        if (objectToLook != null)
        {
            lookAtObject();
        }
    }

    private void lookAtObject()
    {
        objectToLookPosition.y = yPos;
        objectThatLooks.transform.LookAt(objectToLook, Vector3.up);
        
    }
}
