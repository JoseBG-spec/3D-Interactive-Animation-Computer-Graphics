using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleSystem2 : MonoBehaviour
{
    public int N;
    List<GameObject> particles;
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
        
        float x = Random.Range(-5, 5);
        float z = Random.Range(-5, 5);
        float y = Random.Range(4.8f, 5);
        
        p.g = 5.81f;
        p.r = Random.Range(0.2f, 0.4f);
        p.rc = 0.4f;
        p.mass = p.r * 2;
        p.p = new Vector3(x, y, z);
        p.forces = Vector3.zero;
        p.forces.x = Random.Range(-1, 1);
        p.forces.z = Random.Range(-1, 1);
        p.dragUp = 0.000001f;
        p.dragDown = 0.07f;

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
                        Debug.Log("Choque");
                        particle1.GetComponent<Particle3>().sph.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                        particle2.GetComponent<Particle3>().sph.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                        p1Collision = true;
                    }
                }
            }
            if (p1Collision == false)
            {
                Debug.Log("No choque");
                particle1.GetComponent<Particle3>().sph.GetComponent<MeshRenderer>().material.SetColor("_Color", colorp1);
            }
        }
    }
}
