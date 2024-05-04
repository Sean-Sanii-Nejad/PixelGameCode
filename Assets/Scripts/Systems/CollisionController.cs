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
    private AbilitySystemController abilitySystemController;
    private UIController UIController;
    private VFXController VFXController;

    public float effectTriggerInterval = 1f; 
    public float probability = 0.2f; 
    private bool isInsideDebuff = false;

    private Coroutine effectCoroutine;

    void Awake()
    {
        // UI
        greenButton = GameObject.Find("Canvas/PlayerControllerPanel/Ok").GetComponent<Button>();
        redButton = GameObject.Find("Canvas/PlayerControllerPanel/Back").GetComponent<Button>();
        
        bInside = false;
        greenButton.interactable = false;
        redButton.interactable = false;

        // System Controllers
        dialogueController = GameObject.Find("Systems/DialogueController").GetComponent<DialogueController>();
        audioSystemController = GameObject.Find("Systems/AudioController").GetComponent<AudioController>();
        interactionController = GameObject.Find("Systems").GetComponent<InteractionController>();
        sceneController = GameObject.Find("Systems/SceneController").GetComponent<SceneController>();
        abilitySystemController = GameObject.Find("Systems/AbilitySystemController").GetComponent<AbilitySystemController>();
        UIController = GameObject.Find("Systems/UIController").GetComponent<UIController>();
        VFXController = GameObject.Find("Systems/VFXController").GetComponent<VFXController>();
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
        if (other.CompareTag("Debuff"))
        {
            isInsideDebuff = true;
            audioSystemController.PlayDebuffSlow();
            UIController.SetDebuffSymbol(true);
            VFXController.PlayParticleSystem();
            if (effectCoroutine == null)
                effectCoroutine = StartCoroutine(TriggerEffect());
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

        if (other.CompareTag("Debuff"))
        {
            isInsideDebuff = false;
            abilitySystemController.RemoveDebuff(AbilitySystemController.DebuffType.SLOW, 0f);
            audioSystemController.StopDebuffSlow();
            UIController.SetDebuffSymbol(false);
            VFXController.StopParticleSystem();
            if (effectCoroutine != null)
            {
                StopCoroutine(effectCoroutine);
                effectCoroutine = null;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Debuff"))
        {
            abilitySystemController.TriggerDebuff(AbilitySystemController.DebuffType.SLOW, 0f);
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

    private IEnumerator TriggerEffect()
    {
        while (isInsideDebuff)
        {
            Debug.Log(Random.value);
            if (Random.value < probability)
            {
                sceneController.SetScene("BattleScene");
                sceneController.LoadScene();
            }
            yield return new WaitForSeconds(effectTriggerInterval);
        }
    }
}
