using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(ParticleSystem))]
public class SingleSpaceManager : MonoBehaviour
{

    private ParticleSystem _particleSystem;
    private SpacesManager _spacesManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = this.GetComponent<ParticleSystem>();
        _spacesManager = GetComponentInParent<SpacesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Pieces"))
        {
            _spacesManager.PiecesOnSpaces(this.gameObject, true);
            StartParticle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Pieces"))
        {
            _spacesManager.PiecesOnSpaces(this.gameObject, false);
            StopParticle();
        }
    }

    public void StartParticle()
    {
        _particleSystem.Play();
    }
    
    public void StopParticle()
    {
        _particleSystem.Stop();
    }
}
