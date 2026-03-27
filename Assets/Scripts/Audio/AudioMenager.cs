using UnityEngine;

public class AudioMenager : MonoBehaviour
{
    public static AudioMenager Instance;

    public AudioSource audioSource;
    public AudioClip clickClip;  // mo¿esz dalej u¿ywaæ jako domyœlnego

    public InteractableObject intObj;

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
        }
    }

    // stary przyciskowy dŸwiêk
    public void PlayClick()
    {
        PlayClip(clickClip);
    }

    // NOWA metoda – odtwarza dowolny klip
    public void PlayClip(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Brak clip lub audioSource w AudioMenager!");
        }
    }
}
