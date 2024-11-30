using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Dice_Logic : MonoBehaviour
{
    [SerializeField] private SpacesManager GameboardSpacesManager;
    [SerializeField] private ParticleSystem DiceStateParticleSystem;
    
    [SerializeField] private GameObject[] face;
    [SerializeField] private float requiredVelocity = 1.2f;
    
    private Rigidbody _rigidbody;
    private int _currentSide = 0;
    private bool _diceLocked = true;
    



    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        DiceState(false);
        
    }

    public void DiceThrow()
    {
        //print("Dice throw");
        StopCoroutine(DiceLogicStart());
        StartCoroutine(DiceLogicStart());
    }
    
    public void DiceState(bool True_False)
    {
        if (True_False)
        {
            DiceStateParticleSystem.Play();
            _diceLocked = false;
        }
        else
        {
            DiceStateParticleSystem.Stop();  
            _diceLocked = true;
        }
        
    }
    
    private IEnumerator DiceLogicStart()
    {
        yield return new WaitForSeconds(0.05f);
        //print("Dice velocity was at " + _rigidbody.velocity.magnitude);
        if (_rigidbody.velocity.magnitude >= requiredVelocity)
        {
            //print("TROW IS HIGH");
            yield return new WaitWhile(() => _rigidbody.velocity.magnitude >= 0.001);
            //print("DICE STOP");
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
        //print(_currentSide);
        if (!_diceLocked)
        {
            DiceState(false);
            GameboardSpacesManager.UpdateCurrentTargetSpacesPosition(_currentSide);
        }
        
    }

    
}
