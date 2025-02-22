using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyPointList
{

    [Tooltip("[Optional]")]
    public String ListName;
    public Vector3[] keyPointList;

    public Vector3[] GetVector3List()
    {
        return keyPointList;
    }

    public Vector3 GetVector3(int index)
    {
        return keyPointList[index];
    }

    public string GetListName()
    {
        return ListName;
    }
}
