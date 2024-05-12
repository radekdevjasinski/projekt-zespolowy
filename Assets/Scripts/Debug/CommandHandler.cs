using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using Exception = System.Exception;
[RequireComponent(typeof(CommandHelper))]
public class CommandHandler : MonoBehaviour
{
    private HashSet<CommandBase> _commands;
    private CommandHelper commandHelper;
    private void Awake()
    {
        commandsSetup();
        commandHelper = GetComponent<CommandHelper>();
    }

    private void commandsSetup()
    {
        _commands=new HashSet<CommandBase>();

        _commands.Add(new ActionCommand("help", "Prints list of all possible commands", "help", () =>
        {
            StringBuilder sb = new StringBuilder();
            foreach (CommandBase com in _commands)
            {
                sb.Append(com + "\n");
            }
            return sb.ToString();
        }));

        _commands.Add(new ActionCommand("help", "Prints list of all possible commands", "help", () =>
        {
            StringBuilder sb = new StringBuilder();
            foreach (CommandBase com in _commands)
            {
                sb.Append(com + "\n");
            }
            return sb.ToString();
        }));

    }



    public string handleCommand(string input)
    {
        input=input.ToLower();
        try
        {
            CommandBase command;
            if (_commands.TryGetValue(new CommandBase(input), out command))
            {
                if (command as ActionCommand != null)
                {
                    return (command as ActionCommand).invoke();
                }

                Debug.Log("foudn command");
        }

    }
        catch (Exception e)
        {
            return "Exception: "+ e.Message;
        }

return "command not found, you can enter help";
    }

}
