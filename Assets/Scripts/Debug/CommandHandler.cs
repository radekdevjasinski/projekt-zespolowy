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

        _commands.Add(new ActionCommand("cashback", "adds 10 coins to player","cashback", () =>
        {
            commandHelper.getPlayer().GetComponent<PlayerItemsController>().addCoins(10);
            return "Added 10 coins";
        }));
        _commands.Add(new ActionCommand("nobel", "adds 10 bombs to player", "nobel", () =>
        {
            commandHelper.getPlayer().GetComponent<PlayerItemsController>().addBomb(10);
            return "Added 10 bombs";
        }));
        _commands.Add(new ActionCommand("thief", "adds 10 keys to player", "thief", () =>
        {
            commandHelper.getPlayer().GetComponent<PlayerItemsController>().addKey(10);
            return "Added 10 keys";
        }));
        _commands.Add(new ActionCommand("mercy", "adds 10 health potions to player", "mercy", () =>
        {
            commandHelper.getPlayer().GetComponent<PlayerItemsController>().addHelathPotion(10);
            return "Added 10 keys";
        }));

        _commands.Add(new ArgumentCommand<int>("setcoins", "sets amount of coins Player has", "setcoins {amount}", (int val) =>
        {
            if (val >= 0)
            {
                commandHelper.getPlayer().GetComponent<PlayerItemsController>().addCoins(val-commandHelper.getPlayer().GetComponent<PlayerItemsController>().getCoinsAmount());
                return "set coins to "+ val;

            }
            else
            {
                return "value should not be less tahn zero";
            }

        }));
        _commands.Add(new ArgumentCommand<int>("setbombs", "sets amount of coins bombs has", "setbombs {amount}", (int val) =>
        {
            if (val >= 0)
            {
                commandHelper.getPlayer().GetComponent<PlayerItemsController>().addBomb(val - commandHelper.getPlayer().GetComponent<PlayerItemsController>().getBombs());
                return "set bombs to " + val;

            }
            else
            {
                return "value should not be less tahn zero";
            }

        }));
        _commands.Add(new ArgumentCommand<int>("setkeys", "sets amount of keys Player has", "setkeys {amount}", (int val) =>
        {
            if (val >= 0)
            {
                commandHelper.getPlayer().GetComponent<PlayerItemsController>().addKey(val - commandHelper.getPlayer().GetComponent<PlayerItemsController>().getKeys());
                return "set keys to " + val;

            }
            else
            {
                return "value should not be less tahn zero";
            }

        }));
        _commands.Add(new ArgumentCommand<int>("setpotions", "sets amount of health potions Player has", "setpotions {amount}", (int val) =>
        {
            if (val >= 0)
            {
                commandHelper.getPlayer().GetComponent<PlayerItemsController>().addHelathPotion(val - commandHelper.getPlayer().GetComponent<PlayerItemsController>().getHelathPotion());
                return "set health potions to " + val;

            }
            else
            {
                return "value should not be less tahn zero";
            }

        }));

        _commands.Add(new ActionCommand("god", "makes player invurnable/vurnable to damage", "godmode", () =>
        {
            commandHelper.getPlayer().GetComponent<PlayerItemsController>().GetComponent<PlayerEntityController>().setIsInvurnable(!commandHelper.getPlayer().GetComponent<PlayerItemsController>().GetComponent<PlayerEntityController>().getIsInvurnable());
            if (commandHelper.getPlayer().GetComponent<PlayerItemsController>().GetComponent<PlayerEntityController>()
                .getIsInvurnable())
            {
                return "god mode ON";
            }
            return "god mode OFF";
        }));
        _commands.Add(new ActionCommand("ghost", "removes collsion from player", "ghost", () =>
        {
            commandHelper.getPlayer().GetComponent<Collider2D>().enabled = !commandHelper.getPlayer().GetComponent<Collider2D>().enabled;
            if (!commandHelper.getPlayer().GetComponent<Collider2D>().enabled)
            {
                return "ghost mode ON";
            }
            return "ghost mode OFF";
        }));

    }



    public string handleCommand(string input)
    {
        input=input.ToLower();
        try
        {
            CommandBase command;
            string[] splits = input.Split(' ');
            if (_commands.TryGetValue(new CommandBase(splits[0]), out command))
            {
                if (command as ActionCommand != null)
                {
                    return (command as ActionCommand).invoke();
                }

                if (command as ArgumentCommand<int> != null)
                {
                    return (command as ArgumentCommand<int>).invoke(Int32.Parse(splits[1]));
                }
            }
            

    }
        catch (Exception e)
        {
            return "Exception: "+ e.Message;
        }

return "command not found, you can enter help";
    }

}
