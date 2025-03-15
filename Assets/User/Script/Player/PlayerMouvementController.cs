using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]

public class PlayerMouvementController : MonoBehaviour
{
    [SerializeField]
    private GameObject head;

    [SerializeField] [Min(0)]
    private float playerSpeed = 5f;
    [SerializeField]
    [Min(0)]
    private float cameraSensibility = 2f;

    [SerializeField]
    private float playerHeight = 2f;

    [SerializeField] private float playerCrouchHeight = 1f;

    [FormerlySerializedAs("Inertie")]
    [Range(0,0.9f)]
    [SerializeField] private float NoInertie = 0.025f;

    [SerializeField] private int OutOfBoundDistanceY = 15;

    private Vector3 _movementX;
    private Vector3 _movementZ;
    private float _cameraY;
    private float _playerHeightdifference;
    private CharacterController _characterController;
    private PlayerInputController _playerInput;

    private Vector3 _initialePosition;

    // Start is called before the first frame update
    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInputController>();
        _playerHeightdifference = (playerHeight - playerCrouchHeight) / 2;
        _playerInput.noInertie = NoInertie;

        _initialePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _movementX = transform.right * _playerInput.GetleftRightAxis();
        _movementZ = transform.forward * _playerInput.GetUpDownAxis();

        Vector3 move = (_movementX + _movementZ);
        _characterController.Move(move * (playerSpeed * Time.deltaTime));
        _characterController.Move(new Vector3(0,-0.005f,0));




        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");
        
        _cameraY -= inputY * cameraSensibility;
        _cameraY = Mathf.Clamp(_cameraY, -80f, 80f);
        transform.Rotate(0, inputX * cameraSensibility, 0);
        head.transform.localRotation = Quaternion.Euler(_cameraY, 0, 0);

        if (Input.GetKeyDown(_playerInput.GetKeyCrouchAction()))
        {
            _characterController.height = playerCrouchHeight;
            _characterController.Move(new Vector3(0,-_playerHeightdifference,0));
        }
        else if (Input.GetKeyUp(_playerInput.GetKeyCrouchAction()))
        {
            _characterController.height = playerHeight;
            _characterController.Move(new Vector3(0,_playerHeightdifference,0));
        }

        if (transform.position.y < _initialePosition.y -OutOfBoundDistanceY)
        {
            _characterController.Move(Vector3.zero);
            transform.position = _initialePosition;
        }
    }
    
    
}
