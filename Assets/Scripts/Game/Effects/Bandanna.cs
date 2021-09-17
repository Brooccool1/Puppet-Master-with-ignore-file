using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandanna : MonoBehaviour
{
    private LineRenderer _line;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _line.SetPosition(0, GetComponentInParent<Transform>().position);
        _line.SetPosition(1, GetComponentInParent<Transform>().position + new Vector3(0, 100));
    }
}
