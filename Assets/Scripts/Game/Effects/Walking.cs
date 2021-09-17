using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private Rigidbody2D _body;


    private float _airTime = 0;
    private bool _inAir = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inAir = false;

        var particleEmission = _particles.emission;
        _particles.startSize = _body.velocity.magnitude * 0.01f;

        particleEmission.rateOverTime = _airTime * 50;

        _particles.Play();
        _airTime = 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _inAir = true;
    }

    private void Update()
    {
        if (_inAir)
        {
            _airTime += Time.deltaTime;
        }
    }
}
