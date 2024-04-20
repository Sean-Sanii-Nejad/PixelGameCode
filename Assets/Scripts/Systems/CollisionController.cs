using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollisionController : MonoBehaviour
{
    private Button greenButton;
    private Button redButton;
    private DialogueSystemController dialogueSystemController;
    private bool bInside;

    void Awake()
    {
        bInside = false;
        greenButton = GameObject.Find("Canvas/PlayerControllerPanel/Ok").GetComponent<Button>();
        redButton = GameObject.Find("Canvas/PlayerControllerPanel/Back").GetComponent<Button>();
        dialogueSystemController = GameObject.Find("Systems").GetComponent<DialogueSystemController>();
        greenButton.interactable = false;
        redButton.interactable = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            bInside = true;
        }
        if (other.CompareTag("NPC") && !dialogueSystemController.IsChatGoing())
        {
            dialogueSystemController.getPortraitPanel().SetActive(true);
            dialogueSystemController.SetDialogue(other.GetComponent<Dialogue>().dialogue);
            dialogueSystemController.SetPortrait(other.GetComponent<Dialogue>().portrait);
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
        if (other.CompareTag("NPC"))
        {
            bInside = false;
            greenButton.interactable = false;
            redButton.interactable = false;
            ColorBlock colours = greenButton.colors;
            Color normalColour = colours.normalColor;
            normalColour.a = 0.5f;
            colours.normalColor = normalColour;
            greenButton.colors = colours;
            dialogueSystemController.CloseDialogue();
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
