using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Vector3 _defaultPosition;
    [SerializeField] private Vector3 _positionWithPerks;

    public void MoveTo(CameraPosition position)
    {
        switch (position)
        {
            case CameraPosition.Default:
                transform.position = _defaultPosition;    
                break;
            case CameraPosition.WithPerks:
                transform.position = _positionWithPerks;
                break;
        }
    }
}

public enum CameraPosition
{
    Default,
    WithPerks
}
