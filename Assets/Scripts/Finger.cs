using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
    List<GameObject> elements; // Store the sun and planets
    List<Vector3[]> originalVertices;
    List<Matrix4x4> trans, mRot;
    Matrix4x4 scale;
    private float rot1, rot2, rot3;
    public float dir;
    // Start is called before the first frame update

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

    void Start()
    {
        originalVertices = new List<Vector3[]>();
        elements = new List<GameObject>();
        trans = new List<Matrix4x4>();
        rot1 = 0;
        rot2 = 0;
        rot3 = 0;
        dir = 1;

        // Cube 1
        GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        elements.Add(cube1);
        originalVertices.Add(elements[0].GetComponent<MeshFilter>().mesh.vertices);
        // Cube 2
        GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        elements.Add(cube2);
        originalVertices.Add(elements[1].GetComponent<MeshFilter>().mesh.vertices);
        // Cube 3
        GameObject cube3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        elements.Add(cube3);
        originalVertices.Add(elements[2].GetComponent<MeshFilter>().mesh.vertices);

        scale = Transformations.ScaleM(2f, 1f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        mRot = new List<Matrix4x4>();
        if (rot1 > 45 || rot1 < -45)
        {
            dir = -dir;
        }
        rot1 += dir * .05f;
        rot2 += dir * .05f;
        rot3 += dir * .05f;
        trans.Add(Transformations.TranslateM(1f, 0, 0));
        trans.Add(Transformations.TranslateM(2f, 0, 0));
        trans.Add(Transformations.TranslateM(2f, 0, 0));

        mRot.Add(Transformations.RotateM(rot1, Transformations.AXIS.AX_Z));
        mRot.Add(Transformations.RotateM(rot2, Transformations.AXIS.AX_Z));
        mRot.Add(Transformations.RotateM(rot3, Transformations.AXIS.AX_Z));

        Matrix4x4 matrix1 = mRot[0] * trans[0] * scale;
        Matrix4x4 matrix1No = mRot[0] * trans[0];
        elements[0].GetComponent<MeshFilter>().mesh.vertices = ApplyTransformation(originalVertices[0], matrix1);

        Matrix4x4 matrix2 = matrix1No * mRot[1] * trans[1] * scale;
        Matrix4x4 matrix2No = matrix1No * mRot[1] * trans[1];
        elements[1].GetComponent<MeshFilter>().mesh.vertices = ApplyTransformation(originalVertices[1], matrix2);

        Matrix4x4 matrix3 = matrix2No * mRot[2] * trans[2] * scale;
        Matrix4x4 matrix3No = matrix2No * mRot[2] * trans[2];
        elements[2].GetComponent<MeshFilter>().mesh.vertices = ApplyTransformation(originalVertices[2], matrix3);



    }
}