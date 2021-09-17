using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbController : MonoBehaviour
{
    [SerializeField] private float _targetRotation;
    [SerializeField] private float _force;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        _rb.MoveRotation(Mathf.LerpAngle(_rb.rotation, _targetRotation,  _force * Time.fixedDeltaTime));
    }
}
