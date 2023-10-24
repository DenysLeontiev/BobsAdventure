using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    [SerializeField] private List<DialogueLines> dialogueLines;

    private int currentDialogueIndex = 0;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void StartDialogue()
    {
        if(currentDialogueIndex >= dialogueLines.Count)
        {
            Debug.LogError("Index is out of range in dialogueLines");
            return;
        }

        var firstUnplayedDialogue = dialogueLines.FirstOrDefault(x => !x.isFinished);

        if (firstUnplayedDialogue != null && firstUnplayedDialogue.isCurrentlyPlaying == false)
        {
            DialogueManager.Instance.StartDialogue(dialogueLines[currentDialogueIndex]);
            currentDialogueIndex++;
        }
    }

    private void ResetDialogueStates()
    {
        foreach (var dialogueLine in dialogueLines)
        {
            dialogueLine.isFinished = false;
            dialogueLine.isCurrentlyPlaying = false;
        }
    }
}
