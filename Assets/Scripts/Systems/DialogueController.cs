using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public float wordSpeed;
    public Animator animator;

    // UI
    [SerializeField]
    private string[] dialogue;
    [SerializeField]
    private Image portrait;
    private int index;
    
    private GameObject dialoguePanel;
    private GameObject portraitPanel;
    private GameObject playerControllerPanel;

    private GameObject playerBattleHUD_Battle;
    private GameObject enemyBattleHUD_Battle;
    private GameObject dialoguePanel_Battle;

    private GameObject playerBattleStation;
    private GameObject enemyBattleStation;

    private Text dialogueText;
    
    private bool bDialogueOpen;
    private bool bChatOnGoing;

    // System Controllers
    private AudioController audioController;
    private InteractionController interactionController;
    private CollisionController collisionController;

    void Start()
    {
        bDialogueOpen = false;
        bChatOnGoing = false;
        dialoguePanel.SetActive(false);
        portraitPanel.SetActive(false);
        playerControllerPanel.SetActive(false);

        playerBattleHUD_Battle.SetActive(false);
        enemyBattleHUD_Battle.SetActive(false);
        dialoguePanel_Battle.SetActive(false);

        playerBattleStation.SetActive(false);
        enemyBattleStation.SetActive(false);
    }

    void Awake()
    {
        // UI
        dialoguePanel = GameObject.Find("Canvas/DialoguePanel");
        portraitPanel = GameObject.Find("Canvas/PortraitPanel");
        playerControllerPanel = GameObject.Find("Canvas/PlayerControllerPanel");

        playerBattleHUD_Battle = GameObject.Find("Canvas/PlayerBattleHud_Battle");
        enemyBattleHUD_Battle = GameObject.Find("Canvas/EnemyBattleHud_Battle");
        dialoguePanel_Battle = GameObject.Find("Canvas/DialoguePanel_Battle");

        portrait = GameObject.Find("Canvas/PortraitPanel/Portrait").GetComponent<Image>();
        dialogueText = GameObject.Find("Canvas/DialoguePanel/Text").GetComponent<Text>();

        playerBattleStation = GameObject.Find("PlayerBattleStation");
        enemyBattleStation = GameObject.Find("EnemyBattleStation");
        


        // System Controllers
        collisionController = GameObject.Find("Player").GetComponent<CollisionController>();
        audioController = GameObject.Find("Systems/AudioController").GetComponent<AudioController>();
        interactionController = GameObject.Find("Systems").GetComponent<InteractionController>();
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

    public GameObject GetPortraitPanel()
    {
        return portraitPanel;
    }

    public GameObject GetPlayerControllerPanel()
    {
        return playerControllerPanel;
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
        audioController.PlayAudio();
        bDialogueOpen = true;
        bChatOnGoing = true;
        interactionController.SetInteractableButton(InteractionController.ButtonType.GREEN, false);
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
            interactionController.SetInteractableButton(InteractionController.ButtonType.RED, true);
            dialoguePanel.SetActive(true);
            portraitPanel.SetActive(true);
            StartCoroutine(Typing());
        }  
    }

    public void CloseDialogue()
    {
        audioController.StopAudio();
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
        audioController.StopAudio();
        if (!bDialogueOpen)
        {
            dialogueText.text = "";
        }
        bChatOnGoing = false;
        if (collisionController.IsInside())
        {
            interactionController.SetInteractableButton(InteractionController.ButtonType.GREEN, true);
        }
    }

    public void OpenBattleHUD()
    {
        playerBattleHUD_Battle.SetActive(true);
        enemyBattleHUD_Battle.SetActive(true);
        dialoguePanel_Battle.SetActive(true);
        
        playerBattleStation.SetActive(true);
        enemyBattleStation.SetActive(true);
    }

    public void CloseBattleHUD()
    {
        playerBattleHUD_Battle.SetActive(false);
        enemyBattleHUD_Battle.SetActive(false);
        dialoguePanel_Battle.SetActive(false);

        playerBattleStation.SetActive(false);
        enemyBattleStation.SetActive(false);
    }

    public void ClosePlayerControllerPanel()
    {
        playerControllerPanel.SetActive(false);
    }
}
