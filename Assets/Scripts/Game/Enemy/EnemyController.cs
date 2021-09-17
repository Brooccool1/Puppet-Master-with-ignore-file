using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private Rigidbody2D _body;
    public Transform _target;
    [SerializeField] private float _area = 5;

    [SerializeField] private HingeJoint2D _head;
    [SerializeField] private List<LimbController> _limbs;

    private bool _dirRight = true;

    private float _timer = 0;
    private float _maxTime = 1;

    private enum _state
    {
        walk,
        jump,
        punch,
        kick
    }

    private _state _currentState = _state.walk;

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

    private void _stateChecker()
    {
        float distance = _target.position.x -_body.transform.position.x;

        if(distance < 0)
        {
            _dirRight = false;
            _anim.Play("IdleLeft");
        }
        else
        {
            _dirRight = true;
            _anim.Play("IdleRight");
        }

        if(distance < -_area || distance > _area)
        {
            _currentState = _state.walk;
        }
        else
        {
            if (_timer <= 0)
            {
                int state = Random.Range(0, 3);

                switch (state)
                {
                    case 0:
                        _currentState = _state.punch;
                        break;

                    case 1:
                        _currentState = _state.kick;
                        break;

                    case 2:
                        _currentState = _state.jump;
                        break;

                    default:
                        break;
                }


                _timer = _maxTime;
            }
        }
    }

    private void _controller()
    {
        if (_dirRight)
        {
            switch (_currentState)
            {
                case _state.walk:
                    _anim.Play("Walking");
                    _body.AddForce(Vector2.right * _enemySpeed * Time.deltaTime);
                    _dirRight = true;
                    break;

                case _state.jump:
                    _anim.Play("Jump");
                    break;

                case _state.punch:
                    _anim.Play("RightPunch");
                    break;

                case _state.kick:
                    _anim.Play("RightKick");
                    break;

                default:
                    break;
            }
        }
        else
        {
            switch (_currentState)
            {
                case _state.walk:
                    _anim.Play("Walking");
                    _body.AddForce(Vector2.left * _enemySpeed * Time.deltaTime);
                    _dirRight = true;
                    break;

                case _state.jump:
                    _anim.Play("Jump");
                    break;

                case _state.punch:
                    _anim.Play("LeftPunch");
                    break;

                case _state.kick:
                    _anim.Play("LeftKick");
                    break;

                default:
                    break;
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

    void Update()
    {
        if (_target)
        {
            _timer -= Time.deltaTime;
            _stateChecker();
            _controller();
            _limbCheck();
            _dead();
        }
    }
}
