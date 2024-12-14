using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SpacesManager : MonoBehaviour
{
    [SerializeField] private Dice_Logic dice;
    [SerializeField] private GameObject[] spaces;
    [SerializeField] private Gradient actifeSpacesGradient;
    [SerializeField] private Gradient targetSpacesGradient;
    
    
    //[SerializeField] //SerializeField is for Debug Only
    private List<GameObject> _spacesStatusTrue = new List<GameObject>();
    //[SerializeField] //SerializeField is for Debug Only
    private GameObject _activeSpaces;

    private int _currentTargetSpacesPosition;
    //[SerializeField] //SerializeField is for Debug Only
    private GameObject _targetSpaces;

    private void Start()
    {
        UpdateCurrentTargetSpacesPosition(0);
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

            if (!space_status)
            {
                space.GetComponent<SingleSpaceManager>().StopParticle();
            }
            selectedSpace.GetComponent<SingleSpaceManager>().StartParticle(actifeSpacesGradient);
            _activeSpaces = selectedSpace;

        }
        else
        {
            if (space_status)
            {
                space.GetComponent<SingleSpaceManager>().StartParticle(actifeSpacesGradient);
                _activeSpaces = space;
            }
            else
            {
                space.GetComponent<SingleSpaceManager>().StopParticle();
                _activeSpaces = null;
            }
            
        }
        if (_activeSpaces == _targetSpaces && _activeSpaces != null && _targetSpaces != null)
        {
            _targetSpaces.GetComponent<SingleSpaceManager>().StartParticle(actifeSpacesGradient);
            //print("Dice On Target enter");
            if (_targetSpaces.GetComponent<SingleSpaceManager>().SpaceModifier == 0)
            {
                _targetSpaces = null;
                dice.DiceState(true);
            }
            else
            {
                UpdateCurrentTargetSpacesPosition(_targetSpaces.GetComponent<SingleSpaceManager>().SpaceModifier);
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
                if (!gameObject.GetComponent<ParticleSystem>().isStopped)
                {
                    gameObject.GetComponent<SingleSpaceManager>().StopParticle();
                }
            }
        }

        if (!selectedSpace.GetComponent<ParticleSystem>().isPlaying)
        {
            selectedSpace.GetComponent<SingleSpaceManager>().StartParticle(actifeSpacesGradient);
        }
        _activeSpaces = selectedSpace;
        if (_activeSpaces == _targetSpaces && _activeSpaces != null && _targetSpaces != null)
        {
            _targetSpaces.GetComponent<SingleSpaceManager>().StartParticle(actifeSpacesGradient);
            //print("Dice On Target uptade");
            if (_targetSpaces.GetComponent<SingleSpaceManager>().SpaceModifier == 0)
            {
                _targetSpaces = null;
                dice.DiceState(true);
            }
            else
            {
                UpdateCurrentTargetSpacesPosition(_targetSpaces.GetComponent<SingleSpaceManager>().SpaceModifier);
            }
        }
    }

    public void UpdateCurrentTargetSpacesPosition(int movedSpace)
    {
        if (_currentTargetSpacesPosition + movedSpace >= spaces.Length)
        {
            _currentTargetSpacesPosition = spaces.Length - 1;
        }
        else if (_currentTargetSpacesPosition + movedSpace <= 0)
        {
            _currentTargetSpacesPosition = 0;
        }
        else
        {
            _currentTargetSpacesPosition += movedSpace;
        }
        _targetSpaces = spaces[_currentTargetSpacesPosition];
        _targetSpaces.GetComponent<SingleSpaceManager>().StartParticle(targetSpacesGradient);
        //print("Target Spaces Is " + _targetSpaces);
        
    }
}
