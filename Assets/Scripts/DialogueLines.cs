using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DialogueLines : ScriptableObject
{
    [Serializable]
    public class DialogueLine 
    {
        public string interlocutorName;
        public string interlocutorLine;
        public bool isLineFinished = false;
    }

    public List<DialogueLine> dialogueLines;
    public bool isCurrentlyPlaying = false;
    public bool isFinished = false;
}
