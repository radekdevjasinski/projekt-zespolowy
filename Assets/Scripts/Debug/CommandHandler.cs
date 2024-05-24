using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.SmartFormat;
using Exception = System.Exception;
[RequireComponent(typeof(CommandHelper))]
public class CommandHandler : MonoBehaviour
{
    private HashSet<CommandBase> _commands;
    private CommandHelper commandHelper;
    private IEnumerator corutine;
    [SerializeField] private LayerMask npcLayerMask;
    [SerializeField] private LayerMask doorsMask;
    private DebugController debugController;
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
        _commands.Add(new ActionCommand("flash", "sets speed of player to 3", "ghost", () =>
        {
            return commandHelper.setAttribute(3, PlayerAttributesController.attributes.SPEED);
        }));


        _commands.Add(new ArgumentCommand<float>("setspeed", "set attribute speed", "setspeed {amount}", (float val) =>
        {
            return commandHelper.setAttribute(val, PlayerAttributesController.attributes.SPEED);
            }));
        _commands.Add(new ArgumentCommand<float>("sethealth", "set player health", "sethealth {amount}", (float val) =>
        {
            return commandHelper.setAttribute(val, PlayerAttributesController.attributes.HEALTH);
        }));
        _commands.Add(new ArgumentCommand<float>("setdamage", "sets how much player deals damage", "setdamage {amount}", (float val) =>
        {
            return commandHelper.setAttribute(val, PlayerAttributesController.attributes.DAMAGE);
        }));
        _commands.Add(new ArgumentCommand<float>("setfirerate", "sets how fast player is able to attack", "setfirerate {amount}", (float val) =>
        {
            return commandHelper.setAttribute(val, PlayerAttributesController.attributes.FIRE_RATE);
        }));
        _commands.Add(new ActionCommand("heal", "heals player to max health", "heal", () =>
        {
            commandHelper.getPlayer().GetComponent<PlayerAttributesController>().resetHealth();
            return "healed player fully";
        }));
        _commands.Add(new ActionCommand("haggle", "resets trades with nearby npc", "haggle", () =>
        {
            Collider2D[] objectsNearby=new Collider2D[20];
           ContactFilter2D contactFilter = new ContactFilter2D();
           contactFilter.layerMask = npcLayerMask;
    
           int amt= Physics2D.OverlapCircle(commandHelper.getPlayer().transform.position, 5, contactFilter, objectsNearby);
           Debug.Log("player postion nearby " + commandHelper.getPlayer().transform.position);
            Debug.Log("Object nearby " + amt);
           amt = 0;
            foreach (Collider2D var in objectsNearby)
            {
                NpcTrader trader;
                if (var!=null && var.CompareTag("NPC") && var.TryGetComponent<NpcTrader>(out trader))
                {
                    amt++;
                    trader.resetItems(); ;
                }
            }
            return "resets trades for "+ amt+" npcs";
        }));
        _commands.Add(new ActionCommand("alohomora", "opens nearby doors", "alohomora", () =>
        {
            Collider2D[] objectsNearby = new Collider2D[20];
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.layerMask = doorsMask;
            contactFilter.useTriggers = true;
            int amt = Physics2D.OverlapCircle(commandHelper.getPlayer().transform.position, 3, contactFilter, objectsNearby);

            Debug.Log("Object nearby " + amt);
            int doorsamt = 0;

            for (int i = 0; i < amt; i++)
            {
                Collider2D var = objectsNearby[i];
                Debug.Log("Object nearby " + var != null ? var.name : "null");
                Door doors;

                if (var != null && var.TryGetComponent<Door>(out doors))
                {
                    doors.openDoor();
                    doorsamt++;
                }

            }
            return "opened " + doorsamt + " doors";
        }));

        _commands.Add(new ActionCommand("l", "teleport player to left room, Cautions overuse might break Camera", "L/l", () =>
        {
            return commandHelper.teleportPlayer(0);
        }));
        _commands.Add(new ActionCommand("r", "teleport player to right room, Cautions overuse might break Camera", "R/r", () =>
        {
            return commandHelper.teleportPlayer(2);
        }));
        _commands.Add(new ActionCommand("u", "teleport player to up room, Cautions overuse might break Camera", "U/u", () =>
        {
            return commandHelper.teleportPlayer(1);
        }));
        _commands.Add(new ActionCommand("d", "teleport player to down room, Cautions overuse might break Camera", "D/d", () =>
        {
            return commandHelper.teleportPlayer(3);
        }));


        _commands.Add(new ActionCommand("kill", "deals 100.0 points of damge to nerby enteties", "kill", () =>
        {
            Collider2D[] objectsNearby = new Collider2D[20];
            ContactFilter2D contactFilter = new ContactFilter2D();
            int amt = Physics2D.OverlapCircle(commandHelper.getPlayer().transform.position, 10, contactFilter, objectsNearby);

           // Debug.Log("Object nearby " + amt);
            int entetyiAmt = 0;

            for (int i = 0; i < amt; i++)
            {
                Collider2D var = objectsNearby[i];
                //Debug.Log("Object nearby " + var != null ? var.name : "null");
                EntityController<float> enController;

                if (var != null && var.TryGetComponent<EntityController<float>>(out enController))
                {
                    enController.dealDamage(100);
                    entetyiAmt++;
                }

            }
            return "killed " + entetyiAmt + " enteties";
        }));
        _commands.Add(new ActionCommand("chest", "rerolls chest Spawner in current room", "chest", () =>
        {
            ChestSpawner[] spanwers = DungeonGenerator.instance.getCurrRoom().gameObject
                .GetComponentsInChildren<ChestSpawner>();
           
            int chestiAmt = spanwers.Length;

            foreach (ChestSpawner chestSpawner in spanwers)
            {
                chestSpawner.SpawnChest();
            }
            
            return "chest spanwer rerolled " + chestiAmt + " chest";
        }));
        _commands.Add(new ActionCommand("speedrun", "gives player inviniclity, spped 3 and damgage 20", "speedrun", () =>
        {
            commandHelper.setAttribute(20, PlayerAttributesController.attributes.DAMAGE);
            commandHelper.getPlayer().GetComponent<PlayerItemsController>().GetComponent<PlayerEntityController>().setIsInvurnable(!commandHelper.getPlayer().GetComponent<PlayerItemsController>().GetComponent<PlayerEntityController>().getIsInvurnable());
            commandHelper.setAttribute(3, PlayerAttributesController.attributes.SPEED);
            return "set speed run values for debbuging";
        }));
    }



    public string handleCommand(string input)
    {
        input=input.ToLower();
        //try
        //{
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
                if (command as ArgumentCommand<float> != null)
                {
                    return (command as ArgumentCommand<float>).invoke(float.Parse(splits[1]));
                }
            }
            

    //}
    //    catch (Exception e)
    //    {
    //        return "Exception: "+ e.Message;
    //    }

return "command not found, you can enter help";
    }

   

}
