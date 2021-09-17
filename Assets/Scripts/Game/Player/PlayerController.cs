using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private HingeJoint2D _head;
    [SerializeField] private List<LimbController> _limbs;

    private bool _dirRight = true;

    void Start()
    {
        Collider2D[] colliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int o = i + 1; o < colliders.Length; o++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[o]);
            }
        }
    }

    private void _attacks()
    {
        if (_dirRight)
        {
            if (Input.GetKey(KeyCode.K))
            {
                _anim.Play("RightKick");
            }
            if (Input.GetKey(KeyCode.J))
            {
                _anim.Play("RightPunch");
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _anim.Play("ThrowRight");
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.K))
            {
                _anim.Play("LeftKick");
            }
            if (Input.GetKey(KeyCode.J))
            {
                _anim.Play("LeftPunch");
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _anim.Play("ThrowLeft");
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
        }
    }

    private void _restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }

    void Update()
    {
        _dead();
        _restart();


        if (Input.GetKey(KeyCode.Space))
        {
            _anim.Play("Jump");
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _anim.Play("Walking");
            _body.AddForce(Vector2.right * _playerSpeed * Time.deltaTime);
            _dirRight = true;
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            _anim.Play("Walking");
            _body.AddForce(Vector2.left * _playerSpeed * Time.deltaTime);
            _dirRight = false;
        }
        else if(Input.GetAxisRaw("Vertical") < 0)
        {
            _anim.Play("Split");
        }
        else
        {
            _anim.Play("Base");
        }

         _attacks();

        // Idle Layer
        if (_dirRight)
        {
            _anim.Play("IdleRight");
        }
        else
        {
            _anim.Play("IdleLeft");
        }
    }
}
