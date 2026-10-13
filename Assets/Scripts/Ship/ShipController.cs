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

    private void CalculateMovement()
    {
        if (mass <= 0f)
        {
            return;
        }

        Vector3 shipDirection = transform.forward;
        Vector3 sailDirection =
            sailController.transform.forward;

        float windProjection =
            Vector3.Dot(sailDirection, wind);

        Vector3 sailForce =
            Mathf.Abs(windProjection) * sailDeployment 
            * windProjection * sailDirection;

        Vector3 shipForce =
            Vector3.Dot(shipDirection, sailForce)
            * shipDirection;

        Vector3 force =
            shipForce - (dragCoefficient * Mathf.Abs(speed)
            * speed * shipDirection);

        Vector3 acceleration = force / mass;

        float forwardAcceleration =
            Vector3.Dot(acceleration, shipDirection);

        speed = forwardAcceleration * Time.deltaTime;

        transform.position +=
           speed * Time.deltaTime * shipDirection;

    }
}
