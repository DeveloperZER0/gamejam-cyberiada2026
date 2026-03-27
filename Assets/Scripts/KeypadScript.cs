using UnityEngine;
using TMPro;
using System.Collections;

public class KeypadScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI screenText;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject parentSafe;
    [SerializeField] private string successParameterName = "keycode";

    public GameObject Keypad;

    private string currentInput = "";
    private int maxInput = 6;
    private string code = "452785";

    private Animator animator;
    private Animator parentAnimator;

    void Start()
    {
        ClearScreen();

        if (targetObject != null)
        {
            animator = targetObject.GetComponent<Animator>();
            parentAnimator = parentSafe.GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogWarning("KeypadScript: Missing target Animator. Assign targetObject with Animator in Inspector.");
        }
    }

    public void AddDigit(string digit)
    {
        if (currentInput.Length < maxInput)
        {
            currentInput += digit;
            UpdateScreen();
        }
    }

    public void ClearScreen()
    {
        currentInput = "";
        UpdateScreen();
    }

    IEnumerator wrongAnswer()
    {
        yield return new WaitForSeconds(3f);
        ClearScreen();
    }

    public void SubmitCode()
    {
        if (code == currentInput)
        {
            if (animator != null && !string.IsNullOrEmpty(successParameterName))
            {
                animator.SetBool(successParameterName, true);
                parentAnimator.SetBool("IsUnlocked", true);
            }

            Keypad.SetActive(false);
        }
        else
        {
            screenText.text = "WRONG !!!";
            StartCoroutine(wrongAnswer());
        }
    }

    private void UpdateScreen()
    {
        if (screenText != null)
        {
            screenText.text = currentInput;
        }
    }
}
