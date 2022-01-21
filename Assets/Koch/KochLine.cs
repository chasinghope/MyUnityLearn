using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class KochLine : KochGenerator
{
    LineRenderer _lineRenderer;
    [Range(0f, 1f)]
    public float _lerpAmount;
    Vector3[] _lerpPosition;
    public float _generateMultiplier;
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.positionCount = _position.Length;
        _lineRenderer.SetPositions(_position);
    }

    private void Update()
    {
        if (_generationCount != 0)
        {
            for (int i = 0; i < _position.Length; i++)
            {
                _lerpPosition[i] = Vector3.Lerp(_position[i], _targetPosition[i], _lerpAmount);
            }
            _lineRenderer.SetPositions(_lerpPosition);
        }

        if (Input.GetKeyUp(KeyCode.O))
        {
            KochGenerate(_targetPosition, true, _generateMultiplier);
            _lerpPosition = new Vector3[_position.Length];
            _lineRenderer.positionCount = _position.Length;
            _lineRenderer.SetPositions(_position);
            _lerpAmount = 0;
        }


        if (Input.GetKeyUp(KeyCode.I))
        {
            KochGenerate(_targetPosition, false, _generateMultiplier);
            _lerpPosition = new Vector3[_position.Length];
            _lineRenderer.positionCount = _position.Length;
            _lineRenderer.SetPositions(_position);
            _lerpAmount = 0;
        }
    }
}