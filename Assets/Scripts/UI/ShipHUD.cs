using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PanelRenderer))]
public class ShipHUD : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SailController sailController;
    [SerializeField] private PanelRenderer panelRenderer;

    private Label sailDeploymentLabel;
    private Label sailRotationLabel;

    private void OnEnable()
    {
        if (panelRenderer == null)
        {
            panelRenderer = GetComponent<PanelRenderer>();
        }

        panelRenderer.RegisterUIReloadCallback(OnUIReload);

        sailController.OnSailDeploymentChanged += SetDeployment;
        sailController.OnSailRotationChanged += SetRotation;
    }

    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReload);

        sailController.OnSailDeploymentChanged -= SetDeployment;
        sailController.OnSailRotationChanged -= SetRotation;
    }

    /// <summary>
    /// Retrieves sail status labels after the UI reloads.
    /// </summary>
    private void OnUIReload(
        PanelRenderer renderer,
        VisualElement element,
        int Version)
    {
        sailDeploymentLabel =
            element.Q<Label>("SailDeploymentLabel");

        sailRotationLabel =
            element.Q<Label>("SailRotationLabel");
    }

    /// <summary>
    /// Updates the UI with the current sail deployment
    /// value received through an event.
    /// </summary>
    /// <param name="value">
    /// The current sail deployment percentage (0–100).
    /// </param>
    private void SetDeployment(float value)
    {
        if (sailDeploymentLabel != null)
        {
            sailDeploymentLabel.text =
                $"Sail Deploiment {value:F0}%";
        }
    }

    /// <summary>
    /// Updates the UI with the current sail rotation
    /// value received through an event.
    /// </summary>
    /// <param name="value">
    /// The current sail rotation angle in degrees.
    /// </param>
    private void SetRotation(float value)
    {
        if (sailRotationLabel != null)
        {
            sailRotationLabel.text =
                 $"Sail rotaton {value:F1}\u00B0";
        }
    }

}
