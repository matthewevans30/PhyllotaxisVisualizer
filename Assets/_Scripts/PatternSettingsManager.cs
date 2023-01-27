using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSettingsManager : MonoBehaviour
{
    public PhyllotaxisTrailOnAudio _trailScript;

    public void ButtonPreset51() {
        _trailScript.Degree = 51f;
        _trailScript.ResetPattern();
    }

    public void ButtonPreset137() {
        _trailScript.Degree = 137.5f;
        _trailScript.ResetPattern();
    }

    public void DegreeUpdate(float newValue) {
        _trailScript.Degree = newValue;
        _trailScript.ResetPattern();
        //update pattern display
    }

    public void ScaleUpdate(float newValue) {
        _trailScript.Scale = newValue;
        _trailScript.ResetPattern();
        //update pattern display
    }

    public void StepSizeUpdate(float newValue) {
        _trailScript.StepSize = (int)newValue;
        _trailScript.ResetPattern();
        //update pattern display
    }

    public void MaxIterationUpdate(float newValue) {
        _trailScript.MaxIteration = (int)newValue;
        _trailScript.ResetPattern();
        //update pattern display
    }

    public void LerpUpdate(bool newValue) {
        _trailScript.UseLerping = newValue;
        _trailScript.ResetPattern();
    }

    public void MinSpeedUpdate(float newValue) {
        _trailScript._lerpPosSpeedMinMax.x = newValue;
        _trailScript.ResetPattern();
        //update pattern display
    }

    public void MaxSpeedUpdate(float newValue) {
        _trailScript._lerpPosSpeedMinMax.y = newValue;
        _trailScript.ResetPattern();
        //update pattern display
    }
}
