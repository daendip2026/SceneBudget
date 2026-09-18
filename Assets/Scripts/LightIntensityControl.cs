using UnityEngine;
using UnityEngine.InputSystem;

public class LightIntensityControl : MonoBehaviour
{

    [SerializeField] private Light controlledLight;
    [SerializeField] private float changedIntensity;
    private float initialIntensity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(controlledLight == null)
        {
            Debug.LogError("Controlled Light is not assigned.", this);
            enabled = false;
            return;
        }
        initialIntensity = controlledLight.intensity;
        Debug.Log($"Initial Intensity: {initialIntensity}", this);
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        
        if (keyboard == null)
        {
            return;
        }

        if(keyboard.rKey.wasPressedThisFrame)
        {
            controlledLight.intensity = initialIntensity;
        }
        else if (keyboard.spaceKey.wasPressedThisFrame)
        {
            controlledLight.intensity = changedIntensity;
        }
    }
}
