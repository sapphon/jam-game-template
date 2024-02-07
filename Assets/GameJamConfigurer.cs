using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class GameJamConfigurer : MonoBehaviour
{
    private GameObject playerObject;
    private GameObject enemiesObject;
    private AnimationController[] enemyControllers;
    private PlayerController playerController;

    public float playerSpeedMultiplier = 1.0f;
    public float playerJumpMultiplier = 1.0f;
    public float enemySpeedMultiplier = 1.0f;
    public bool playerCanBop = true;
    public bool[] enemiesEnabled = new[] { true };
    
    void Start()
    {
        this.playerObject = GameObject.Find("Player");
        this.playerController = this.playerObject.GetComponent<PlayerController>();
        this.enemiesObject = GameObject.Find("Enemies");
        this.enemyControllers = enemiesObject.GetComponentsInChildren<AnimationController>();

        setPlayerSpeed();
        setPlayerJump();
        setEnemySpeeds();
        setBopStatus();
    }

    private void setBopStatus()
    {
        this.playerController.canBopEnemies = playerCanBop;
    }

    private void setPlayerJump()
    {
        this.playerController.jumpTakeOffSpeed *= playerJumpMultiplier;
    }

    private void setEnemySpeeds()
    {
        foreach (AnimationController controller in this.enemyControllers)
        {
            controller.maxSpeed *= enemySpeedMultiplier;
        }
    }

    private void setPlayerSpeed()
    {
        this.playerController.maxSpeed *= playerSpeedMultiplier;
    }
}
