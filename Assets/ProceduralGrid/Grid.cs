using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public int xSize, ySize;

    private Vector3[] vertices;
    private Vector2[] UV;
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
        UV = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1,0,0,-1);
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                UV[i] = new Vector2((float)x/xSize, (float)y /ySize);
                tangents[i] = tangent;
                //yield return new WaitForSeconds(0.2f);
            }
        }

        triangles = new int[xSize*ySize*6];

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        yield return new WaitForEndOfFrame();

        int vi = 0;
        int ti = 0;
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[ti] = vi;
                triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                triangles[ti + 1] = triangles[ti + 4] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
                vi += 1;
                ti += 6;
            }

            vi += 1;
        }

        mesh.triangles = triangles;
        mesh.uv = UV;
        mesh.tangents = tangents;
        mesh.RecalculateNormals();
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
