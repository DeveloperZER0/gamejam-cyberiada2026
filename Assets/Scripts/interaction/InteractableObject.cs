using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    [Header("Ustawienia")]
    [SerializeField] private string objectName = "Nieznany";

    [SerializeField] private BaseInteraction interaction;
    private bool isHovered = false;

    [Header("Dźwięk")]
    [SerializeField] private AudioClip interactionSound;   // <<-- tu przypisujesz dźwięk

    public AudioClip objectSound
    {
        get { return interactionSound; }
    }

    private void Awake()
    {
        interaction = GetComponent<BaseInteraction>();
    }

    public void OnHoverEnter()
    {
        isHovered = true;
        Debug.Log($"Najechano na: {objectName}");

        CursorManager cm = CursorManager.Instance;
        if (cm != null)
            cm.SetPointerCursor();
    }

    public void OnHoverExit()
    {
        isHovered = false;

        CursorManager cm = CursorManager.Instance;
        if (cm != null)
            cm.SetDefaultCursor();
    }

    public void OnClicked()
    {
        Debug.Log($"ONCLICKED WYWOŁANE na obiekcie: {objectName}");
        // Dźwięk dla tego konkretnego obiektu
        if (AudioMenager.Instance != null)
        {
            // jeśli ustawiono własny dźwięk, zagra jego,
            // jeśli nie – użyje domyślnego clickClip
            if (interactionSound != null)
                AudioMenager.Instance.PlayClip(interactionSound);
            else
                AudioMenager.Instance.PlayClick();
        }

        if (interaction != null)
        {
            interaction.Interact();
        }
        else
        {
            Debug.LogWarning($"Brak skryptu interakcji na: {objectName}");
        }
    }

    public string GetObjectName() => objectName;
    public bool IsHovered() => isHovered;
}
