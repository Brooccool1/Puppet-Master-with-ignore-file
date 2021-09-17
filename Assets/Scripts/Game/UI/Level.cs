using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    public static int level = 0;
    [SerializeField] private TextMeshProUGUI _text;

    // Update is called once per frame
    void Update()
    {
        _text.text = "Wave: " + level;
    }
}
