using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(ParticleSystem))]
public class SingleSpaceManager : MonoBehaviour
{

    private ParticleSystem _particleSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Pieces"))
        {
            _particleSystem.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Pieces"))
        {
            _particleSystem.Stop();
        }
    }
}
