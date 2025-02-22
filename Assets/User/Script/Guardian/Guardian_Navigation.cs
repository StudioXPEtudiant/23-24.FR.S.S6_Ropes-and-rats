using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]

public class GuardianNavigation : MonoBehaviour
{
    [SerializeField] private KeyPointList[] keyPointLists;
    [SerializeField] private bool randomPaths;
    private NavMeshAgent _agent;
    private int _currentList = 0;
    private Vector3 _destination;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _destination = _agent.destination;
        if (keyPointLists.Length == 0) 
        {
            Debug.LogWarning("No KeyPoints To Go for : " + this.name);
            return;
        }
        StartCoroutine(GuardPatrol());
    }

    IEnumerator GuardPatrol()
    {
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        if (randomPaths)
        {
            int randomNum = Random.Range(0, keyPointLists.Length);
            while (randomNum == _currentList) // this is to avoid repeat path
            {
                //print("need to gamble");
                randomNum = Random.Range(0, keyPointLists.Length);
            }
            _currentList = randomNum;
        }

        Debug.Log("Agent " + this.name + " new Path is : " + keyPointLists[_currentList].GetListName());
        foreach (var keyPointVector3 in keyPointLists[_currentList].GetVector3List())
        {
            _destination = keyPointVector3;
            _agent.destination = _destination;
            //Debug.Log("Agent " + this.name + " new destination is : " + _destination);
            yield return new WaitUntil(IsOnDestination);
        }

        if (!randomPaths)
        {
            if (_currentList >= keyPointLists.Length - 1)
            {
                _currentList = 0;
            }
            else
            {
                _currentList += 1;
            }
            
        }
        StartCoroutine(GuardPatrol());
    }

    bool IsOnDestination()
    {
        return Vector3.Distance(_destination, transform.position) < 1f;
    }

}
