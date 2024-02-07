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
    private GameObject tokensObject;
    private SpriteRenderer[] tokens;

    public float playerSpeedMultiplier = 1.0f;
    public float playerJumpMultiplier = 1.0f;
    public float enemySpeedMultiplier = 1.0f;
    public bool playerCanBop = true;
    public bool enemiesEnabled = true;
    public bool tokensEnabled = true;

    void Start()
    {
        this.playerObject = GameObject.Find("Player");
        this.playerController = this.playerObject.GetComponent<PlayerController>();
        this.enemiesObject = GameObject.Find("Enemies");
        this.enemyControllers = enemiesObject.GetComponentsInChildren<AnimationController>();
        this.tokensObject = GameObject.Find("Tokens");
        this.tokens = tokensObject.GetComponentsInChildren<SpriteRenderer>();

        setPlayerSpeed();
        setPlayerJump();
        setEnemySpeeds();
        setBopStatus();
        if (!enemiesEnabled)
        {
            disableEnemies();
        }
        if (!tokensEnabled)
        {
            disableTokens();
        }
    }

    private void disableTokens()
    {
        foreach (var controller in this.tokens)
        {
            controller.gameObject.SetActive(false);
        }
    }

    private void disableEnemies()
    {
        foreach (AnimationController controller in this.enemyControllers)
        {
            controller.gameObject.SetActive(false);
        }
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
