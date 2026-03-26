using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuration = 1f;

    void Start() {
      StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName) {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeOut(string sceneName) {

      float t = 0f;
        Color color = fadePanel.color;
        while(t < fadeDuration) {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadePanel.color = color;

            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeIn() {

      float t = 0f;
        Color color = fadePanel.color;
        while(t < fadeDuration) {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadePanel.color = color;

            yield return null;
        }

    }



}
