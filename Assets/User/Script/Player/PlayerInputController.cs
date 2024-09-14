using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInputController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode mainActionKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode secondaryActionKey = KeyCode.Mouse1;
    [SerializeField] private KeyCode crouchActionKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode leftActionKey = KeyCode.Q;
    [SerializeField] private KeyCode rightActionKey = KeyCode.D;
    [SerializeField] private KeyCode upActionKey = KeyCode.Z;
    [SerializeField] private KeyCode downActionKey = KeyCode.S;
    [SerializeField] private KeyCode leftAltActionKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode rightAltActionKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode upAltActionKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode downAltActionKey = KeyCode.DownArrow;

    [Header("Hold")] [SerializeField] private float minimumHeldDuration = 0.25f;

    private float _pressedTime;
    private bool _mainIsHold;
    private float _leftRightAxis;
    private float _upDownAxis;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftActionKey) && !Input.GetKey(rightActionKey))
        {
            _leftRightAxis += -0.01f;
            _leftRightAxis = Mathf.Clamp(_leftRightAxis, -1, 1);
        }
        else if (Input.GetKey(rightActionKey) && !Input.GetKey(leftActionKey))
        {
            _leftRightAxis += 0.01f;
            _leftRightAxis = Mathf.Clamp(_leftRightAxis, -1, 1);
        }
        else if (_leftRightAxis > 0.01)
        {
            _leftRightAxis += -0.01f;
        }
        else if (_leftRightAxis < -0.01)
        {
            _leftRightAxis += 0.01f;
        }
        else
        {
            _leftRightAxis = 0;
        }
        
        if (Input.GetKey(upActionKey) && !Input.GetKey(downActionKey))
        {
            _upDownAxis += 0.01f;
            _upDownAxis = Mathf.Clamp(_upDownAxis, -1, 1);
        }
        else if (Input.GetKey(downActionKey) && !Input.GetKey(upActionKey))
        {
            _upDownAxis += -0.01f;
            _upDownAxis = Mathf.Clamp(_upDownAxis, -1, 1);
        }
        else if (_upDownAxis > 0.01)
        {
            _upDownAxis += -0.01f;
        }
        else if (_upDownAxis < -0.01)
        {
            _upDownAxis += 0.01f;
        }
        else
        {
            _upDownAxis = 0;
        }


        if (Input.GetKeyDown(mainActionKey))
        {
            _pressedTime = Time.timeSinceLevelLoad;
            _mainIsHold = false;
        }
        else if (Input.GetKeyUp(mainActionKey))
        {
            _mainIsHold = false;
        }
        if (Input.GetKey(mainActionKey))
        {
            if (Time.timeSinceLevelLoad - _pressedTime > minimumHeldDuration)
            {
                _mainIsHold = true;
                //print("button is Hold");
            }
        }
    }

    public bool MainKeyHold()
    {
        return _mainIsHold;
    }

    public bool IsMainPressedTimeAHold()
    {
        return Time.timeSinceLevelLoad - _pressedTime > minimumHeldDuration;
    }

    public float GetleftRightAxis()
    {
        return _leftRightAxis;
    }
    public float GetUpDownAxis()
    {
        return _upDownAxis;
    }

    public KeyCode GetKeyMainAction()
    {
        return mainActionKey;
    }

    public KeyCode GetKeysecondaryAction()
    {
        return secondaryActionKey;
    }

    public KeyCode GetKeyCrouchAction()
    {
        return crouchActionKey;
    }

    public KeyCode GetKeyLeftAction()
    {
        return leftActionKey;
    }
    
    public KeyCode GetKeyRightAction()
    {
        return rightActionKey;
    }
    
    public KeyCode GetKeyUpAction()
    {
        return upActionKey;
    }
    
    public KeyCode GetKeyDownAction()
    {
        return downActionKey;
    }
    
    public KeyCode GetKeyLeftAltAction()
    {
        return leftAltActionKey;
    }
    
    public KeyCode GetKeyRightAltAction()
    {
        return rightAltActionKey;
    }
    
    public KeyCode GetKeyUpAltAction()
    {
        return upAltActionKey;
    }
    
    public KeyCode GetKeyDownAltAction()
    {
        return downAltActionKey;
    }
    
}
