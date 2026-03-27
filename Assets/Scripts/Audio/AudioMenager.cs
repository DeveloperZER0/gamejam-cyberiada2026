using UnityEngine;

// Ten atrybut automatycznie doda komponent AudioSource do obiektu, jeœli go tam nie ma
[RequireComponent(typeof(AudioSource))]
public class AudioMenager : MonoBehaviour
{
    public static AudioMenager Instance;

    public AudioClip clickClip;

    // Referencja do "g³oœnika", który odtworzy dŸwiêk
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Pobieramy komponent AudioSource podpiêty do tego samego GameObjectu
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClick()
    {
        PlayClip(clickClip);
    }

    public void PlayClip(AudioClip clip)
    {
        // Sprawdzamy, czy przekazano klip oraz czy mamy AudioSource
        if (clip != null && audioSource != null)
        {
            // Odtwarzamy przekazany klip u¿ywaj¹c AudioSource
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Brak clip lub AudioSource w AudioMenager!");
        }
    }
}