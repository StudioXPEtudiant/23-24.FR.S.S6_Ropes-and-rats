using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Guardian_Navigation : MonoBehaviour
{
    [SerializeField] private Vector3[] keyPoints;
    private NavMeshAgent agent;
    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        if (keyPoints.Length == 0) 
        {
            Debug.LogWarning("No KeyPoints To Go for : " + this.name);
            return;
        }
        StartCoroutine(GuardPatrol());
    }

    IEnumerator GuardPatrol()
    {
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        destination = keyPoints[Random.Range(0, keyPoints.Length)];
        agent.destination = destination;
        Debug.Log("Agent " + this.name + " new destination is : " + destination);
        yield return new WaitUntil(IsOnDestination);
        StartCoroutine(GuardPatrol());
    }

    bool IsOnDestination()
    {
        return Vector3.Distance(destination, transform.position) < 1f;
    }

}
