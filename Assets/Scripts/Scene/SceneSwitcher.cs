using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    [System.Serializable]
    private class PseudoSceneEntry
    {
        public string sceneId;
        public GameObject rootObject;
    }

    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private PseudoSceneEntry[] pseudoScenes;
    [SerializeField] private int initialSceneIndex;
    [SerializeField] private bool preserveAnimatorStateOnDisable = true;

    // Zmienione na AudioClip
    public AudioClip fadeAudioClip;

    // Prywatny AudioSource do odtworzenia klipu
    private AudioSource audioSource;

    private bool isSwitching;
    private int currentSceneIndex = -1;

    void Start()
    {
        SetupAudio();
        ConfigureAnimators();
        SwitchToIndexImmediate(initialSceneIndex);
        StartCoroutine(FadeIn());
    }

    private void SetupAudio()
    {
        // Sprawdzamy, czy obiekt ma ju¿ AudioSource. Jeœli nie, dodajemy go z poziomu kodu.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void ConfigureAnimators()
    {
        if (!preserveAnimatorStateOnDisable || pseudoScenes == null)
        {
            return;
        }

        for (int i = 0; i < pseudoScenes.Length; i++)
        {
            GameObject root = pseudoScenes[i].rootObject;
            if (root == null)
            {
                continue;
            }

            Animator[] animators = root.GetComponentsInChildren<Animator>(true);
            for (int j = 0; j < animators.Length; j++)
            {
                animators[j].keepAnimatorStateOnDisable = true;
            }
        }
    }

    public void FadeToScene(string sceneName)
    {
        FadeToPseudoScene(sceneName);
    }

    public void FadeToPseudoScene(string sceneId)
    {
        int targetIndex = FindSceneIndex(sceneId);

        if (targetIndex < 0)
        {
            Debug.LogWarning($"SceneSwitcher: pseudo-scene '{sceneId}' not found.");
            return;
        }

        FadeToIndex(targetIndex);
    }

    public void FadeToIndex(int targetIndex)
    {
        if (isSwitching)
        {
            return;
        }

        if (!IsIndexValid(targetIndex))
        {
            Debug.LogWarning($"SceneSwitcher: invalid pseudo-scene index {targetIndex}.");
            return;
        }

        if (targetIndex == currentSceneIndex)
        {
            return;
        }

        StartCoroutine(FadeSwitch(targetIndex));
    }

    public void SwitchToIndexImmediate(int targetIndex)
    {
        if (!IsIndexValid(targetIndex))
        {
            return;
        }

        SetActivePseudoScene(targetIndex);
    }

    private IEnumerator FadeSwitch(int targetIndex)
    {
        isSwitching = true;

        // Odtworzenie dŸwiêku z przypisanego klipu
        if (fadeAudioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(fadeAudioClip);
        }

        yield return FadeOut();
        SetActivePseudoScene(targetIndex);
        yield return FadeIn();

        isSwitching = false;
    }

    private IEnumerator FadeOut()
    {
        if (fadePanel == null)
        {
            yield break;
        }

        if (fadeDuration <= 0f)
        {
            Color instantColor = fadePanel.color;
            instantColor.a = 1f;
            fadePanel.color = instantColor;
            yield break;
        }

        float t = 0f;
        Color color = fadePanel.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadePanel.color = color;
            yield return null;
        }

        color.a = 1f;
        fadePanel.color = color;
    }

    private IEnumerator FadeIn()
    {
        if (fadePanel == null)
        {
            yield break;
        }

        if (fadeDuration <= 0f)
        {
            Color instantColor = fadePanel.color;
            instantColor.a = 0f;
            fadePanel.color = instantColor;
            yield break;
        }

        float t = 0f;
        Color color = fadePanel.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadePanel.color = color;
            yield return null;
        }

        color.a = 0f;
        fadePanel.color = color;
    }

    private void SetActivePseudoScene(int targetIndex)
    {
        for (int i = 0; i < pseudoScenes.Length; i++)
        {
            if (pseudoScenes[i].rootObject != null)
            {
                pseudoScenes[i].rootObject.SetActive(i == targetIndex);
            }
        }

        currentSceneIndex = targetIndex;
    }

    private int FindSceneIndex(string sceneId)
    {
        if (string.IsNullOrEmpty(sceneId))
        {
            return -1;
        }

        for (int i = 0; i < pseudoScenes.Length; i++)
        {
            if (pseudoScenes[i].sceneId == sceneId)
            {
                return i;
            }
        }

        return -1;
    }

    private bool IsIndexValid(int index)
    {
        return index >= 0 && index < pseudoScenes.Length;
    }
}