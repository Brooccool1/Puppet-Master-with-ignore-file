using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailWidth : MonoBehaviour
{
    private Rigidbody2D _body;
    private TrailRenderer _trail;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _trail = GetComponent<TrailRenderer>();

        _trail.endWidth = 0f;
        _trail.time = 0.5f;

        _trail.startColor = Color.black;
        _trail.endColor = Color.gray;
    }

    private void Update()
    {
        if (_body.velocity.magnitude > 10)
        {
            _trail.startWidth = _body.velocity.magnitude * 0.01f;
            _trail.emitting = true;
        }
        else
        {
            _trail.emitting = false;
        }
    }
}
