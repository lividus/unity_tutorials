using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationGrid : MonoBehaviour
{
    public Transform PointPrefab = null;
    public int GridResolution = 10;

    private Transform[] grid;
    private List<Transformation> Transformations;

    private Vector3 GetCoordinates(int x, int y, int z)
    {
        return new Vector3(
            x - (GridResolution - 1) * 0.5f,
            y - (GridResolution - 1) * 0.5f,
            z - (GridResolution - 1) * 0.5f);
    }

    private Transform CreateGridPoint(int x, int y, int z)
    {
        var result = Instantiate(PointPrefab);
        var coords = GetCoordinates(x, y, z);
        result.name = $"Point_[{coords.x}, {coords.y}, {coords.z}]";
        result.localPosition = coords;
        result.GetComponent<Renderer>().material.color = new Color(
            (float)x/GridResolution,
            (float)y / GridResolution,
            (float)z / GridResolution);

        return result;
    }

    void Awake()
    {
        grid = new Transform[GridResolution * GridResolution * GridResolution];
        int index = 0;
        for (int x = 0; x < GridResolution; x++)
        {
            for (int y = 0; y < GridResolution; y++)
            {
                for (int z = 0; z < GridResolution; z++)
                {
                    grid[index] = CreateGridPoint(x, y, z);
                    index += 1;
                }
            }
        }

        Transformations = new List<Transformation>();
    }

    private Vector3 TransformPoint(int x, int y, int z)
    {
        var point = GetCoordinates(x, y, z);
        for (var i = 0; i < Transformations.Count; i++)
        {
            point = Transformations[i].Apply(point);
        }

        return point;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponents<Transformation>(Transformations);
        int index = 0;
        for (int x = 0; x < GridResolution; x++)
        {
            for (int y = 0; y < GridResolution; y++)
            {
                for (int z = 0; z < GridResolution; z++)
                {
                    grid[index].localPosition = TransformPoint(x, y, z);
                    index += 1;
                }
            }
        }
    }
}
