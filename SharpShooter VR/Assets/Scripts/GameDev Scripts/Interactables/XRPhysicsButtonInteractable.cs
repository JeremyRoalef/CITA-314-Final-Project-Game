using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpringJoint))]
public class XRPhysicsButtonInteractable : XRSimpleInteractable
{
    public UnityEvent OnPhysicsButtonPressed;
    public UnityEvent OnBaseExit;

    [SerializeField]
    Collider minHeightCollider;

    [SerializeField]
    Collider maxHeightCollider;

    const string DEFAULT_LAYER_MASK = "Default";
    const string GRAB_LAYER_MASK = "Grab";

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);


    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (minHeightCollider != null)
        {
            if (isHovered && other == minHeightCollider)
            {
                OnPhysicsButtonPressed?.Invoke();
            }
        }

        if (maxHeightCollider != null)
        {
            if (other == maxHeightCollider)
            {
                ChangeLayerMask(DEFAULT_LAYER_MASK);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (minHeightCollider != null)
        {
            if (other == minHeightCollider)
            {
                OnBaseExit?.Invoke();
            }
        }

        if (maxHeightCollider != null)
        {
            if (other == maxHeightCollider)
            {
                ChangeLayerMask(GRAB_LAYER_MASK);
            }
        }
    }

    void ChangeLayerMask(string mask)
    {
        interactionLayers = InteractionLayerMask.GetMask(mask);
    }
}
