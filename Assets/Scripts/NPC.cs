using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private List<DialogueLines> dialogueLines;
    [SerializeField] private float triggerDialogueDistance = 2f;

    private Transform player;
    private int currentDialogueIndex = 0;

    private DialogueActivationImage dialogueActivationImage;
    private Animator interactionImageAnimator;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        interactionImageAnimator = GetComponentInChildren<Animator>();

        dialogueActivationImage = GetComponentInChildren<DialogueActivationImage>();
        dialogueActivationImage.OnDialogueImageClicked += DialogueActivationImage_OnDialogueImageClicked;

        ResetDialogueStates();
    }

    private void Update()
    {
        HandleDialogueActivation();
    }

    private void HandleDialogueActivation()
    {

        float distanceBetween = DistanceBetween(player);
        bool hasUnfinishedDialogue = dialogueLines.Any(d => !d.isFinished);
        bool shouldFadeOut = !hasUnfinishedDialogue || distanceBetween > triggerDialogueDistance;

        interactionImageAnimator.SetBool("fadeOut", shouldFadeOut);
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

    private float DistanceBetween(Transform player)
    {
        return Vector2.Distance(player.position, transform.position);
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
