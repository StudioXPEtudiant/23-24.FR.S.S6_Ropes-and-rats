using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class SpacesManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] spaces;
    //[SerializeField] 
    private List<GameObject> _spacesStatusTrue = new List<GameObject>();
    //[SerializeField] 
    private GameObject _activeSpaces;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PiecesOnSpaces(GameObject space, bool space_status)
    {
        if (space_status)
        {
            if (!_spacesStatusTrue.Contains(space))
            {
                _spacesStatusTrue.Add(space);
            }
        }
        else
        {
            if (_spacesStatusTrue.Contains(space))
            {
                _spacesStatusTrue.Remove(space);
            }
        }
        //print(spacesStatusTrue.Length);
        if (_spacesStatusTrue.ToArray().Length > 1)
        {
            List<float> length = new List<float>();
            foreach (var gameObject in _spacesStatusTrue)
            {
                length.Add(gameObject.GetComponent<SingleSpaceManager>().GetPiecesDistenceValue());
                //print(gameObject);
            }
            GameObject selectedSpace = _spacesStatusTrue[length.IndexOf(length.Min())];
            foreach (var gameObject in _spacesStatusTrue)
            {
                if (gameObject != selectedSpace)
                {
                    gameObject.GetComponent<SingleSpaceManager>().StopParticle();
                }
            }
            
            if (space_status)
            {
                selectedSpace.GetComponent<SingleSpaceManager>().StartParticle();
                _activeSpaces = selectedSpace;
            }
            else
            {
                selectedSpace.GetComponent<SingleSpaceManager>().StopParticle();
                _activeSpaces = null;
            }
        }
        else
        {
            if (space_status)
            {
                space.GetComponent<SingleSpaceManager>().StartParticle();
                _activeSpaces = space;
            }
            else
            {
                space.GetComponent<SingleSpaceManager>().StopParticle();
                _activeSpaces = null;
            }
            
        }
        
    }

    public void UpdatePiecesOnSpaces()
    {
        List<float> length = new List<float>();
        foreach (var gameObject in _spacesStatusTrue)
        {
            length.Add(gameObject.GetComponent<SingleSpaceManager>().GetPiecesDistenceValue());
            //print(gameObject);
        }
        GameObject selectedSpace = _spacesStatusTrue[length.IndexOf(length.Min())];
        foreach (var gameObject in _spacesStatusTrue)
        {
            if (gameObject != selectedSpace)
            {
                gameObject.GetComponent<SingleSpaceManager>().StopParticle();
            }
        }
        
        selectedSpace.GetComponent<SingleSpaceManager>().StartParticle();
        _activeSpaces = selectedSpace;

    }
}
