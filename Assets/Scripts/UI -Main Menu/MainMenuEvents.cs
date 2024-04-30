using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument document;
    private Button button;
    private SceneController sceneController;

    void Start()
    {
        sceneController = GameObject.Find("Systems/SceneController").GetComponent<SceneController>();
    }

    void Awake()
    {
        document = GetComponent<UIDocument>();
        button = document.rootVisualElement.Q("StartButton") as Button;
        button.RegisterCallback<ClickEvent>(OnPlayGameClick);
    }

    private void OnDisable()
    {
        button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        sceneController.SetScene("PixelScene");
        sceneController.LoadScene();
    }
}
