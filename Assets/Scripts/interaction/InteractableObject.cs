using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    [Header("Ustawienia")]
    [SerializeField] private string objectName = "Nieznany";

    private BaseInteraction interaction;
    private bool isHovered = false;

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
        Debug.Log($"Przedmiot ({objectName}) zosta³ klikniêty");

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