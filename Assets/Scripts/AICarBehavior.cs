using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarBehavior : MonoBehaviour
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

    public float offsetx;
    public float offsetz;

    private int offsetCounter,sign;

    GameObject go;
    Particle3 pCar;
    public string pName;
    void Start()
    {
        originals = theCar.GetComponent<MeshFilter>().mesh.vertices;
        objectsToLook = GameObject.Find("GameManager").GetComponent<Bezier>();

        counter = 0;
        angle = 0;
        offsetCounter = 0;
        sign = 1; go = new GameObject();
        go.AddComponent<Particle3>();
        go.name = "AiCar" + pName;
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
    void FixedUpdate()
    {
        if (counter >= 2423)
        {
            counter = 0;
        }
        else if (counter < 0)
        {
            counter = 2422;
        }
        if (Time.frameCount > 50)
        {
            if (offsetCounter >= 8)
            {
                sign = -sign;
                offsetCounter = 0;
            }
            offsetCounter += offsetCounter * sign;
            start = objectsToLook.movementPoints[counter];
            start.x = start.x + Random.Range(offsetx + offsetCounter, offsetx);
            start.z = start.z + Random.Range(offsetz + offsetCounter, offsetz);
            end = objectsToLook.movementPoints[counter + 1];
            end.x = end.x + Random.Range(offsetx + offsetCounter, offsetx);
            end.z = end.z + Random.Range(offsetz + offsetCounter, offsetz);
            param += 0.001f;
            pos = Interpolation(start, end, param);
            pCar.sph.transform.position = pos;
            pCar.p = pos;

            Vector3 dir = pos - start;

            Vector3 du = dir.normalized;

            angle = Mathf.Atan(-du.z / du.x) * Mathf.Rad2Deg;


            Matrix4x4 t = Transformations.TranslateM(pos.x, pos.y, pos.z);
            Matrix4x4 r = Transformations.RotateM(angle, Transformations.AXIS.AX_Y);

            Matrix4x4 result = t * r;
            theCar.GetComponent<MeshFilter>().mesh.vertices = ApplyTransformation(originals, result);
            counter++;


        }
    }
}
