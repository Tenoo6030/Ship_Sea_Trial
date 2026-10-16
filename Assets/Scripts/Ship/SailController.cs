using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SailController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform sail;

    [Header("Sail")]
    [SerializeField, Range(0f, 100f)]
    private float sailDeployment = 0f;
    private float lastSailDeployment;

    [SerializeField, Range(-45f, 45f)]
    private float sailRotation = 0f;
    private float lastSailRotation;

    [Header("Controls")]
    private ShipInputActions inputActions;

    [SerializeField] private float sailDeploymentSpeed = 20f;
    [SerializeField] private float sailRotationSpeed = 20f;

    public event Action<float> OnSailDeploymentChanged;
    public event Action<float> OnSailRotationChanged;


    private void Awake()
    {
        inputActions = new();
    }

    private void OnEnable()
    {
        inputActions.Ship.Enable();
    }

    private void OnDisable()
    {
        inputActions.Ship.Disable();
    }

    private void OnDestroy()
    {
        inputActions.Dispose();
    }

    private void Update()
    {
        UpdateSailDeployment();
        UpdateSailRotation();
    }

    private void UpdateSailDeployment()
    {
        float input = inputActions.Ship
            .SailDeployment.ReadValue<float>();

        float newValue =
            Mathf.Clamp(sailDeployment
            + input * sailDeploymentSpeed
            * Time.deltaTime, 0f, 100f
            );

        if (Mathf.Approximately(lastSailDeployment, newValue))
        {
            return;
        }

        sailDeployment = newValue;

        sail.localScale =
                new Vector3(1f, sailDeployment / 100f, 1f);

        OnSailDeploymentChanged?.Invoke(sailDeployment);

        lastSailDeployment = sailDeployment;

    }

    private void UpdateSailRotation()
    {
        float input = inputActions.Ship
            .SailRotation.ReadValue<float>();

        float newValue =
            Mathf.Clamp(sailRotation
            + input * sailRotationSpeed
            * Time.deltaTime, -45f, 45f
            );

        if (Mathf.Approximately(lastSailRotation, newValue))
        {
            return;
        }

        sailRotation = newValue;

        sail.localRotation =
            Quaternion.Euler(0f, sailRotation, 0f);

        OnSailRotationChanged?.Invoke(sailRotation);

        lastSailRotation = sailRotation;
    }
}
