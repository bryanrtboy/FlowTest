using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class Blender : MonoBehaviour
{
    public float blendShapeMultiplier = 100f;
    int currentBlend = 0;
    int blendShapeCount;
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;


    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    void Start()
    {
        blendShapeCount = skinnedMesh.blendShapeCount;

        if (blendShapeCount < 1)
        {
            Debug.LogError("Get your blendshapes dude!!!");
            Destroy(this);
        }
    }


    public void SetBlendWeightOnCurrentShape(float weight)
    {
        //Set all the blendshape weights to zero
        for (int i = 0; i < blendShapeCount; i++)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(i, 0f);
        }

        //set the current blend to the specified value
        skinnedMeshRenderer.SetBlendShapeWeight(currentBlend, weight * blendShapeMultiplier);

    }

    public void SetCurrent(int blendIndex)
    {
        if (blendIndex > blendShapeCount)
        {
            Debug.LogError("You are trying to blend to an index that does not exist. There are only " + blendShapeCount + " blendshapes, and you specified the " + blendIndex + " index count!");
            return;
        }

        //Cache the current blendshape weight
        float currentWeight = skinnedMeshRenderer.GetBlendShapeWeight(currentBlend);

        currentBlend = blendIndex;

        SetBlendWeightOnCurrentShape(currentWeight / blendShapeMultiplier);


    }


}
