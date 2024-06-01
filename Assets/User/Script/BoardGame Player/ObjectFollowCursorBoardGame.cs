using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowCursorBoardGame : MonoBehaviour
{
    [SerializeField] private float extraYPosition = 0.1f;
    
    private MousePositionAndObjectDetection _mousePosition;
    private Rigidbody _rigidbody;
    private Vector3 _newposition;
    
    // Start is called before the first frame update

    private void Awake()
    {
        _mousePosition = GameObject.FindGameObjectWithTag("BoardGame Player")
            .GetComponent<MousePositionAndObjectDetection>();
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void FollowCursor()
    {
        _newposition = _mousePosition.MousePositionOnWorld() + new Vector3(0,extraYPosition, 0);
        transform.position = _newposition;
        //_rigidbody.position = _newposition;
        //_rigidbody.Move(_newposition, transform.rotation);
        _rigidbody.Sleep();
    }
    
}
