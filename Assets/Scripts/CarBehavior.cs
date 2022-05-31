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
    public int counter;

    public GameObject theCar;
    Vector3[] originals;
    public Vector3 start;
    public Vector3 end;
    Vector3 pos;
    float param;
    public float angle;

    void Start()
    {
        originals = theCar.GetComponent<MeshFilter>().mesh.vertices;
        objectsToLook = GameObject.Find("GameManager").GetComponent<Bezier>();
        
        counter = 0;
        angle = 0;


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
        if (Input.GetKey(KeyCode.W)) 
        {
            start = objectsToLook.movementPoints[counter];
            end = objectsToLook.movementPoints[counter + 1];
            param += 0.001f;
            pos = Interpolation(start, end, param);

            //Vector3 prev = Interpolation(start, end, param - 0.00005f);
            //Debug.Log("Start");
            //Debug.Log(start);
            //Debug.Log("End");
            //Debug.Log(end);
            Vector3 dir = pos - start;
            //Debug.Log("dir");
            //Debug.Log(dir);

            Vector3 du = dir.normalized;
            //Debug.Log(du);

            

            angle = Mathf.Atan(-du.z / du.x) * Mathf.Rad2Deg;
            

            Matrix4x4 t = Transformations.TranslateM(pos.x, pos.y, pos.z);
            Matrix4x4 r = Transformations.RotateM(angle, Transformations.AXIS.AX_Y);

            /*if (angle < 95 && angle > 0)
            {
                r = Transformations.RotateM(180 + angle, Transformations.AXIS.AX_Y);
            }
            if (angle == -90)
            {
                r = Transformations.RotateM(180 + angle, Transformations.AXIS.AX_Y);
            }*/
            //Debug.Log(angle + " duz: " + du.z + " dux: " + du.x);
            Matrix4x4 result = t * r;
            theCar.GetComponent<MeshFilter>().mesh.vertices = ApplyTransformation(originals, result);
            //theCar.GetComponent<MeshFilter>().mesh.RecalculateBounds();

            //Debug.Log(Time.frameCount);
            counter++;
            /*if (Time.frameCount % 300 == 0)
            {
                angle = Mathf.Atan(du.z / du.x) * Mathf.Rad2Deg;
                //end = objectsToLook.movementPoints[counter];

            }*/

        }
        if (Input.GetKey(KeyCode.S))
        {
            start = objectsToLook.movementPoints[counter + 1];
            end = objectsToLook.movementPoints[counter];
            param += 0.001f;
            pos = Interpolation(start, end, param);

            //Vector3 prev = Interpolation(start, end, param - 0.00005f);
            Vector3 dir = end - start;
            Vector3 du = dir.normalized;
            //Debug.Log(du);


            angle = Mathf.Atan(-du.z / du.x) * Mathf.Rad2Deg;
            Debug.Log(angle + " duz: " + du.z + " dux: " + du.x);

            Matrix4x4 t = Transformations.TranslateM(pos.x, pos.y, pos.z);
            Matrix4x4 r = Transformations.RotateM(angle, Transformations.AXIS.AX_Y);

            Matrix4x4 result = t * r;
            theCar.GetComponent<MeshFilter>().mesh.vertices = ApplyTransformation(originals, result);


            //Debug.Log(Time.frameCount);
            counter--;
            /*if (Time.frameCount % 300 == 0)
            {
                angle = Mathf.Atan(du.z / du.x) * Mathf.Rad2Deg;
                //end = objectsToLook.movementPoints[counter];

            }*/
        }

    }

    private void FixedUpdate()
    {
        
        /*if (objectToLook != null)
        {
            lookAtObject();
        }*/
    }
}
