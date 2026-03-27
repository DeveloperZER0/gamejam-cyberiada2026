using UnityEngine;
using TMPro;
using System.Collections;

public class KeypadScript : MonoBehaviour
{
    [SerializeField ]private TextMeshProUGUI screenText;

    public GameObject Keypad;

    private string currentInput = "";
    private int maxInput = 4;
    private int code = 4527;
    void Start()
    {
        ClearScreen();
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
        if(code == int.Parse(currentInput))
        {
            Debug.Log("Code correct! Door unlocked.");
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
