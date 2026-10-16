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
        panelRenderer = GetComponent<PanelRenderer>();

        panelRenderer.RegisterUIReloadCallback(OnUIReload);

        sailController.OnSailDeploymentChanged += SetDeployment;
        sailController.OnSailRotationChanged += SetRotation;
    }

    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReload);
    }

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

    private void SetDeployment(float value)
    {
        if (sailDeploymentLabel != null)
        {
            sailDeploymentLabel.text =
                $"Sail Deploiment {value:F0}%";
        }
    }

    private void SetRotation(float value)
    {
        if (sailRotationLabel != null)
        {
            sailRotationLabel.text =
                 $"Sail rotaton {value:F1}\u00B0";
        }
    }

}
