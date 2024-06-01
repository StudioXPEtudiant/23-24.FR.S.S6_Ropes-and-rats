using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoardGamePickUp : MonoBehaviour
{
    [SerializeField] private float extraYPosition = 0.1f;
    [SerializeField] private float objectSpeed = 2;
    [SerializeField] private float pickUpDistence = 0.2f;
    [SerializeField] private float throwForce = 1f;
    [SerializeField] private float MaxthrowForce = 10f;
    [SerializeField] private bool onThrowDoRotate;
    
    private MousePositionAndObjectDetection _mousePosition;
    private Rigidbody _rigidbody;
    private Vector3 _newMouseposition;
    
    private bool _isHold;
    private bool _wasHold;
    private Vector3 _lastPosition = Vector3.zero;
    private Vector3 _obj_velocity;
    
    // Start is called before the first frame update
    private void Awake()
        {
            _mousePosition = GameObject.FindGameObjectWithTag("BoardGame Player")
                .GetComponent<MousePositionAndObjectDetection>();
            _rigidbody = GetComponent<Rigidbody>();
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
        _newMouseposition = _mousePosition.MousePositionOnWorld() + new Vector3(0,extraYPosition, 0);
        Vector3 direction = _newMouseposition - transform.position;
        Debug.DrawRay(transform.position, direction, Color.blue);
        _rigidbody.useGravity = false;
        if (direction.magnitude > pickUpDistence)
        {
            _rigidbody.constraints = UnityEngine.RigidbodyConstraints.FreezeRotation;
            _rigidbody.Move(transform.position + (direction.normalized / 10 * direction.magnitude * objectSpeed), transform.rotation);
            //_rigidbody.Sleep();
        }
        else
        {
            _rigidbody.constraints = UnityEngine.RigidbodyConstraints.FreezeRotation;
            _rigidbody.Move(_newMouseposition, transform.rotation);
        }

        if (transform.position - _lastPosition != Vector3.zero)
        {
            //print(transform.position);
            //print(_lastPosition);
            _obj_velocity = (transform.position - _lastPosition);
            //print(_obj_velocity);
        }
        Debug.DrawRay(transform.position, _obj_velocity, Color.green);
        _lastPosition = transform.position;
    }
    
    
    
    private void OnHoldEnd()
    {
        //print("OnHoldEnd");
        _rigidbody.WakeUp();
        _rigidbody.useGravity = true;
        _rigidbody.constraints = UnityEngine.RigidbodyConstraints.None;
        transform.parent = null;
        //print(_obj_velocity);
        //print(transform.position);
        //print(_lastPosition);
        _rigidbody.velocity = Vector3.ClampMagnitude(_obj_velocity * (_obj_velocity.magnitude * 100 * throwForce), MaxthrowForce);
        //print(Vector3.ClampMagnitude(_obj_velocity * (_obj_velocity.magnitude * 100 * throwForce), MaxthrowForce));
        Debug.DrawRay(transform.position, _obj_velocity, Color.green, 3);
        if (onThrowDoRotate)
        {
            Vector3 test = new Vector3(_obj_velocity.z, _obj_velocity.y, -_obj_velocity.x);
            _rigidbody.AddTorque(test, ForceMode.Impulse);
        }
        
    }
    
}
