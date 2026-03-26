using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneDialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public float typingSpeed = 0.05f;
    public float delayBetweenSentences = 2f;

    void Start()
    {
        StartCoroutine(PlayDialogue());
    }

    IEnumerator PlayDialogue()
    {
        foreach (string sentence in sentences)
        {
            yield return StartCoroutine(TypeSentence(sentence));
            yield return new WaitForSeconds(delayBetweenSentences);
        }

        // przejście do gry
        SceneManager.LoadScene("MainGame");
    }

    IEnumerator TypeSentence(string sentence)
    {
        textDisplay.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}