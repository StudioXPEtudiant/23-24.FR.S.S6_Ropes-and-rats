using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PickUp : MonoBehaviour
{
    [SerializeField] private float objectSpeed = 2;
    [FormerlySerializedAs("_pickUpDistence")] [SerializeField] private float lockMagnetudeDistance = 0.2f;
    [SerializeField] private float throwForce = 1.5f;
    [SerializeField] private float maxthrowForce = 10f;
    [SerializeField] private float rotationMultiplier = 1f;

    [SerializeField] private float2 minMaxHandDistanceFromPlayer = new float2(0.7f,1.3f);


    [FormerlySerializedAs("_onThrowDoRotate")] [SerializeField] private bool onThrowDoRotate;
    
    private GameObject _playerHand;
    private Rigidbody _rigidbody;
    private bool _isHold;
    private bool _wasHold;
    private Vector3 _lastPosition = Vector3.zero;
    private Vector3 _objVelocity;
    private PlayerInputController _playerInput;
    private Transform _hand;
    private Transform _arms;

    private void Awake()
    {
        _playerHand = GameObject.FindGameObjectWithTag("Player Hand");
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInputController>();
    }

    private void LateUpdate()
    {
        if (_isHold)
        {
            _isHold = false;
            _wasHold = true;
        }
        else if (_wasHold)
        {
            _wasHold = false;
            OnHoldEnd();
        }
        
    }
    
    

    public void OnHold()
    {
        _isHold = true;
        transform.parent = _playerHand.transform;
        _hand = transform.parent;
        _arms = transform.parent.parent;
        Vector3 direction = _playerHand.transform.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.blue);
        
        if (direction.magnitude > lockMagnetudeDistance)
        {
            //_rigidbody.constraints = UnityEngine.RigidbodyConstraints.FreezeRotation;
            _rigidbody.useGravity = false;
            _rigidbody.AddForce(direction * objectSpeed + direction.normalized);
            //_rigidbody.Sleep();
        }
        else
        {
            _rigidbody.Sleep();
            //_rigidbody.constraints = UnityEngine.RigidbodyConstraints.None;
            _rigidbody.useGravity = false;
            if (Input.GetKey(_playerInput.GetKeyUpAltAction()) && !Input.GetKey(_playerInput.GetKeyDownAltAction()))
            {
                _arms.Rotate(Vector3.right, Space.Self);
                //print("up");
            }
            else if (Input.GetKey(_playerInput.GetKeyDownAltAction()) && !Input.GetKey(_playerInput.GetKeyUpAltAction()))
            {
                
                _arms.Rotate(Vector3.left, Space.Self);
                //_rigidbody.AddRelativeTorque(Vector3.right, ForceMode.Force);
                //print("down");
            }
            if (Input.GetKey(_playerInput.GetKeyLeftAltAction()) && !Input.GetKey(_playerInput.GetKeyRightAltAction()))
            {
                _hand.Rotate(Vector3.up, Space.World);
                //_rigidbody.AddRelativeTorque(Vector3.up, ForceMode.Force);
                //print("left");
            }
            else if (Input.GetKey(_playerInput.GetKeyRightAltAction()) && !Input.GetKey(_playerInput.GetKeyLeftAltAction()))
            {
                _hand.Rotate(Vector3.down, Space.World);
                //_rigidbody.AddRelativeTorque(Vector3.down, ForceMode.Force);
                //print("right");
            }

        }

        if (transform.position - _lastPosition != Vector3.zero)
        {
            //print(transform.position);
            //print(_lastPosition);
            _objVelocity = (transform.position - _lastPosition);
            //print(_obj_velocity);
        }
        Debug.DrawRay(transform.position, _objVelocity, Color.green);
        _lastPosition = transform.position;
    }

    private void OnHoldEnd()
    {
        //print("OnHoldEnd");
        _rigidbody.WakeUp();
        _rigidbody.useGravity = true;
        _rigidbody.constraints = UnityEngine.RigidbodyConstraints.None;
        transform.parent = null;
        _arms.localRotation = quaternion.Euler(Vector3.zero);
        _hand.localRotation = quaternion.Euler(Vector3.zero);
        //print(_obj_velocity);
        //print(transform.position);
        //print(_lastPosition);
        _rigidbody.velocity = Vector3.ClampMagnitude(_objVelocity * (_objVelocity.magnitude * 100 * throwForce), maxthrowForce);
        //print(Vector3.ClampMagnitude(_obj_velocity * (_obj_velocity.magnitude * 100 * _throwForce), _MaxthrowForce));
        Debug.DrawRay(transform.position, _objVelocity, Color.green, 3);
        if (onThrowDoRotate)
        {
            Vector3 torque = new Vector3(_objVelocity.z, _objVelocity.y, -_objVelocity.x);
            _rigidbody.AddTorque(torque * rotationMultiplier, ForceMode.Impulse);
        }
    }
    
    
}
