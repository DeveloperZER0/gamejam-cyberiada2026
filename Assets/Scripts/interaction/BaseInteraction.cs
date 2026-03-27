using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class BaseInteraction : MonoBehaviour
{
  [SerializeField] private GameObject fadeObject;
  [SerializeField] private float fadeDuration = 0.5f;
  private Image fadePanel;

    protected virtual void Awake() {
      if(fadeObject == null) {
        fadeObject = GameObject.Find("fadeObject");
      }

      if (fadeObject != null) {
        fadePanel = fadeObject.GetComponent<Image>();
        if (fadePanel != null) {
          // Keep panel clickable behavior neutral when alpha is zero.
          fadePanel.raycastTarget = false;
        }
      }
    }

    public abstract void Interact();

    public virtual void FadeInAndOut(Animator oAnimator, string parameterName) {
      // Start fade in animation, then wait for it to finish before animating the object, then start fade out animation
      if (fadeObject == null || fadePanel == null) {
        if (oAnimator != null && !string.IsNullOrEmpty(parameterName)) {
          oAnimator.SetBool(parameterName, !oAnimator.GetBool(parameterName));
        }
        return;
      }

      fadeObject.SetActive(true);
      StartCoroutine(WaitForFade(oAnimator, parameterName));

    }

    private IEnumerator WaitForFade(Animator objectAnimator, string parameterName) {
      yield return FadeAlpha(0f, 1f);

      if (objectAnimator != null && !string.IsNullOrEmpty(parameterName)) {
        objectAnimator.SetBool(parameterName, !objectAnimator.GetBool(parameterName));

        yield return null;
        yield return WaitForAnimationToFinish(objectAnimator);
      }

      yield return FadeAlpha(1f, 0f);
    }

    private IEnumerator FadeAlpha(float from, float to) {
      if (fadeDuration <= 0f) {
        Color instantColor = fadePanel.color;
        instantColor.a = to;
        fadePanel.color = instantColor;
        yield break;
      }

      float t = 0f;
      Color color = fadePanel.color;

      while (t < fadeDuration) {
        t += Time.deltaTime;
        color.a = Mathf.Lerp(from, to, t / fadeDuration);
        fadePanel.color = color;
        yield return null;
      }

      color.a = to;
      fadePanel.color = color;
    }

    private IEnumerator WaitForAnimationToFinish(Animator animator) {
      if (animator == null) {
        yield break;
      }

      float timeout = 5f;
      float elapsed = 0f;

      while (animator.IsInTransition(0) && elapsed < timeout) {
        elapsed += Time.deltaTime;
        yield return null;
      }

      AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

      if (stateInfo.loop) {
        yield break;
      }

      while (stateInfo.normalizedTime < 1f && elapsed < timeout) {
        elapsed += Time.deltaTime;
        yield return null;
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
      }

    }
}
