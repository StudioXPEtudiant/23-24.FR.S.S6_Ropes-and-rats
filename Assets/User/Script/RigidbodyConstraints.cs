using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyConstraints : MonoBehaviour
{

    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Constraints_FreezeAll()
    {
        _rigidbody.constraints = UnityEngine.RigidbodyConstraints.FreezeAll;
    }
    public void Constraints_None()
    {
        _rigidbody.constraints = UnityEngine.RigidbodyConstraints.None;
    }
}
