using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class SpacesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spaces;

    private Dictionary<GameObject, bool> _spacesStatusDictionary = new Dictionary<GameObject, bool>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var gameObject in spaces)
        {
            _spacesStatusDictionary.Add(gameObject, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<GameObject> GiveKeyWhitTrueValue(Dictionary<GameObject, bool> dictionary)
    {
        List<GameObject> trueGameObjects = new List<GameObject>();
        foreach (var KEY in dictionary)
        {
            if (KEY.Value.Equals(true))
            {
                trueGameObjects.Add(KEY.Key);
                //print(KEY.Key);
            }
        }

        return trueGameObjects;
    }

    public void PiecesOnSpaces(GameObject space, bool space_status)
    {
        
        
        
        _spacesStatusDictionary[space] = space_status;
        GameObject[] spacesStatusTrue = GiveKeyWhitTrueValue(_spacesStatusDictionary).ToArray();
        //print(spacesStatusTrue.Length);
        if (spacesStatusTrue.Length > 1)
        {
            List<float> length = new List<float>();
            foreach (var gameObject in spacesStatusTrue)
            {
                length.Add(gameObject.GetComponent<SingleSpaceManager>().GetPiecesDistenceValue());
                //print(gameObject);
            }
            GameObject selectedSpace = spacesStatusTrue[length.IndexOf(length.Min())];
            foreach (var gameObject in spacesStatusTrue)
            {
                if (gameObject != selectedSpace)
                {
                    gameObject.GetComponent<SingleSpaceManager>().StopParticle();
                }
            }
            
            if (space_status)
            {
                selectedSpace.GetComponent<SingleSpaceManager>().StartParticle();
            }
            else
            {
                selectedSpace.GetComponent<SingleSpaceManager>().StopParticle();
            }
        }
        else
        {
            if (space_status)
            {
                space.GetComponent<SingleSpaceManager>().StartParticle();
            }
            else
            {
                space.GetComponent<SingleSpaceManager>().StopParticle();
            }
            
        }
        
    }
}
