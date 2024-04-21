using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystemController : MonoBehaviour
{
    public float wordSpeed;
    public CollisionController collisionController;
    public Button greenButton;
    public Button redButton;
    public Animator animator;

    [SerializeField]
    private string[] dialogue;
    [SerializeField]
    private Image portrait;
    private int index;
    private GameObject dialoguePanel;
    private GameObject portraitPanel;
    private Text dialogueText;
    private bool bDialogueOpen;
    private bool bChatOnGoing;
    private bool bNPC;
    private AudioSystemController audioSystemController;
    
    void Start()
    {
        portrait = GameObject.Find("Canvas/PortraitPanel/Portrait").GetComponent<Image>();
        dialoguePanel = GameObject.Find("Canvas/DialoguePanel");
        portraitPanel = GameObject.Find("Canvas/PortraitPanel");
        dialogueText = GameObject.Find("Canvas/DialoguePanel/Text").GetComponent<Text>();
        collisionController = GameObject.Find("Entities/Player").GetComponent<CollisionController>();
        audioSystemController = GameObject.Find("Systems").GetComponent<AudioSystemController>();
        greenButton = collisionController.GetGreenButton();
        redButton = collisionController.GetRedButton();
        dialoguePanel.SetActive(false);
        portraitPanel.SetActive(false);
        bDialogueOpen = false;
        bChatOnGoing = false;
    }

    public void SetDialogue(string[] dialogue)
    {
        this.dialogue = dialogue;
    }

    public void SetPortrait(Sprite portrait)
    {
        this.portrait.sprite = portrait;
    }

    public void SetDialogueOpen(bool value)
    {
        bDialogueOpen = value;
    }

    public void SetIsNPC(bool bNPC)
    {
        this.bNPC = bNPC;
    }

    public GameObject getPortraitPanel()
    {
        return portraitPanel;
    }

    public bool IsChatGoing()
    {
        return bChatOnGoing;
    }

    public bool IsDialogueOpen()
    {
        return bDialogueOpen;
    }

    public void OpenDialogue()
    {
        if (bNPC)
        {
            //animator.Play("Luna_Talking",0);
            audioSystemController.PlayAudio();
            bDialogueOpen = true;
            bChatOnGoing = true;
            greenButton.interactable = false;
            if (dialoguePanel.activeInHierarchy)
            {
                if (index == dialogue.Length - 1)
                {
                    CloseDialogue();
                }
                NextLine();
            }
            else
            {
                redButton.interactable = true;
                dialoguePanel.SetActive(true);
                portraitPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        else
        {
            // Logic to interact with object
            audioSystemController.PlayAudio();
            
        }
    }

    public void CloseDialogue()
    {
        audioSystemController.StopAudio();
        bDialogueOpen = false;
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        portraitPanel.SetActive(false);
        index = 0;
    }

    public void NextLine()
    { 
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        audioSystemController.StopAudio();
        if (!bDialogueOpen)
        {
            dialogueText.text = "";
        }
        bChatOnGoing = false;
        if (collisionController.IsInside())
        {
            greenButton.interactable = true;
        }
    }
}
