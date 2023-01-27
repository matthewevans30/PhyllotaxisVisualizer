using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyllotaxisTrailOnAudio : MonoBehaviour
{

    public AudioAnalysis Analyzer;

    private Material _trailMat;
    public Color _trailColor;

    public float Degree, Scale;
    public float Number;
    public int StepSize, MaxIteration;
    public int NumberStart;

    //Lerping
    public bool UseLerping;
    private bool _isLerping;
    private Vector3 _startPosition, _endPosition;

    public int _currentIteration;
    private TrailRenderer _trailRenderer;

    private Vector2 _phyllotaxisPosition;

    public float _lerpPosTimer;
    public float _lerpPosSpeed;     //timer goes 0-1 at lerpPosSpeed
    public Vector2 _lerpPosSpeedMinMax;             //min max of lerpPosSpeed
    public AnimationCurve _lerpPosAnimCurve;        //specifies how number increases/decreases
    private bool _forward;              //keeps track of which direction we are going

    private float _trailLength;

    public bool Repeat, Invert;         //determines whether to repeat or invert when we hit maxIteration

    public bool InEditMode;

    //Scaling
    public bool UseScaleAnimation, UseScaleCurve;
    public Vector2 ScaleAnimMinMax;
    public AnimationCurve ScaleAnimCurve;
    public float ScaleAnimSpeed;
    public int ScaleBand;
    private float _scaleTimer, _currentScale;

    private Vector3 _origin;

    private void Awake()
    {
        _currentScale = Scale;
        _forward = true;
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailMat = new Material(_trailRenderer.material);
        //_trailMat.SetColor("_TintColor", _trailColor);
        _trailRenderer.material = _trailMat;
        Number = NumberStart;
        _origin = CalculatePhyllotaxis(Degree, _currentScale, NumberStart);
        transform.localPosition = _origin;

        if (UseLerping)
        {
            _isLerping = true;
            SetLerpPositions();
        }

        _trailLength = _trailRenderer.time;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Entering Edit Mode");
            StartEditMode();
        }

        if (InEditMode) {
            PatternEditMode();
            return;
        }

        if (UseScaleAnimation)
        {
            if (UseScaleCurve)
            {
                _scaleTimer += (ScaleAnimSpeed * Analyzer.bandBuffer[Analyzer.FocusBand]) * Time.deltaTime;
                if (_scaleTimer >= 1)
                {
                    _scaleTimer -= 1;
                }
                _currentScale = Mathf.Lerp(ScaleAnimMinMax.x, ScaleAnimMinMax.y, ScaleAnimCurve.Evaluate(_scaleTimer));
            }
            else
            {
                _currentScale = Mathf.Lerp(ScaleAnimMinMax.x, ScaleAnimMinMax.y, Analyzer.bandBuffer[Analyzer.FocusBand]);
            }
        }

        if (UseLerping)
        {
            if (_isLerping)
            {
                _lerpPosSpeed = Mathf.Lerp(_lerpPosSpeedMinMax.x, _lerpPosSpeedMinMax.y,
                    _lerpPosAnimCurve.Evaluate(Analyzer.bandBuffer[Analyzer.FocusBand]));
                _lerpPosTimer += Time.deltaTime * _lerpPosSpeed;
                transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, Mathf.Clamp01(_lerpPosTimer));

                if (_lerpPosTimer >= 1)
                {
                    _lerpPosTimer = 0;
                    if (_forward)
                    {
                        Number += StepSize;
                        _currentIteration++;
                    }
                    else
                    {
                        Number -= StepSize;
                        _currentIteration--;
                    }

                    if (_currentIteration > 0 && _currentIteration < MaxIteration)
                    {
                        SetLerpPositions();
                    }
                    else
                    {   //current iteration has hit 0 or maxIteration
                        if (Repeat)
                        {
                            if (Invert)
                            {
                                _forward = !_forward;
                                SetLerpPositions();
                            }
                            else
                            {
                                
                                ResetPattern();
                                //_trailRenderer.Clear();
                            }
                        }
                        else
                        {
                            _isLerping = false;
                        }
                    }

                }
            }
        }
        if (!UseLerping)
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

    void SetLerpPositions()
    {
        _phyllotaxisPosition = CalculatePhyllotaxis(Degree, Scale, Number);
        _startPosition = this.transform.localPosition;
        _endPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
    }

    public void ResetPattern()
    {
        Number = NumberStart;
        _currentIteration = 0;
        _phyllotaxisPosition = CalculatePhyllotaxis(Degree, Scale, Number);
        transform.localPosition = _origin;
        _startPosition = transform.localPosition;
        _endPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
        _trailRenderer.Clear();
    }

    public void StartEditMode() {
        ResetPattern();
        _trailRenderer.time = 10000;
        _currentIteration = 0;
        Number = NumberStart;
        InEditMode = true;
    }

    void PatternEditMode() {
        if(_currentIteration < MaxIteration) {
            _phyllotaxisPosition = CalculatePhyllotaxis(Degree, Scale, Number);
            transform.localPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
            Number += StepSize;
            _currentIteration++;
        }
        
    }


    

}

