using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour
{
    [SerializeField] private LineRenderer[] _lines = new LineRenderer[5];

    private void Update()
    {
        _trailController(OptionsTemplate.doll);
    }

    private void _trailController(bool state)
    {
        for (int i = 0; i < _lines.Length; i++)
        {
            _lines[i].enabled = state;
        }
    }
}
