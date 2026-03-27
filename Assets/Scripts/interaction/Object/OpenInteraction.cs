using UnityEngine;

public class OpenInteraction : BaseInteraction
{
    [SerializeField] private Animator animator;
    private bool hasInteracted;

    public override void Interact() {
        if (hasInteracted)
        {
            return;
        }

        if (animator == null)
        {
            Debug.LogWarning("OpenInteraction: Animator is not assigned.");
            return;
        }

        hasInteracted = true;
        FadeInAndOut(animator, "isOpen");

        if (TryGetComponent(out Collider2D objectCollider))
        {
            objectCollider.enabled = false;
        }

        if (TryGetComponent(out InteractableObject interactableObject))
        {
            interactableObject.enabled = false;
        }
    }
}
