using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GamplaySwitch : MonoBehaviour
{
    [SerializeField] private UnityEvent onPlayer;
    [SerializeField] private UnityEvent onBoardGamePlayer;
    
    private GameObject _playerGameObject;
    private GameObject _boardGamePlayerGameObject;

    // Start is called before the first frame update
    void Awake()
    {
        _playerGameObject = GameObject.FindGameObjectWithTag("Player");
        _boardGamePlayerGameObject = GameObject.FindGameObjectWithTag("BoardGame Player");
    }

    private void Start()
    {
        _boardGamePlayerGameObject.SetActive(false);
    }


    public void SetGamplayToBoardGame(bool setToBoardGame)
    {
        if (setToBoardGame)
        {
            onBoardGamePlayer.Invoke();
            
            _boardGamePlayerGameObject.SetActive(true);
            _playerGameObject.SetActive(false);
        }
        else
        {
            onPlayer.Invoke();
            _boardGamePlayerGameObject.SetActive(false);    
            _playerGameObject.SetActive(true);
        }
    }

    [ContextMenu("setToMain")]
    private void SetToMain()
    {
        SetGamplayToBoardGame(false);
    }
    [ContextMenu("setToBoard")]
    private void SetToBoard()
    {
        SetGamplayToBoardGame(true);
    }
}
