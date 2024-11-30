using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactive : MonoBehaviour
{
    [SerializeField] private UnityEvent eventOnMainActPresse;
    [SerializeField] private UnityEvent eventOnMainActHold;
    [SerializeField] private UnityEvent eventOnMainActRelease;

    public void OnMainActPresse()
    {
        //print("interaction Press On " + this.transform);
        eventOnMainActPresse.Invoke();
    }

    public void OnMainActHold()
    {
        //print("interaction Hold On "+ this.transform);
        eventOnMainActHold.Invoke();
    }
    
    public void OnMainActRelease()
    {
        //print("interaction Release "+ this.transform);
        eventOnMainActRelease.Invoke();
    }
}
