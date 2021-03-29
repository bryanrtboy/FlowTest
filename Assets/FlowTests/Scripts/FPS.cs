using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPS : MonoBehaviour
{
    public TextMeshProUGUI fpsDisplay;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int fps = Mathf.RoundToInt(1 / Time.unscaledDeltaTime);
        fpsDisplay.text = "" + fps + " fps";
    }
}
