using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle3 : MonoBehaviour
{
    public float mass;
    public float g;         // gravedad
    public float r;         // radio
    public float rc;        // Restitution Coefficient (elastic=1, inelastic = 0)
    public Vector3 p;       // position
    public Vector3 forces;
    public Color color;
    public Vector3 a;       // acceleration
    public float dragUp;
    public float dragDown;

    public Vector3 drag;
    public Vector3 prev;       // previous position
    Vector3 temp;       // temporal position
    public GameObject sph;     // game object for the particle
    public bool car =false;

    // Start is called before the first frame update
    void Start()
    {
        //sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        if (sph != null)
        {
            sph = Instantiate(sph);
            sph.transform.localScale = new Vector3(r * 1.5f, r * 1.5f, r * 1.5f);
        }
        else
        {
            sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            car = true;
            sph.transform.localScale = new Vector3(r * 2, r * 2, r * 2);
            Material newMat = Resources.Load("ParticleTransparent", typeof(Material)) as Material;
            sph.GetComponent<MeshRenderer>().material = newMat;

        }

        sph.transform.position = p;
        

        float cr = Random.Range(0.0f, 1.0f);
        float cg = Random.Range(0.0f, 1.0f);
        float cb = Random.Range(0.0f, 1.0f);
        color = new Color(cr, cg, cb);
        //sph.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        prev = p;
        a = Vector3.zero;
        drag.y = 1;
    }

    void Verlet(float dt)
    {
        temp = p;                           // save p temporarily
        a = forces / mass;                  // a = F/m
        p = 2 * p - prev + (a * dt * dt);   // Verlet
        prev = temp;                        // restore previous position
        sph.transform.position = p;
    }

    // Update is called once per frame
    void Update()
    {
        if (!car)
        {
            if (Time.frameCount > 20)
            {
                forces.y += -g * mass * 0.02f;
                if (p.y > prev.y) drag = -forces * dragUp;
                else if (p.y < prev.y) drag = -forces * dragDown;
                else drag = Vector3.zero;
                forces += drag;
                CheckFloor();
                //CheckCubeWalls();
                Verlet(0.02f);
            }
        }
        
    }

    void CheckFloor()
    {
        if (p.y < r)
        {
            forces.y = -forces.y * rc;
            //Debug.Log("F=" + forces.ToString("F5"));
            float diff = prev.y - p.y;
            p.y = r;
            prev.y = r - diff;
        }
    }

    void CheckCubeWalls()
    {
        if (p.y > 20 - r)
        {
            forces.y = -forces.y * rc;
            float diff = prev.y - p.y;
            p.y = 15f - r;
            prev.y = 15f - r - diff;
        }

        if (p.x > 88 - r)
        {
            forces.x = -forces.x * rc;
            float diff = prev.x - p.x;
            p.x = 7.5f - r;
            prev.x = 7.5f - r - diff;
        }
        else if (p.x < 112 + r)
        {
            forces.x = -forces.x * rc;
            float diff = prev.x - p.x;
            p.x = -7.5f + r;
            prev.x = -7.5f + r - diff;
        }


        if (p.z > 3 - r)
        {
            forces.z = -forces.z * rc;
            float diff = prev.z - p.z;
            p.z = 7.5f - r;
            prev.z = 7.5f - r - diff;
        }
        else if (p.z < 7.3 + r)
        {
            forces.z = -forces.z * rc;
            float diff = prev.z - p.z;
            p.z = -7.5f + r;
            prev.z = -7.5f + r - diff;
        }


    }

    public bool CheckCollision(Particle3 other)
    {
        float dx = other.p.x - p.x;
        float dy = other.p.y - p.y;
        float dz = other.p.z - p.z;

        float sumR = other.r + r;
        sumR *= sumR;

        return sumR > (dx * dx + dy * dy + dz * dz);
    }
}
