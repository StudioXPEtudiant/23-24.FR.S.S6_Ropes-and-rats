using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DetectAndAct : MonoBehaviour
{
    [SerializeField] private GameObject playerHead;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float detectDistance = 1.5f;

    private GameObject _currentGameObject;
    private PlayerInputController _playerInputController;
    private bool _raycasthit;
    private bool _ignoreInput;



    // Start is called before the first frame update
    void Start()
    {
        _playerInputController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInputController>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(playerHead.transform.position, playerHead.transform.TransformDirection(Vector3.forward), out raycastHit,detectDistance, layerMask))
        {
            if (!_playerInputController.MainKeyHold())
            {
                _currentGameObject = raycastHit.transform.gameObject;
            }
            Debug.DrawRay(playerHead.transform.position, playerHead.transform.TransformDirection(Vector3.forward) * raycastHit.distance, Color.yellow);
            //Debug.Log("Did Hit " + _currentGameObject); 
            _raycasthit = true;
        }
        else
        {
            Debug.DrawRay(playerHead.transform.position, playerHead.transform.TransformDirection(Vector3.forward) * detectDistance, Color.white);
            //Debug.Log("Did not Hit");
            _raycasthit = false;
        }
        
        if (_ignoreInput && Input.GetKeyUp(_playerInputController.GetKeyMainAction()))
        {
            _ignoreInput = false;
            return;
        }
        
        if (Input.GetKeyDown(_playerInputController.GetKeyMainAction()) && !_raycasthit || _ignoreInput)
        {
            Debug.DrawRay(playerHead.transform.position, playerHead.transform.TransformDirection(Vector3.forward) * detectDistance, Color.red);
            _ignoreInput = true;
            return;
        }
        
        if(_currentGameObject == null)return;
        
        if (_currentGameObject.GetComponent<Interactive>() == null)
        {
            //print("Object have no Interactive Component");
            return;
        }
        
        if (_playerInputController.MainKeyHold())
        {
            _currentGameObject.GetComponent<Interactive>().OnMainActHold();

            //print("key is Hold");
        }
        else if (Input.GetKeyUp(_playerInputController.GetKeyMainAction()) && _raycasthit && !_playerInputController.IsMainPressedTimeAHold())
        {
            //print(_raycasthit);
            _currentGameObject.GetComponent<Interactive>().OnMainActPresse();
            //print("key is Press");
        }
        else if (Input.GetKeyUp(_playerInputController.GetKeyMainAction()) && _playerInputController.IsMainPressedTimeAHold())
        {
            _currentGameObject.GetComponent<Interactive>().OnMainActRelease();
            //print("key is Release");
        }

    }

    private void OnEnable()
    {
        //print("restart");
        _currentGameObject = null;
        _raycasthit = false;
    }
}
