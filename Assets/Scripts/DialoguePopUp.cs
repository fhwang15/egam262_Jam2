using UnityEngine;
using TMPro;
using System.Collections;

public class DialoguePopUp : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text dialogueText;
    public float displayTime = 2f;

    public void ShowDialogue(string message)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = message;
        StopAllCoroutines();
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        dialogueBox.SetActive(false);
    }
}