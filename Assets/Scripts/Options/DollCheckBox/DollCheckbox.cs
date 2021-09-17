using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DollCheckbox : MonoBehaviour
{
    public Toggle selectedToggle;

    private void Start()
    {
        selectedToggle.isOn = OptionsTemplate.doll;
        selectedToggle.onValueChanged.AddListener(delegate { _onToggleChange(selectedToggle); });
    }

    private void _onToggleChange(Toggle tglValue)
    {
        OptionsTemplate.doll = tglValue.isOn;
    }
}
