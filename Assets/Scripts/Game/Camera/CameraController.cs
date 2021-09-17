using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float _speed = 0.05f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float _currentSpeed = target.GetComponent<Rigidbody2D>().velocity.magnitude;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.position.x, _speed), Mathf.Lerp(transform.position.y, target.position.y, _speed), -15);

        if (_currentSpeed > 20)
        {
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, target.GetComponent<Rigidbody2D>().velocity.magnitude * 4, _speed * 0.05f);
        }
        else
        {
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 10, _speed * 0.1f);
        }
    }
}
