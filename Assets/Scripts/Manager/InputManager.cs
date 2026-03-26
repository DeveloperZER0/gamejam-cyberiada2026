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
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();   // zamiast Input.mousePosition [web:8][web:16]

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(
            new Vector3(mouseScreenPos.x, mouseScreenPos.y, mainCamera.nearClipPlane)
        );

        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 0f, interactableLayer);

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
        if (Mouse.current.leftButton.wasPressedThisFrame && currentTarget != null)   // zamiast Input.GetMouseButtonDown(0) [web:8]
        {
            currentTarget.OnClicked();
        }
    }
}