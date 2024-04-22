using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollisionController : MonoBehaviour
{
    private Button greenButton;
    private Button redButton;
    private bool bInside;

    // System Controllers
    private DialogueController dialogueController;
    private AudioController audioSystemController;
    private InteractionController interactionController;
    private SceneController sceneController;

    void Awake()
    {
        // UI
        greenButton = GameObject.Find("Canvas/PlayerControllerPanel/Ok").GetComponent<Button>();
        redButton = GameObject.Find("Canvas/PlayerControllerPanel/Back").GetComponent<Button>();
        
        bInside = false;
        greenButton.interactable = false;
        redButton.interactable = false;

        // System Controllers
        dialogueController = GameObject.Find("Systems").GetComponent<DialogueController>();
        audioSystemController = GameObject.Find("Systems").GetComponent<AudioController>();
        interactionController = GameObject.Find("Systems").GetComponent<InteractionController>();
        sceneController = GameObject.Find("Systems").GetComponent<SceneController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable") && other.GetComponent<Data>().NPC)
        {
            bInside = true;
        }
        if (other.CompareTag("Interactable") && !dialogueController.IsChatGoing())
        {
            // Transfer Data from Selected Target
            dialogueController.SetDialogue(other.GetComponent<Data>().dialogue);
            dialogueController.SetPortrait(other.GetComponent<Data>().portrait);
            interactionController.SetIsNPC(other.GetComponent<Data>().NPC);
            sceneController.SetScene(other.GetComponent<Data>().sceneName);
            audioSystemController.SetAudioEffect(other.GetComponent<AudioSource>());

            dialogueController.GetPortraitPanel().SetActive(true);
            greenButton.interactable = true;
            ColorBlock colours = greenButton.colors;
            Color normalColour = colours.normalColor;
            normalColour.a = 1.0f;
            colours.normalColor = normalColour;
            greenButton.colors = colours;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            bInside = false;
            greenButton.interactable = false;
            redButton.interactable = false;
            ColorBlock colours = greenButton.colors;
            Color normalColour = colours.normalColor;
            normalColour.a = 0.5f;
            colours.normalColor = normalColour;
            greenButton.colors = colours;
            dialogueController.CloseDialogue();
        }
    }

    public Button GetGreenButton()
    {
        return greenButton;
    }

    public Button GetRedButton()
    {
        return redButton;
    }

    public bool IsInside()
    {
        return bInside;
    }
}
