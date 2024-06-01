using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionAndObjectDetection : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _rayColliderMask;
    [SerializeField] private LayerMask _rayObjectDetectionMask;
    [SerializeField] private float _rayObjectLenght = 1.8f;

    private PlayerInputController _playerInputController;
    private Vector3 _mousePositionOnScreen;
    private Vector3 _mousePositionOnWorld;
    private GameObject _currentGameObject;
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
        _mousePositionOnScreen = Input.mousePosition;
        Ray mouseColliderRay = _camera.ScreenPointToRay(_mousePositionOnScreen);
        if (Physics.Raycast(mouseColliderRay, out RaycastHit raycastColliderHit,10, _rayColliderMask))
        {
            //Debug.DrawLine(mouseColliderRay.origin, raycastColliderHit.point,Color.cyan);
            _mousePositionOnWorld = raycastColliderHit.point;
            
        }
       
              
        Ray mouseObjectRay = _camera.ScreenPointToRay(_mousePositionOnScreen);
        if (Physics.Raycast(mouseObjectRay,out RaycastHit raycastObjectHit,_rayObjectLenght,_rayObjectDetectionMask)) 
        {
            Debug.DrawLine(mouseObjectRay.origin, raycastObjectHit.point, Color.yellow);
            if (!_playerInputController.MainKeyHold())
            {
                _currentGameObject = raycastObjectHit.collider.gameObject;
            }
            _raycasthit = true;
            
        }
        else
        {
            Debug.DrawRay(mouseObjectRay.origin, mouseObjectRay.direction * _rayObjectLenght, Color.white);
            _raycasthit = false;
        }
        
        if (_ignoreInput && Input.GetKeyUp(_playerInputController.GetKeyMainAction()))
        {
            _ignoreInput = false;
            return;
        }
        
        if (Input.GetKeyDown(_playerInputController.GetKeyMainAction()) && !_raycasthit || _ignoreInput)
        {
            Debug.DrawLine(mouseObjectRay.origin, raycastObjectHit.point, Color.yellow);
            _ignoreInput = true;
            return;
        }
        
        if(_currentGameObject == null)return;
        
        if (_currentGameObject.GetComponent<InteractiveGameBoard>() == null)
        {
            //print("Object have no Interactive Component");
            return;
        }
        
        if (_playerInputController.MainKeyHold())
        {
            _currentGameObject.GetComponent<InteractiveGameBoard>().OnMainActHold();
            
            //print("key is Hold");
        }
        else if (Input.GetKeyUp(_playerInputController.GetKeyMainAction()) && _raycasthit && !_playerInputController.IsMainPressedTimeAHold() )
        {
            //print(_raycasthit);
            _currentGameObject.GetComponent<InteractiveGameBoard>().OnMainActPresse();
            //print("key is Press");
        }
    }
    private void OnEnable()
    {
        //print("restart");
        _currentGameObject = null;
        _raycasthit = false;
    }

    public Vector3 MousePositionOnWorld()
    {
        return _mousePositionOnWorld;
    }
}
