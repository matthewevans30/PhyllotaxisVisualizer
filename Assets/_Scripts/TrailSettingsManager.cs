using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSettingsManager : MonoBehaviour
{
    [SerializeField] TrailRenderer _trail;
    public void LengthUpdate(float newValue)
    {
        _trail.time = newValue;
    }

    public void SizeUpdate(float newValue)
    {
        _trail.startWidth = newValue;
    }
}
