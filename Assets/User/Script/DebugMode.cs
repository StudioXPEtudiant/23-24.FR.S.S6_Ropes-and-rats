using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMode : MonoBehaviour
{
    private float _timeEscHold = 0;
    private float _pressedTime;
    private float _holdDur = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pressedTime = Time.timeSinceLevelLoad;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("time of hold = " + (Time.timeSinceLevelLoad - _pressedTime));
            if (Time.timeSinceLevelLoad - _pressedTime > _holdDur)
            {
                Debug.Log("QUITTING THE GAME");
                Application.Quit();
            }
        }
    }

}
