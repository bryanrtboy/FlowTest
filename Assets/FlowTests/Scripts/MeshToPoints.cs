using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class MeshToPoints : MonoBehaviour
{
    public Mesh normalMesh;
    public Mesh[] blendToMeshes;
    public GameObject prefab;

    [Tooltip("If this list is empty, pointobjects will be generated at runtime. Hint: You can copy while" +
        " the game is running, stop the game and paste those objects " +
        " in the Editor to pre-populate this list for use as VFX Multiple Position Binding Targets.")]
    public List<GameObject> pointObjects;

    Dictionary<int, List<Vector3>> meshToPositions;
    List<Vector3> normalPositions;

    int currentMesh = 0;
    float currentWeight = 0f;


    // Start is called before the first frame update
    void Awake()
    {


        SetupPositions();

    }


    public void SetBlendWeightOnCurrentMesh(float weight)
    {
        currentWeight = weight;

        if (blendToMeshes.Length < 1)
        {
            Debug.LogError("Not enough Meshes for this script to work! Destroying myself now...");
            Destroy(this);
        }

        for (int i = 0; i < pointObjects.Count; i++)
        {
            //Go through the array of triangles and blend from the original [0] positions to the new ones
            pointObjects[i].transform.position = Vector3.Lerp(normalPositions[i], meshToPositions[currentMesh][i], weight);
        }
    }

    public void SetCurrent(int meshIndex)
    {
        if (meshIndex > blendToMeshes.Length)
        {
            Debug.LogError("You are trying to set to an index that does not exist. There are only " + blendToMeshes.Length + " meshes, and you specified the " + meshIndex + " index count!");
            return;
        }


        currentMesh = meshIndex;

        SetBlendWeightOnCurrentMesh(currentWeight);


    }

    void SetupPositions()
    {
        //Create a list of positions for each mesh in the MeshToObject array
        meshToPositions = new Dictionary<int, List<Vector3>>();

        for (int i = 0; i < blendToMeshes.Length; i++)
        {
            Vector3[] verticePositions = blendToMeshes[i].vertices;
            meshToPositions.Add(i, GetTriangleCenterPositions(verticePositions));
        }

        normalPositions = new List<Vector3>();
        normalPositions = GetTriangleCenterPositions(normalMesh.vertices);

        //Only make new gameObjects if the List is not pre-populated or has the wrong number of objects
        if (pointObjects == null || pointObjects.Count < 1 || pointObjects.Count != meshToPositions[0].Count)
        {
            pointObjects = new List<GameObject>();

            for (int i = 0; i < normalPositions.Count; i++)
            {
                GameObject p = Instantiate(prefab, normalPositions[i], Quaternion.identity);
                p.name = "point " + i;
                pointObjects.Add(p);
            }
        }


    }

    List<Vector3> GetTriangleCenterPositions(Vector3[] verticePositions)
    {
        List<Vector3> centroids = new List<Vector3>();

        for (int j = 0; j < verticePositions.Length; j += 6)
        {
            //Debug.Log("Mesh " + i + ", position is " + verticePositions[j]);

            if (j + 5 < verticePositions.Length)
            {
                Vector3 centroid = (verticePositions[j] + verticePositions[j + 1] + verticePositions[j + 2] + verticePositions[j + 3] + verticePositions[j + 4] + verticePositions[j + 5]) / 6;

                centroids.Add(centroid + transform.position);
            }
        }

        return centroids;

    }


}
