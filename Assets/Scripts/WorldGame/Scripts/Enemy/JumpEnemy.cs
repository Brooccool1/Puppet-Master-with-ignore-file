using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private Rigidbody2D _body;
    public Transform _target;
    [SerializeField] private float _areaVisual = 20;

    [SerializeField] private BoxCollider2D _groundCollider;

    [SerializeField] private HingeJoint2D _head;
    [SerializeField] private List<LimbController> _limbs;

    private bool _dirRight = true;
    private bool _jumping = false;
    private bool _jumped = true;

    private float _prepareTimer = 0;
    private float _prepareDelay = 2;

    private float _statechange = 0;
    private float _timeForChange = 0.5f;

    private enum _state
    {
        Idle,
        Jump
    }

    private _state _currentState = _state.Idle;

    void Start()
    {
        Collider2D[] colliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int o = i + 1; o < colliders.Length; o++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[o]);
                if (colliders[i].GetComponent<Rigidbody2D>())
                {
                    colliders[i].GetComponent<Rigidbody2D>().mass = _head.transform.lossyScale.y;
                }
            }
        }
    }

    private void _limbCheck()
    {
        for (int i = 0; i < _limbs.Count; i++)
        {
            if (!_limbs[i].GetComponent<HingeJoint2D>())
            {
                _limbs[i].enabled = false;
            }
        }
    }

    private void _dead()
    {
        if (!_head)
        {
            for (int i = 0; i < _limbs.Count; i++)
            {
                _limbs[i].enabled = false;
            }
            Destroy(gameObject, 2f);
        }
    }

    private void _stateChecker()
    {
        float distance = _target.position.x - _body.transform.position.x;

        if (distance < 0)
        {
            _dirRight = false;
        }
        else
        {
            _dirRight = true;
        }

        if (distance < -_areaVisual || distance > _areaVisual)
        {
            _currentState = _state.Idle;
        }
        else
        {
            if (!_jumping && _jumped && _statechange <= 0)
            {
                _currentState = _state.Jump;
                _prepareTimer = _prepareDelay;
                _jumped = false;
            }
        }
    }

    private void _groundCollision()
    {
        if (_groundCollider.IsTouchingLayers())
        {
            if (_jumping && _prepareTimer < -1)
            {
                _statechange = _timeForChange;
                _jumping = false;
                _jumped = true;
            }
        }
    }

    private void _controller()
    {
        if(_currentState == _state.Jump)
        {
            if (_prepareTimer <= 0 && !_jumped)
            {
                _jumping = true;
                _anim.Play("Jump");

                if (_dirRight)
                {
                    _body.AddForce(Vector2.right * _enemySpeed * Time.deltaTime);
                }
                else if (!_dirRight)
                {
                    _body.AddForce(Vector2.left * _enemySpeed * Time.deltaTime);
                }
            }
            else if(_statechange <= 0)
            {
                _anim.Play("Prepare");
            }
        }
    }

    void Update()
    {
        _groundCollision();
        if (_target)
        {

            float distance = _target.position.x - _body.transform.position.x;

            if (distance < _areaVisual && distance > -_areaVisual)
            {
                _controller();
                _stateChecker();
            }
            _limbCheck();
            _dead();

            _prepareTimer -= Time.deltaTime;
            _statechange -= Time.deltaTime;
        }
    }
}
