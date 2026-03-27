using UnityEngine;
using TMPro;
using System.Collections;

public class KeypadScript : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI screenText;

    public GameObject Keypad;

    private string currentInput = "";
    private int maxInput = 6;
    private string code = "452785";

    private Animator animator;
    void Start()
    {
        ClearScreen();
        GameObject safeObject = GameObject.Find("Safe");

        if (safeObject != null)
        {
            animator = safeObject.GetComponent<Animator>();
            Debug.Log("Uda³o siê znaleŸæ Animator na obiekcie Sejf!");
        }
        else
        {
            Debug.LogError("B£¥D: Nie znaleziono w scenie obiektu o nazwie 'Sejf'!");
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
        if(code == currentInput)
        {
            animator.SetBool("IsUnlocked", true);
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
