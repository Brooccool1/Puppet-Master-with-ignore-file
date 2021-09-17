using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detach : MonoBehaviour
{
    [SerializeField] private HingeJoint2D _head;
    private bool _playing = false;

    private void Update()
    {
        if (!_playing)
        {
            if (!_head)
            {
                GetComponent<ParticleSystem>().Play();
                _playing = true;
            }
        }
    }
}
