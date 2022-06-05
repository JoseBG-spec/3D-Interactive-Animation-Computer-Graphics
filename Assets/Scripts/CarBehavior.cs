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
    GameObject go;
    Particle3 pCar;
    private GameObject arrow;

    void Start()
    {
        originals = theCar.GetComponent<MeshFilter>().mesh.vertices;
        objectsToLook = GameObject.Find("GameManager").GetComponent<Bezier>();
        arrow = theCar.transform.GetChild(0).gameObject;
        counter = 0;
        angle = 0;
        go = new GameObject();
        go.AddComponent<Particle3>();
        go.name = "Car";
        pCar = go.GetComponent<Particle3>();
        pCar = setParticle(pCar);
        //pCar.sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //pCar.sph.transform.localScale = new Vector3(pCar.r * 2, pCar.r * 2, pCar.r * 2);

    }

    Particle3 setParticle(Particle3 p)
    {
        p.p = Vector3.zero;
        p.forces = new Vector3(0, 150, 200);
        p.r = 7.5f;
        p.g = 0;
        p.rc = 7.5f;
        p.mass = p.r * 2;
        p.dragUp = 0.00000001f;
        p.dragDown = 0.08f;

        return p;
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
        if(counter >= 2423)
        {
            counter = 0;
        }else if (counter < 0)
        {
            counter = 2422;
        }
        if (Input.GetKey(KeyCode.K)) 
        {
            start = objectsToLook.movementPoints[counter];
            end = objectsToLook.movementPoints[counter + 1];
            param += 0.001f;
            pos = Interpolation(start, end, param);
            pCar.sph.transform.position = pos;
            pCar.p = pos;
            arrow.transform.position = new Vector3(pos.x,arrow.transform.position.y,pos.z);
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
            var mf = theCar.GetComponent<MeshFilter>();
            mf.mesh.bounds = new Bounds(pos, new Vector3(10, 5, 1));
            //theCar.GetComponent<MeshFilter>().mesh.RecalculateBounds();

            //Debug.Log(Time.frameCount);
            counter++;
            /*if (Time.frameCount % 300 == 0)
            {
                angle = Mathf.Atan(du.z / du.x) * Mathf.Rad2Deg;
                //end = objectsToLook.movementPoints[counter];

            }*/

        }
        if (Input.GetKey(KeyCode.L))
        {
            start = objectsToLook.movementPoints[counter + 1];
            end = objectsToLook.movementPoints[counter];
            param += 0.001f;
            pos = Interpolation(start, end, param);
            pCar.sph.transform.position = pos;
            pCar.p = pos;
            arrow.transform.position = new Vector3(pos.x, arrow.transform.position.y, pos.z);
            //Vector3 prev = Interpolation(start, end, param - 0.00005f);
            Vector3 dir = end - start;
            Vector3 du = dir.normalized;
            //Debug.Log(du);


            angle = Mathf.Atan(-du.z / du.x) * Mathf.Rad2Deg;
            //Debug.Log(angle + " duz: " + du.z + " dux: " + du.x);

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
