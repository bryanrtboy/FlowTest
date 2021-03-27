using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateUV : MonoBehaviour
{

    public SkinnedMeshRenderer m_meshRenderer;
    public Vector2 m_speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_meshRenderer.material.mainTextureOffset += m_speed * Time.deltaTime;
    }
}
