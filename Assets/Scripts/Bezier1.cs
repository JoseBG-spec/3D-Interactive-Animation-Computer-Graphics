using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier1 : MonoBehaviour
{
    // Start is called before the first frame update
    List<Vector3> ctrl;
    List<Vector3> bluePoints, movementPoints;
    GameObject greenSphere;
    int dir = -1;
    public int mov1 = 0;
    void Start()
    {
        ctrl = new List<Vector3>();
        bluePoints = new List<Vector3>();
        movementPoints = new List<Vector3>();

        ctrl.Add(new Vector3(0, 0, 0));
        ctrl.Add(new Vector3(1, 5, 0));
        ctrl.Add(new Vector3(5, 10, 0));
        ctrl.Add(new Vector3(7, 8, 0));
        ctrl.Add(new Vector3(10, 0, 0));

        Vector3 test1 = EvalBezier(ctrl, 0); // == ctrl[0]
        Debug.Log(test1);

        Vector3 test2 = EvalBezier(ctrl, 1.0f); // == ctrl[0]
        Debug.Log(test2);

        for(float x = 0; x < 1; x += 0.05f)
        {
            bluePoints.Add(EvalBezier(ctrl, x));
        }
        for (float x = 0; x < 1; x += 0.01f)
        {
            movementPoints.Add(EvalBezier(ctrl, x));
        }

        //pt1
        GameObject red1_sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        red1_sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        red1_sphere.transform.position = ctrl[0];

        //pt2
        GameObject red2_sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        red2_sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        red2_sphere.transform.position = ctrl[1];

        //pt3
        GameObject red3_sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        red3_sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        red3_sphere.transform.position = ctrl[2];

        //pt4
        GameObject red4_sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        red4_sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        red4_sphere.transform.position = ctrl[3];

        //pt5
        GameObject red5_sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        red5_sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        red5_sphere.transform.position = ctrl[4];

        //BlueSpheres
        foreach (Vector3 x in bluePoints)
        {
            GameObject bluePoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bluePoint.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
            bluePoint.transform.position = x;
            bluePoint.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        greenSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        greenSphere.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
        greenSphere.transform.position = ctrl[0];
        greenSphere.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);


    }

    // Update is called once per frame
    void Update()
    {
        greenSphere.transform.position = movementPoints[mov1];
        if (mov1 >= 100 || mov1 <= 0)
        {
            dir = -dir;
        }
        mov1 += dir * 1;
        

        
        
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
