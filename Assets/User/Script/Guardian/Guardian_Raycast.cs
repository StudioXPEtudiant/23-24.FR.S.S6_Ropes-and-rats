using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// SI C'est les vertices sont trop nombreux, utiliser des point vector 3 placer manuellement.
public class Guardian_Raycast : MonoBehaviour
{
    [SerializeField] private GameObject[] DetectableObjects;
    [SerializeField] private MeshFilter TargetTest; // [DEBUG ONLY]
    // Start is called before the first frame update
    
    void Start()
    {
        StartCoroutine(CheckDetectableObjectVisibility());
        //StartCoroutine(DebugRaycast());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator CheckDetectableObjectVisibility()
    {
        yield return new WaitForSeconds(1f);
        foreach (var detectableGameObject in DetectableObjects)
        {
            if (detectableGameObject.GetComponent<MeshFilter>().mesh.vertices.Length < 20)
            {
                Debug.DrawLine(transform.position, detectableGameObject.transform.position,Color.red,2f);
            }
            yield return new WaitForSeconds(1f);
        }
        
        yield return new WaitForSeconds(2f);
        StartCoroutine(CheckDetectableObjectVisibility());
    }
    
    // To Test the raycast system.
    private IEnumerator DebugRaycast() // [DEBUG ONLY].
    {
        Debug.Log("Number of vertices : " + TargetTest.mesh.vertices.LongLength);
        foreach (var Vertices in TargetTest.mesh.vertices)
        {
            Vector3 verticesPosition = TargetTest.transform.TransformPoint(Vertices);
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, (verticesPosition - transform.position), out raycastHit))
            {
                //Debug.Log(Vector3.Angle(transform.forward,(verticesPosition - transform.position) ));
                if (DetectableObjects.Contains(raycastHit.transform.gameObject))
                {
                    Debug.DrawLine(transform.position,raycastHit.point,Color.green,2f);
                }
                else
                {
                    Debug.DrawLine(transform.position,raycastHit.point,Color.red,2f);
                    Debug.DrawLine(raycastHit.point,verticesPosition,Color.yellow,2f);
                }
                
            }
            //print(result);
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(DebugRaycast());
        
        
    }
}
