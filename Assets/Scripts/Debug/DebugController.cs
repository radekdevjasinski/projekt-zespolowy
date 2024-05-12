using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CommandHandler))]
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
        Canvas.gameObject.active = !Canvas.gameObject.active;
        if (Canvas.gameObject.active)
        {
            textInput.ActivateInputField();
        }
        else
        {
            textInput.DeactivateInputField();
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

}
