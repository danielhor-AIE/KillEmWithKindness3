using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeneGames.DialogueSystem;

public class NPCCharacter : MonoBehaviour
{

    private DialogueManager npcDialogue = null;
    private DialogueUI dialogueUI = null;


    private void Awake()
    {
       dialogueUI = FindObjectOfType<DialogueUI>();
       npcDialogue = GetComponent<DialogueManager>();
    }
    public void Interact()
    {
        Debug.Log("Interacting"); // this is where you will trigger a dialogue from
        npcDialogue.StartDialogue();
        dialogueUI.currentDialogueManager = npcDialogue; 


    }

    public void ChangeConversation()
    {
        //conversation = currentConversation;
    }
}
