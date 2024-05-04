using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { IDLE, START, PROGRESS, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject attackVFX;

    public Sprite textureCrying;

    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer enemySpriteRenderer;

    private Transform playerBattleStation;
    private Transform enemyBattleStation;

    private Unit playerUnit;
    private Unit enemyUnit;

    private BattleHUD playerHUD;
    private BattleHUD enemyHUD;

    private Text dialogueText;

    public BattleState state;

    // System Controllers
    SceneController sceneController;
    AudioController audioController;
    DialogueController dialogueController;
    VFXController VFXController;

    void Awake()
    {
        sceneController = GameObject.Find("Systems/SceneController").GetComponent<SceneController>();
        audioController = GameObject.Find("Systems/AudioController").GetComponent<AudioController>();
        VFXController = GameObject.Find("Systems/VFXController").GetComponent<VFXController>();
        dialogueController = GameObject.Find("Systems/DialogueController").GetComponent<DialogueController>();
    }

    void Start()
    {
        state = BattleState.IDLE;
        playerHUD = GameObject.Find("Canvas/PlayerBattleHud_Battle").GetComponent<BattleHUD>();
        enemyHUD = GameObject.Find("Canvas/EnemyBattleHud_Battle").GetComponent<BattleHUD>();

        playerBattleStation = GameObject.Find("PlayerBattleStation").GetComponent<Transform>();
        enemyBattleStation = GameObject.Find("EnemyBattleStation").GetComponent<Transform>();
        dialogueText = GameObject.Find("Canvas/DialoguePanel_Battle/DialogueText").GetComponent<Text>();

        GameObject player = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = player.GetComponent<Unit>();

        GameObject enemy = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemy.GetComponent<Unit>();

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("BattleScene") && state == BattleState.IDLE)
        {
            StartCoroutine(SetupBattle());
        }    
    }

    private IEnumerator SetupBattle()
    {
        state = BattleState.START;
        dialogueText.text = "A Wild " + enemyUnit.name + " has appeared!";
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        playerSpriteRenderer = GameObject.Find("PlayerBattleStation/Player(Clone)/image").GetComponent<SpriteRenderer>();
        enemySpriteRenderer = GameObject.Find("EnemyBattleStation/Enemy(Clone)/pixil-frame-0_2").GetComponent<SpriteRenderer>();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private IEnumerator PlayerAttack()
    {
        state = BattleState.PROGRESS;
        audioController.PlayAttack();
        VFXController.PlayParticleAttack();
        yield return new WaitForSeconds(0.1f);
        enemySpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.1f);
        enemySpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.1f);
        enemySpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.1f);
        enemySpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.1f);
        enemySpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.1f);
        enemySpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
 

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHealth);
        dialogueText.text = "The attack is successful!";
        
        yield return new WaitForSeconds(2f);
        
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        
    }

    private void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    IEnumerator EnemyTurn()
    {
        audioController.PlayAttack();
        dialogueText.text = enemyUnit.name + " attacks!";
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHealth);

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }


    private void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";

            dialogueController.CloseBattleHUD();
            sceneController.SetScene("PixelScene");
            sceneController.LoadScene();
        }
        else
        {
            dialogueController.CloseBattleHUD();
            dialogueText.text = "You were defeated!";
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)  return;

        StartCoroutine(PlayerAttack());
    }
}
