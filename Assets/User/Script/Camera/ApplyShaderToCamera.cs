using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteAlways]
public class ApplyShaderToCamera : MonoBehaviour
{
    [SerializeField] private Material MaterialShaders;

    private Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = this.GetComponent<Camera>();
        if (MaterialShaders == null)
        {
            Debug.LogWarning("No Shader attached To " + this.name);
            GetComponent<ApplyShaderToCamera>().enabled = false;
        }
        _mainCamera.SetReplacementShader(MaterialShaders.shader, null);
    }

    // Update is called once per frame
    void Update()
    {
        _mainCamera.RenderWithShader(MaterialShaders.shader, String.Empty);
    }
}
