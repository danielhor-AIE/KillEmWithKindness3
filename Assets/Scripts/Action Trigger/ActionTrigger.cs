﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionTrigger : MonoBehaviour
{
    [Multiline][Tooltip("The text to display on screen when the player is in the trigger.")]
    public string promptText;
    [Tooltip("The key the player must press to activate the action. If set to None, it will activate immediately.")]
    public KeyCode key;
    [Tooltip("Should the trigger only work once?")]
    public bool onceOnly;
    public UnityEvent action;

    private Text promptTextBox;
    private bool ranOnce, inTrigger;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            promptTextBox = GameObject.Find("InteractCanvas").GetComponentInChildren<Text>();
        }
        catch (System.NullReferenceException exception)
        {
            Debug.LogError("No InteractCanvas in scene! Drag the prefab in.");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (onceOnly && ranOnce) return;
        if (!inTrigger) return;
        if (Input.GetKeyDown(key))
        {
            ranOnce = true;
            
            action.Invoke();
            print("invoked");
                
            if (onceOnly)
            {
                promptTextBox.text = "";
            }
            if (key == KeyCode.None)
                inTrigger = false;
            
        }
        if(key == KeyCode.None && !onceOnly)
        {
            action.Invoke();
            print("invoked");

            if (onceOnly)
            {
                promptTextBox.text = "";
            }
            if (key == KeyCode.None)
                inTrigger = false;

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (onceOnly && ranOnce) return;
        if (other.gameObject.tag == "Player")
        {
            promptTextBox.text = promptText;
            inTrigger = true;
            if (onceOnly && key == KeyCode.None)
            {
                action.Invoke();
                ranOnce = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            promptTextBox.text = "";
            inTrigger = false;
        }
    }

}
