using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesToVFXPositions : MonoBehaviour
{
    public Line normalLine;
    public Line[] blendToLines;

    Dictionary<int, List<Vector3>> linePointsToPositions;
    List<Vector3> normalPositions;
    List<GameObject> pointObjects;

    int currentPosition = 0;
    float currentWeight = 0f;


    // Start is called before the first frame update
    void Awake()
    {
        SetupPositions();
    }


    public void SetBlendWeightOnCurrentLine(float weight)
    {
        currentWeight = weight;

        if (blendToLines.Length < 1)
        {
            Debug.LogError("Not enough Meshes for this script to work! Destroying myself now...");
            Destroy(this);
        }

        for (int i = 0; i < pointObjects.Count; i++)
        {
            //Go through the array of triangles and blend from the original [0] positions to the new ones
            pointObjects[i].transform.position = Vector3.Lerp(normalPositions[i], linePointsToPositions[currentPosition][i], weight);
        }
    }

    public void SetCurrent(int lineIndex)
    {
        if (lineIndex > blendToLines.Length)
        {
            Debug.LogError("You are trying to set to an index that does not exist. There are only " + blendToLines.Length + " meshes, and you specified the " + lineIndex + " index count!");
            return;
        }


        currentPosition = lineIndex;

        SetBlendWeightOnCurrentLine(currentWeight);


    }

    void SetupPositions()
    {
        //Create a list of positions for each point in the each Lines object points array
        linePointsToPositions = new Dictionary<int, List<Vector3>>();

        for (int i = 0; i < blendToLines.Length; i++)
        {
            List<Vector3> verticePositions = new List<Vector3>();

            for (int j = 0; j < blendToLines[i].points.Length; j++)
            {
                verticePositions.Add(blendToLines[i].points[j].transform.position);
            }

            linePointsToPositions.Add(i, verticePositions);
        }

        normalPositions = new List<Vector3>();
        pointObjects = new List<GameObject>();

        for (int i = 0; i < normalLine.points.Length; i++)
        {
            normalPositions.Add(normalLine.points[i].transform.position);
            pointObjects.Add(normalLine.points[i]);
        }

    }

}
