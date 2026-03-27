using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    [Header("Kursory")]
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D pointerCursor;

    [Header("Hotspoty (punkt kliknięcia w pikselach od lewego-górnego rogu)")]
    [SerializeField] private Vector2 defaultHotspot = Vector2.zero;
    [SerializeField] private Vector2 pointerHotspot = Vector2.zero;

    private Vector2 resolvedDefaultHotspot;
    private Vector2 resolvedPointerHotspot;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        resolvedDefaultHotspot = ResolveHotspot(defaultCursor, defaultHotspot);
        resolvedPointerHotspot = ResolveHotspot(pointerCursor, pointerHotspot);
    }

    private void Start()
    {
        SetDefaultCursor();
    }

    public void SetPointerCursor()
    {
        // ForceSoftware gwarantuje użycie naszego hotspotu na każdej platformie
        Cursor.SetCursor(pointerCursor, resolvedPointerHotspot, CursorMode.ForceSoftware);
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, resolvedDefaultHotspot, CursorMode.ForceSoftware);
    }

    private Vector2 ResolveHotspot(Texture2D texture, Vector2 hotspot)
    {
        if (texture == null)
        {
            Debug.LogWarning($"[CursorManager] Brak tekstury kursora!");
            return Vector2.zero;
        }

        // Ogranicz do granic tekstury, żeby Unity nie zgubiło hotspotu
        hotspot.x = Mathf.Clamp(hotspot.x, 0f, texture.width  - 1f);
        hotspot.y = Mathf.Clamp(hotspot.y, 0f, texture.height - 1f);

        return hotspot;
    }
}