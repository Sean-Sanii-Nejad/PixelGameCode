using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [SerializeField] private Animator transitionAnim;
    private SceneAsset sceneAsset;
    private Transform playerTransform;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void SetScene(SceneAsset sceneAsset)
    {
        this.sceneAsset = sceneAsset;
    }

    public void LoadScene()
    {
        StartCoroutine(LoadRedHouse());
    }

    IEnumerator LoadRedHouse()
    {
        Debug.Log(sceneAsset.name);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneAsset.name);
        playerTransform.position = new Vector3(0.0722465217f, -4.08670235f);
        transitionAnim.SetTrigger("Start");
    }
}
