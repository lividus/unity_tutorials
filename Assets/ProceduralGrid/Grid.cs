﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public int xSize, ySize;

    private Vector3[] vertices;
    private int[] triangles;
    private Mesh mesh;

    void Awake()
    {
        StartCoroutine( GenerateMeshProcess());
    }

    private void GenerateMesh()
    {
        vertices = new Vector3[(xSize+1)*(ySize+1)];
        for (int i = 0, y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
            }
        }
    }

    private IEnumerator GenerateMeshProcess()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        yield return new WaitForSeconds(1f);
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                //yield return new WaitForSeconds(0.2f);
            }
        }

        triangles = new int[xSize*ySize*6];

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        yield return new WaitForEndOfFrame();

        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
            for (int x = 0; x < xSize; x++, vi++, ti += 6)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
                mesh.triangles = triangles;
                yield return new WaitForEndOfFrame();
            }

        
    }

    private void OnDrawGizmos()
    {
        if(vertices == null)
            return;
        if(vertices.Length == 0)
            return;
        
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(transform.TransformPoint(vertices[i]), 0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
