using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SailController sailController;

    [Header("Wind")]
    [SerializeField] private Vector3 wind = new(5f, 0f, 7f);

    [Header("Ship Parameters")]
    [SerializeField] private float mass = 15f;
    [SerializeField] private float dragCoefficient = 5f;
    [SerializeField] private float speed;

    [Header("Sail Parameters")]
    private float sailDeployment;
    private float sailRotation;

    private void OnEnable()
    {
        sailController.OnSailDeploymentChanged
            += SetSailDeployment;
        sailController.OnSailRotationChanged
            += SetSailRotaton;
    }

    private void OnDisable()
    {
        sailController.OnSailDeploymentChanged
            -= SetSailDeployment;
        sailController.OnSailRotationChanged
            -= SetSailRotaton;
    }

    private void SetSailRotaton(float value)
    {
        sailRotation = value;
    }

    private void SetSailDeployment(float value)
    {
        sailDeployment = value / 100f;
    }


    private void Update()
    {
        CalculateMovement();
    }

    /// <summary>
    /// Calculates ship movement based on wind,
    /// sail force, and water resistance.
    /// Updates the ship speed and position
    /// </summary>
    private void CalculateMovement()
    {
        if (mass <= 0f)
        {
            return;
        }

        Vector3 shipDirection = transform.forward;
        Vector3 sailDirection =
            sailController.transform.forward;

        // Project the wind vector onto the sail direction.
        float windProjection =
            Vector3.Dot(sailDirection, wind);

        // Calculate sail force using quadratic wind scaling.
        Vector3 sailForce =
            Mathf.Abs(windProjection) * sailDeployment
            * windProjection * sailDirection;

        // Project sail force onto ship's forward axis
        Vector3 shipForce =
            Vector3.Dot(shipDirection, sailForce)
            * shipDirection;

        // Calculate net force with quadratic water drag.
        Vector3 force =
            shipForce - (dragCoefficient * Mathf.Abs(speed)
            * speed * shipDirection);

        // Calculate acceleration using Newton's second law.
        Vector3 acceleration = force / mass;


        // Project acceleration onto the ship's forward axis.
        float forwardAcceleration =
            Vector3.Dot(acceleration, shipDirection);


        speed += forwardAcceleration * Time.deltaTime;

        // Stop the ship at very low speed with no sail.
        if (sailDeployment <= 0f
            && Mathf.Abs(speed) < 0.5f)
        {
            speed = 0f;
        }

        transform.position +=
           speed * Time.deltaTime * shipDirection;

    }
}
