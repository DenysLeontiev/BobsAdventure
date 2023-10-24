using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    private const string ANIM_STATE_NAME = "appear"; // for handling animations

    public event EventHandler OnDialogueStarted;
    public event EventHandler OnDialogueFinished;

    [SerializeField] private KeyCode dialogueActivationKey = KeyCode.E;
    [SerializeField] private int charactersPerSecond = 1;

    [SerializeField] private GameObject dialogueBox;
    private TextMeshProUGUI dialogueBoxText;
    [SerializeField] private GameObject dialogueeName;
    private TextMeshProUGUI dialogueeNameText;

    private DialogueLines currentDialogueLines;
    private int currentDialogueLineIndex = 0;

    private Animator dialogueBoxAnimator;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dialogueBoxAnimator = GetComponent<Animator>();

        Hide();
        dialogueBoxText = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
        dialogueeNameText = dialogueeName.GetComponentInChildren<TextMeshProUGUI>();

    }

    public void StartDialogue(DialogueLines dialogueLines)
    {
        Show();
        AnimDialogueAppear();

        OnDialogueStarted?.Invoke(this, EventArgs.Empty);

        currentDialogueLines = dialogueLines;

        StartCoroutine(MoveThroughDialogueLines(currentDialogueLines));
    }

    private IEnumerator MoveThroughDialogueLines(DialogueLines dialogue)
    {
        dialogue.isCurrentlyPlaying = true;
        if (dialogue.isFinished)
        {
            yield break;
        }

        if (dialogue == null)
        {
            yield break;
        }

        for (int i = 0; i < dialogue.dialogueLines.Count; i++)
        {
            yield return StartCoroutine(TypeText(dialogueeNameText, dialogue.dialogueLines[i].interlocutorName));
            yield return StartCoroutine(TypeText(dialogueBoxText, dialogue.dialogueLines[i].interlocutorLine));

            currentDialogueLineIndex++;
            dialogue.dialogueLines[i].isLineFinished = true;

            yield return new WaitUntil(() =>
            {
                return Input.GetKeyDown(dialogueActivationKey);
            });

            yield return null;
        }

        dialogue.isFinished = true;
        OnDialogueFinished?.Invoke(this, EventArgs.Empty);
        StartCoroutine(HideWithDelay());
    }

    private IEnumerator TypeText(TextMeshProUGUI textToType, string line)
    {
        string textBuffer = null;
        foreach (char c in line)
        {
            textBuffer += c;
            textToType.text = textBuffer;
            yield return new WaitForSeconds((float)(1 / charactersPerSecond));
        }
    }

    private void StopDialogue()
    {
        currentDialogueLines = null;
        currentDialogueLineIndex = 0;
        dialogueBoxText.text = "";
        dialogueeNameText.text = "";

        Hide(); // TODO: not the best practice, disappears after each new line
    }

    private void AnimDialogueAppear()
    {
        dialogueBoxAnimator.SetBool(ANIM_STATE_NAME, true);
    }

    private void AnimDialogueDisappear()
    {
        dialogueBoxAnimator.SetBool(ANIM_STATE_NAME, false);
    }

    private void Show()
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueeName.gameObject.SetActive(true);
    }

    private void Hide()
    {
        dialogueBox.SetActive(false);
        dialogueeName.SetActive(false);
    }

    private IEnumerator HideWithDelay()
    {
        AnimDialogueDisappear();
        yield return new WaitForSeconds(0.5f);
        Hide();
    }
}
