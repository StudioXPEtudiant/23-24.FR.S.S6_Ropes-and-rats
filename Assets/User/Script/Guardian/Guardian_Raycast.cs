using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

// SI les vertices sont trop nombreux, utiliser des point vector 3 a placer manuellement.
// Systeme vertices a étais abandoner.
public class Guardian_Raycast : MonoBehaviour
{
    [FormerlySerializedAs("DetectionPointObjects")] [SerializeField] 
    [Tooltip("Put the GameObject You want to be detectable" + " [The GameObject Need to have a children name ''DetectionPoint'']")]
    private GameObject[] DetectableObjects;

    [SerializeField] private float TimeBetwenCheck = 0.5f;

    [SerializeField] private int MinimumPointNeededForGameOver = 7;
    
    private int _numberOfDetectablePoint;
    
    //[SerializeField] 
    private MeshFilter TargetTest; // [DEBUG ONLY]

    void Start()
    {
        foreach (var parentObject in DetectableObjects)
        {
            if (parentObject.transform.Find("DetectionPoint"))
            {
                Transform detectionPoint = parentObject.transform.Find("DetectionPoint");
                _numberOfDetectablePoint += detectionPoint.childCount;
            }
            else
            {
                Debug.LogError("Object ''" + parentObject + "'' dosent have a child ''DetectionPoint''.");
            }
        }
        StartCoroutine(CheckDetectableObjectVisibility());
        //StartCoroutine(DebugRaycast());
    }
    private IEnumerator CheckDetectableObjectVisibility()
    {
        yield return new WaitForSeconds(TimeBetwenCheck);
        List<Transform> detectionPointPosition = new List<Transform>();
        foreach (var parentObject in DetectableObjects)
        {
            if (parentObject.transform.Find("DetectionPoint"))
            {
                Transform detectionPoint = parentObject.transform.Find("DetectionPoint");
                for (int i = 0; i < detectionPoint.childCount; i++)
                {
                    //Debug.Log(detectionPoint.childCount);
                    detectionPointPosition.Add(detectionPoint.GetChild(i));
                }
            }
            else
            {
                Debug.LogError("Object ''" + parentObject + "'' dosent have a child ''DetectionPoint''.");
            }
        }

        int numberOfDetectedPoint = 0;
        foreach (var point in detectionPointPosition)
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, (point.position - transform.position), out raycastHit))
            {
                if (DetectableObjects.Contains(raycastHit.transform.gameObject))
                {
                    Debug.DrawLine(transform.position,raycastHit.point,Color.green,TimeBetwenCheck);
                    numberOfDetectedPoint += 1;
                }
                else
                {
                    Debug.DrawLine(transform.position,raycastHit.point,Color.red,TimeBetwenCheck);
                    //Debug.DrawLine(raycastHit.point,point.position,Color.yellow,TimeBetwenCheck);
                }
            }
        }
        
        //Debug.Log(numberOfDetectedPoint + " / " + _numberOfDetectablePoint + " point as been detected.");
        if (numberOfDetectedPoint >= MinimumPointNeededForGameOver)
        {
            //Debug.Log("Guardian See To much point (GameOver)");
            Debug.DrawLine(transform.position,transform.position + new Vector3(0,3,0),Color.green,TimeBetwenCheck);
        }
        else
        {
            Debug.DrawLine(transform.position,transform.position + new Vector3(0,3,0),Color.red,TimeBetwenCheck);
        }
        
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
