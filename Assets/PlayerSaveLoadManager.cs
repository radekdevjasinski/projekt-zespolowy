using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveLoadManager : MonoBehaviour
{
    void Start()
    {
        if (SaveLoadManager.instance.GameLoaded)
        {
            GameData gameData = SaveLoadManager.instance.LoadGame();
            PlayerAttributesController playerAttributesController = GetComponent<PlayerAttributesController>();

            playerAttributesController.setAttrib(PlayerAttributesController.attributes.MAX_HEALTH, gameData.player.MaxHealth);
            playerAttributesController.setAttrib(PlayerAttributesController.attributes.MAX_HEALTH, gameData.player.MaxHealth);
            playerAttributesController.setAttrib(PlayerAttributesController.attributes.HEALTH, gameData.player.health);
            playerAttributesController.setAttrib(PlayerAttributesController.attributes.MAX_STAMINA, gameData.player.MaxStamina);
            playerAttributesController.setAttrib(PlayerAttributesController.attributes.STAMINA, gameData.player.stamina);
            playerAttributesController.setAttrib(PlayerAttributesController.attributes.ARMOR, gameData.player.armor);
            playerAttributesController.setAttrib(PlayerAttributesController.attributes.SPEED, gameData.player.speed);
            playerAttributesController.setAttrib(PlayerAttributesController.attributes.FIRE_RATE, gameData.player.fireRate);
            playerAttributesController.setAttrib(PlayerAttributesController.attributes.DAMAGE, gameData.player.damage);
            playerAttributesController.setAttrib(PlayerAttributesController.attributes.RANGE, gameData.player.range);

            Vector3 playerPos = new Vector3(gameData.currentRoom.x * DungeonGenerator.instance.roomSpace,
                gameData.currentRoom.y * DungeonGenerator.instance.roomSpace);
            transform.position = playerPos;

            PlayerItemsController playerItemsController = GetComponent<PlayerItemsController>();
            playerItemsController.SetBombs(gameData.items.bombs);
            playerItemsController.SetKeys(gameData.items.keys);
            playerItemsController.SetCoins(gameData.items.coins);
            playerItemsController.SetHealthPotions(gameData.items.healthPotions);

        }
    }
}
