using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    public enum ButtonType {GREEN, RED}

    // UI
    private Button greenButton;
    private Button redButton;

    private bool bNPC = true;

    // System Controllers
    private DialogueController dialogueController;
    private AudioController audioController;
    private SceneController sceneController;

    void Awake()
    {  
        // UI
        greenButton = GameObject.Find("Canvas/PlayerControllerPanel/Ok").GetComponent<Button>();
        redButton = GameObject.Find("Canvas/PlayerControllerPanel/Back").GetComponent<Button>();

        // System Controllers
        dialogueController = GameObject.Find("Systems").GetComponent<DialogueController>();
        audioController = GameObject.Find("Systems").GetComponent<AudioController>();
        sceneController = GameObject.Find("Systems").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInteractableButton(ButtonType buttonType, bool value)
    {
        Button targetButton = buttonType == ButtonType.GREEN ? greenButton : redButton;
        switch (buttonType)
        {
            case ButtonType.GREEN:
                targetButton.interactable = value;
                break;
            case ButtonType.RED:
                targetButton.interactable = value;
                break;
            default:
                Debug.LogError("Unknown button type: " + buttonType);
                break;
        }
    }

    public void SetIsNPC(bool bNPC)
    {
        this.bNPC = bNPC;
    }

    public void InteractionButton()
    {
        if (bNPC) // Action button on NPCs 
        {
            dialogueController.OpenDialogue();
        }
        else // Action button on Items 
        {
            audioController.PlayAudio();
            sceneController.LoadScene();
        }
    }
}
