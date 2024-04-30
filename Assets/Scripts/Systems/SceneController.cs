using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [SerializeField] private Animator transitionAnim;
    private string sceneName;
    private Transform playerTransform;
    private DialogueController dialogueController;
    private PlayerController playerController;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.gameObject.SetActive(false);
    }

    void Awake()
    {
        dialogueController = GameObject.Find("Systems/DialogueController").GetComponent<DialogueController>();
    }

    public void SetScene(string sceneName)
    {
        this.sceneName = sceneName;
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneDelay());
    }

    IEnumerator LoadSceneDelay()
    {
        Debug.Log(sceneName);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
        dialogueController.GetPlayerControllerPanel().SetActive(true);
        playerController.GetPlayer().SetActive(true);
        if(sceneName.Equals("RedHouse")) playerTransform.position = new Vector3(0.0722465217f, -4.08670235f);
        else if (sceneName.Equals("PixelScene")) playerTransform.position = new Vector3(1.47000003f, -0.579999983f);
        transitionAnim.SetTrigger("Start");
    }
}


