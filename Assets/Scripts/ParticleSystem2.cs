using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleSystem2 : MonoBehaviour
{
    public int N;
    List<GameObject> particles;
    public GameObject particleObject;
    List<GameObject> particles2 = new List<GameObject>();
    void Start()
    {
        particles = new List<GameObject>();
        for(int i = 0; i < N; i++)
        {
            GameObject go = new GameObject();
            go.AddComponent<Particle3>(); //This is adding Particle.cs as a component of this gameObject
            Particle3 p = go.GetComponent<Particle3>();
            //ToDo: Set the parameters of "p" right here
            p = setParticle(p);

            particles.Add(go);
        }
        
    }

    Particle3 setParticle(Particle3 p)
    {
        
        float x = Random.Range(87, 111);
        float z = Random.Range(0, 8);
        float y = Random.Range(10, 22f);

        p.p = new Vector3(x, y, z);
        p.forces = Vector3.zero;
        p.forces.x = Random.Range(-0.5f, 0.5f);
        p.forces.z = Random.Range(-2, 2);
        p.r = Random.Range(1f, 2f);
        p.g = 9.81f;
        p.rc = 0.1f;
        p.mass = p.r * 2;
        p.dragUp = 0.00000001f;
        p.dragDown = 0.08f;

        p.sph = particleObject;

        return p;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject particle1 in particles)
        {
            bool p1Collision = false;
            Color colorp1 = particle1.GetComponent<Particle3>().color;
            foreach (GameObject particle2 in particles)
            {
                if(particle1 != particle2) //they all become red
                {
                    bool c = particle1.GetComponent<Particle3>().CheckCollision(particle2.GetComponent<Particle3>());
                    if (c)
                    {
                        //Debug.Log("Choque");
                        //particle1.GetComponent<Particle3>().sph.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                        //particle2.GetComponent<Particle3>().sph.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                        p1Collision = true;
                    }
                }
            }
            GameObject p1Car = GameObject.Find("Car");
            GameObject AiCar = GameObject.Find("AiCar1");
            GameObject Ai2Car = GameObject.Find("AiCar2");
            particles2.Add(p1Car);
            particles2.Add(AiCar);
            particles2.Add(Ai2Car);
            foreach (GameObject pCar in particles2)
            {
                bool d = particle1.GetComponent<Particle3>().CheckCollision(pCar.GetComponent<Particle3>());
                if (d)
                {
                    particle1.GetComponent<Particle3>().forces.z = pCar.GetComponent<Particle3>().forces.z;
                    particle1.GetComponent<Particle3>().forces.y = pCar.GetComponent<Particle3>().forces.y;
                    Debug.Log("Choque");
                    if(pCar.name == "Car")
                    {
                        GameObject.Find("GameManager").GetComponent<LifePointsCar>().lifePoints -= 1;
                    }
                    
                    p1Collision = true;
                }
            }
            particles2.Clear();
            if (p1Collision == false)
            {
                //Debug.Log("No choque");
                //particle1.GetComponent<Particle3>().sph.GetComponent<MeshRenderer>().material.SetColor("_Color", colorp1);
            }
        }
    }
}
