using UnityEngine;
using UnityEngine.InputSystem;   // <--- nowy Input System

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Ustawienia Raycast")]
    [SerializeField] private LayerMask interactableLayer;

    private Camera mainCamera;
    private InteractableObject currentTarget;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleRaycast();
        HandleClick();
    }

    private void HandleRaycast()
    {
        // pozycja myszy z nowego Input Systemu
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        // Uzycie promienia kamery eliminuje bledy wynikajace z niepoprawnej glebokosci Z.
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, interactableLayer);

        if (hit.collider != null)
        {
            InteractableObject newTarget = hit.collider.GetComponent<InteractableObject>();

            if (newTarget != null)
            {
                if (newTarget != currentTarget)
                {
                    if (currentTarget != null)
                        currentTarget.OnHoverExit();

                    currentTarget = newTarget;
                    currentTarget.OnHoverEnter();
                }
                return;
            }
        }

        if (currentTarget != null)
        {
            currentTarget.OnHoverExit();
            currentTarget = null;
        }
    }

    private void HandleClick()
    {
        // lewy przycisk z nowego Input Systemu
        if (Mouse.current.leftButton.wasPressedThisFrame && currentTarget != null)
        {
            currentTarget.OnClicked();
        }
    }
}