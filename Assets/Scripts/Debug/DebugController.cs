using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class DebugController : MonoBehaviour
{
    [SerializeField] private TMP_Text textOuptut;
    [SerializeField] private TMP_InputField textInput;
    [SerializeField] private ScrollRect scrollView;
    private Controls controls;
    private InputAction Enter;
    private InputAction OpenConsole;
    private Transform Canvas;
    private CommandHandler commandHandler;

    private void Awake()
    {
        controls = new Controls();
        Canvas=transform.Find("Canvas");
        Canvas.gameObject.active = false;
        commandHandler = GetComponent<CommandHandler>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(Canvas as RectTransform);
    }

    void OnEnable()
    {
        this.controls.Debug.Enable();
        Enter = controls.Debug.Enter;
        Enter.canceled += ctx => {onEnter(); };

        OpenConsole = controls.Debug.OpenConsole;
        OpenConsole.performed += ctx => { onOpenConsole(); };
    }
    void OnDisable()
    {
        this.controls.Debug.Disable();
    }
    private void onOpenConsole()
    {
        Debug.Log("Open console");
       
        if (!Canvas.gameObject.active)
        {
            Canvas.gameObject.active = true;
            textInput.ActivateInputField();
            GameControler.instance.pauseGame();
        }
        else
        {
           hideConsole();
        }
    }

    private void onEnter()
    {
        Debug.Log("ENTER");
        if (Canvas.gameObject.active)
        {
            string input = textInput.text;
            textInput.text = "";
            textOuptut.text +="\n"+ input;
            textOuptut.text += "\n" + commandHandler.handleCommand(input) ;
            LayoutRebuilder.ForceRebuildLayoutImmediate(scrollView.content);
            textInput.ActivateInputField();
        }
    }


    void Start()
    {

        
    }
    public void hideConsole()
    {
        GameControler.instance.resumeGame();
        Canvas.gameObject.active = false;
        textInput.DeactivateInputField();
    }
}
