using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{

    public Light m_Light; // im using m_Light name since 'light' is already a variable used by unity
    public bool isOn;

    void UpdateLight()
    {
        m_Light.enabled = isOn;
    }

    public override string GetDescription()
    {
        if (isOn) return "Press [F] to turn <color=red>off</color> the light.";
        return "Press [F] to turn <color=green>on</color> the light.";
    }

    public override void Interact()
    {
        isOn = !isOn;
        UpdateLight();
    }
}