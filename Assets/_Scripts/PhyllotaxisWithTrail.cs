using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyllotaxisWithTrail : MonoBehaviour
{

    public float Degree, Scale, Number;  
    public int StepSize, MaxIteration;
    public int NumberStart;

    //Lerping
    public bool UseLerping;
    public float IntervalLerp;
    private bool _isLerping;
    private Vector3 _startPosition, _endPosition;
    private float _timeStartedLerp;

    private int _currentIteration;
    private TrailRenderer _trailRenderer;

    private Vector2 _phyllotaxisPosition;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        Number = NumberStart;
        transform.position = CalculatePhyllotaxis(Degree, Scale, NumberStart);

        if (UseLerping)
        {
            StartLerping();
        }
    }

    private void FixedUpdate()
    {
        if (UseLerping)
        {
            if (_isLerping)
            {
                bool complete = false;
                float timeSinceStarted = Time.time - _timeStartedLerp;
                float percentageComplete = timeSinceStarted / IntervalLerp;
                if(percentageComplete >= 1f)
                {
                    complete = true;
                    percentageComplete = 1f;
                }

                transform.position = Vector3.Lerp(_startPosition, _endPosition, percentageComplete);
                if(complete)
                {
                    transform.localPosition = _endPosition;
                    Number += StepSize;
                    _currentIteration++;
                    if(_currentIteration <= MaxIteration)
                    {
                        StartLerping();
                    }
                    else
                    {
                        _isLerping = false;
                    }
                }
            }
        }
        else
        {
            _phyllotaxisPosition = CalculatePhyllotaxis(Degree, Scale, Number);
            transform.localPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
            Number += StepSize;
            _currentIteration++;
        } 
    }

    //phyllotaxis formula: 
    //angle = n * degree;
    //radius = scale * sqrt(n) 
    //then convert from polar to cartesian coordinates
    private Vector2 CalculatePhyllotaxis(float degree, float scale, float count)
    {
        double angle = count * (degree * Mathf.Deg2Rad);

        float radius = scale * Mathf.Sqrt(count);

        float x = radius * (float)System.Math.Cos(angle);
        float y = radius * (float)System.Math.Sin(angle);

        Vector2 cartesianPoint = new Vector2(x, y);

        return cartesianPoint;
    }

    void StartLerping()
    {
        _isLerping = true;
        _timeStartedLerp = Time.time;
        _phyllotaxisPosition = CalculatePhyllotaxis(Degree, Scale, Number);
        _startPosition = this.transform.position;
        _endPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
    }

}
