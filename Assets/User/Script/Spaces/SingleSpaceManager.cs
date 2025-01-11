using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(ParticleSystem))]
public class SingleSpaceManager : MonoBehaviour
{
    [SerializeField] public int SpaceModifier = 0;
    
    private ParticleSystem _particleSystem;
    private ParticleSystem.MainModule _mainModule;
    private SpacesManager _spacesManager;

    private float _piecesDistanceFromCenter;
    
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = this.GetComponent<ParticleSystem>();
        _spacesManager = GetComponentInParent<SpacesManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Pieces"))
        {
            GetPiecesDistance(other.gameObject);
            _spacesManager.PiecesOnSpaces(this.gameObject, true);
            //StartParticle();
        }
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Contains("Pieces"))
        {
            if (_piecesDistanceFromCenter == Vector3.Distance(other.transform.position,
                    this.transform.position)) return;
            //print("trigger");
            GetPiecesDistance(other.gameObject);
            _spacesManager.UpdatePiecesOnSpaces();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Pieces"))
        {
            _spacesManager.PiecesOnSpaces(this.gameObject, false);
            //StopParticle();
        }
    }

    private void GetPiecesDistance(GameObject gameObject)
    {

        _piecesDistanceFromCenter = Vector3.Distance(gameObject.transform.position,this.transform.position);
        //print(_piecesDistanceFromCenter);
    }
    

    public void StartParticle(Gradient gradient)
    {
        var mainGradian = _particleSystem.main;
        mainGradian.startColor = gradient;
        _particleSystem.Play();
    }
    
    public void StopParticle()
    {
        _particleSystem.Stop();
    }

    public float GetPiecesDistenceValue()
    {
        return _piecesDistanceFromCenter;
    }
}
