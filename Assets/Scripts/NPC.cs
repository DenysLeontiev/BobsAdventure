using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private List<DialogueLines> dialogueLines;

    private Transform player;
    [SerializeField] private int currentDialogueIndex = -1;

    private DialogueActivationImage dialogueActivationImage;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;

        dialogueActivationImage = GetComponentInChildren<DialogueActivationImage>();
        dialogueActivationImage.OnDialogueImageClicked += DialogueActivationImage_OnDialogueImageClicked;

        ResetDialogueStates();
    }

    private void DialogueActivationImage_OnDialogueImageClicked(object sender, System.EventArgs e)
    {
        var firstUnplayedDialogue = dialogueLines.FirstOrDefault(d => !d.isFinished);

        if(firstUnplayedDialogue != null && firstUnplayedDialogue.isCurrentlyPlaying == false)
        {
            DialogueManager.Instance.StartDialogue(dialogueLines[currentDialogueIndex]);
            currentDialogueIndex++;
        }
    }


    // Because ScriptableObjects remember states, so we reset them for easier testing
    private void ResetDialogueStates()
    {
        foreach (var dialogueLine in dialogueLines)
        {
            dialogueLine.isFinished = false;
            dialogueLine.isCurrentlyPlaying = false;
        }
    }
}
