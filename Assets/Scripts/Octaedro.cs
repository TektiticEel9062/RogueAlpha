using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Octaedro : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 A, B, C, D, E, F, G, H, P, Q, N, nu;

    MeshFilter mf;
    MeshRenderer mr;
    public Material mat;
    List<Vector3> geometry;
    List<int> topology;
    float size;
    float alpha;



    void Start()
    {
        size = 2f;
        alpha = 0.7f;

        A = new Vector3(0, 0, 1);
        B = new Vector3(1, 0, 0);
        C = new Vector3(0, 1, -0);
        D = new Vector3(0, 0, -1);
        E = new Vector3(-1, 0, 0);
        F = new Vector3(0, -1, 0);


        mf = gameObject.AddComponent<MeshFilter>();
        geometry = new List<Vector3>() { A, B, C, D, E, F };
        topology = new List<int>() {    0,1,2,
                                        0,2,4,
                                        0,4,5,
                                        0,5,1,
                                        3,4,2,
                                        3,2,1,
                                        3,1,5
                                        ,3,5,4
        };

        for (int i = 0; i < 2; i++)
        {
            int triangleNumbers = topology.Count / 3;
            List<int> newTopology = new List<int>();

            for (int j = 0; j < triangleNumbers; j++)
            {
                List<int> prueba = new List<int>() { topology[0 + j * 3], topology[1 + j * 3], topology[2 + j * 3] };
                newTopology.AddRange(subdivide(prueba));
            }
            topology.Clear();
            topology.AddRange(newTopology);
        }

        // FDFF75

        // List<int> prueba = new List<int>() { 3, 5, 4 };
        // topology.AddRange(subdivide(prueba));

        Mesh miMesh = new Mesh();
        miMesh.vertices = geometry.ToArray();
        miMesh.triangles = topology.ToArray();
        miMesh.RecalculateNormals();
        miMesh.RecalculateBounds();
        mf.sharedMesh = miMesh;
        mr = gameObject.AddComponent<MeshRenderer>();
        Color color = mat.color;
        color.a = alpha;
        mat.color = color;
        mr.material = mat;
        gameObject.transform.localScale = new Vector3(2,2,2);

    }

    // Update is called once per frame
    void Update()
    {
        MakeBig();
    }

    void MakeBig()
    {
        if (size < 10)
        {
            size += 0.1f;
            gameObject.transform.localScale = new Vector3(size, size, size);
        }
        else {
            alpha -= 0.01f;
            Color color = mat.color;
            color.a = alpha;
            mat.color = color;
        }
    }

    


    List<int> subdivide(List<int> triangle)
    {


        int iA = triangle[0];
        int iB = triangle[1];
        int iC = triangle[2];
        int iD, iE, iF;

        Vector3 A = geometry[iA];
        Vector3 B = geometry[iB];
        Vector3 C = geometry[iC];

        Vector3 D = (A + B) / 2;
        Vector3 E = (B + C) / 2;
        Vector3 F = (A + C) / 2;

        D = D.normalized;
        E = E.normalized;
        F = F.normalized;


        if (geometry.Contains(D))
        {
            iD = geometry.IndexOf(D);
        }
        else
        {
            iD = geometry.Count;
            geometry.Add(D);
        }

        if (geometry.Contains(E))
        {
            iE = geometry.IndexOf(E);
        }
        else
        {
            iE = geometry.Count;
            geometry.Add(E);
        }

        if (geometry.Contains(F))
        {
            iF = geometry.IndexOf(F);
        }
        else
        {
            iF = geometry.Count;
            geometry.Add(F);
        }

        List<int> result = new List<int>()
        {
            iA, iD, iF,
            iD, iB, iE,
            iF, iE, iC,
            iD, iE, iF
        };

        return result;

    }
}
