using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Dice_Logic : MonoBehaviour
{
    
    [SerializeField] private GameObject[] face;
    [SerializeField] private float requiredVelocity = 1.2f;
    
    private Rigidbody _rigidbody;
    private int _currentSide = 0;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(DiceLogicStart());
    }

    private IEnumerator DiceLogicStart()
    {
        while (true)
        {
            yield return new WaitUntil(() => _rigidbody.velocity.magnitude >= requiredVelocity);
            print("TROW IS HIGH");
            yield return new WaitWhile(() => _rigidbody.velocity.magnitude >= 0.001);
            print("DICE STOP");
            yield return new WaitForSeconds(1);
            WhichSideIsIt();
        }
    }

    private void WhichSideIsIt()
    {
        
        List<float> sideYPosition = new List<float>();
        foreach (var faceGameObject in face)
        {
            sideYPosition.Add(faceGameObject.transform.position.y);
        }
        _currentSide = sideYPosition.IndexOf(sideYPosition.Max()) + 1;
        print(_currentSide);
    }
}
