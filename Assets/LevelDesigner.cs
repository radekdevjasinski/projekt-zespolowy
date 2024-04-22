using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesigner : MonoBehaviour
{
    private PlayerTeleporter playerTeleporter;
    private GameObject player;
    private DungeonGenerator dungeonGenerator;
    void Start()
    {
        player = GameObject.Find("Player");
        playerTeleporter = player.GetComponent<PlayerTeleporter>();
        dungeonGenerator = gameObject.GetComponent<DungeonGenerator>();
    }
    public void PrepareRoom(DungeonRoom activePlayerRoom, float controlsOffTime)
    {
        if (activePlayerRoom.enemiesCount > 0)
        {
            activePlayerRoom.CloseAllDoors();
            SpawnEnemies();
        }
        player.GetComponent<PlayerControler>().lockInput();
        StartCoroutine(TurnOnControls(controlsOffTime));

    }
    IEnumerator TurnOnControls(float controlsOffTime)
    {
        yield return new WaitForSeconds(controlsOffTime);
        player.GetComponent<PlayerControler>().unlockInput();
        WakeUpEnemies();

    }
    public void WakeUpEnemies()
    {
        if (playerTeleporter.activePlayerRoom != null)
        {
            EnemyBase[] enemies = playerTeleporter.activePlayerRoom.gameObject.GetComponentsInChildren<EnemyBase>();
            foreach (EnemyBase enemy in enemies)
            {
                enemy.LockMovement = false;
            }
        }
    }
    public void SpawnEnemies()
    {
        if (playerTeleporter.activePlayerRoom != null)
        {
            AddEnemy[] roomsWithEnemies = playerTeleporter.activePlayerRoom.gameObject.GetComponentsInChildren<AddEnemy>();
            foreach (AddEnemy room in roomsWithEnemies)
            {
                room.Spawn();
            }
        }
    }
    public void EnemyDied()
    {
        if (playerTeleporter.activePlayerRoom != null)
        {
            playerTeleporter.activePlayerRoom.enemiesCount--;
            if (playerTeleporter.activePlayerRoom.enemiesCount <= 0)
            {
                playerTeleporter.activePlayerRoom.OpenRightDoors(dungeonGenerator.rooms);
            }
        }
    }

}
