using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper : MonoBehaviour
{

    public Vector3 minPosition = Vector3.left;
    public Vector3 maxPosition = Vector3.right;
    public float scaleMultiplier = .01f;

    Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.localPosition;
    }


    public void MoveToPosition(float lerpValue)
    {
        transform.localPosition = startPosition + Vector3.Lerp(minPosition, maxPosition, lerpValue * scaleMultiplier);

    }


}
