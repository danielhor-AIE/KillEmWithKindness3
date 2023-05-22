using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacter : MonoBehaviour
{

    //[SerializeField] public DialogBehaviour npcDialogue = null;
    //[SerializeField] public DialogNodeGraph conversation = null;
    private GameObject dialogueGo = null;


    private void Awake()
    {
        //dialogueGo = FindObjectOfType<DialogDisplayer>().gameObject;
        dialogueGo.SetActive(false);
    }
    public void Interact()
    {
        Debug.Log("Interacting"); // this is where you will trigger a dialogue from
        //npcDialogue.StartDialog();
        dialogueGo.SetActive(true);
    }

    public void ChangeConversation()
    {
        //conversation = currentConversation;
    }
}
