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

    public GameObject theCar;
    Vector3[] originals;
    public Vector3 start;
    public Vector3 end;
    Vector3 pos;
    float param;

    void Start()
    {
        originals = theCar.GetComponent<MeshFilter>().mesh.vertices;
        
        Matrix4x4 t = Transformations.TranslateM(3, 0, 0);
        theCar.GetComponent<MeshFilter>().mesh.vertices = ApplyTransformation(originals,t);
        counter = 0;
        objectToLook = objectsToLook.movementPoints[counter];
    }

    Vector3 Interpolation(Vector3 A, Vector3 B, float t)
    {
        return A + t * (B - A);
    }
    Vector3[] ApplyTransformation(Vector3[] verts, Matrix4x4 m)
    {
        int number = verts.Length;
        Vector3[] result = new Vector3[number];
        for (int i = 0; i < number; i++)
        {
            Vector3 v = verts[i];
            Vector4 temp = new Vector4(v.x, v.y, v.z, 1);
            result[i] = m * temp;
        }
        return result;
    }
    void Update()
    {
        param += 0.0001f;
        pos = Interpolation(start, end, param);
        Vector3 prev = Interpolation(start, end, param - 0.00005f);
        Vector3 dir = pos - prev;
        Vector3 du = dir.normalized;

        Matrix4x4 t = Transformations.TranslateM(pos.x, pos.y, pos.z);
        theCar.GetComponent<MeshFilter>().mesh.vertices = ApplyTransformation(originals, t);

        //Debug.Log(Time.frameCount);
        /*if(Time.frameCount % 1000 == 0)
        {
            counter++;
            objectToLook = objectsToLook.movementPoints[counter];
            
        }*/
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
