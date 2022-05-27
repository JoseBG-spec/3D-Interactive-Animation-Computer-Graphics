using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    // Start is called before the first frame update
    List<Vector3> ctrl,ctrl2;
    public List<Vector3> movementPoints;
    GameObject greenSphere;
    int dir = -1;
    public int mov1 = 0;
    public GameObject racingPoints;
    public GameObject curveRacingPoints;
    public GameObject Car;
    void Start()
    {
        ctrl = new List<Vector3>();
        ctrl2 = new List<Vector3>();

        movementPoints = new List<Vector3>();

        for (int i = 0; i < racingPoints.transform.childCount; i++)
        {
            GameObject point = racingPoints.transform.GetChild(i).gameObject;
            GameObject point2 = curveRacingPoints.transform.GetChild(i).gameObject;
            ctrl.Add(point.transform.position);
            ctrl2.Add(point2.transform.position);
        }
        List<Vector3> temp = new List<Vector3>();
        
        for (int i = 0; i < ctrl.Count-1; i++)
        {
            for (float x = 0; x < 1; x += 0.01f)
            {
                temp = new List<Vector3>();
                temp.Add(ctrl[i]);
                temp.Add(ctrl2[i]);
                temp.Add(ctrl[i+1]);
                movementPoints.Add(EvalBezier(temp, x));
            }
        }

        //printRedDots();


    }
    void Update()
    {
        
        

        
        
    }

    void printRedDots()
    {
        for (int i = 0; i < movementPoints.Count; i++)
        {
            GameObject red_sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            red_sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            red_sphere.transform.position = movementPoints[i];
        }
    }
    Vector3 EvalBezier(List<Vector3> P,float t)
    {
        int n = P.Count;
        Vector3 p = Vector3.zero;
        for(int i = 0; i < n; i++)
        {
            p += Combination(n-1, i) * Mathf.Pow(1.0f - t, n -1 - i)
                * Mathf.Pow(t, i) * P[i];
        }
        return p;

    }
    float Combination(int n, int i)
    {
        return (float)Factorial(n) / (Factorial(i) * Factorial(n - i));
    }
    int Factorial(int n)
    {
        if (n == 0) return 1;
        else return n * Factorial(n - 1);
    }
}
