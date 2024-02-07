using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using TMPro;
using UnityEngine;

public class GameJamConfigurer : MonoBehaviour
{
    public struct GameplayConfigData
    {
        public float playerSpeedMultiplier;
        public float playerJumpMultiplier;
        public float enemySpeedMultiplier;
        public bool playerCanBop;
        public bool enemiesEnabled;
        public bool tokensEnabled;
        
        public static GameplayConfigData CreateFromJson(string json)
        {
            return JsonUtility.FromJson<GameplayConfigData>(json);
        }
    }
    
    public struct PlotBeatConfigData
    {
        public float x;
        public float y;
        public string text;
        
        public static PlotBeatConfigData CreateFromJson(string json)
        {
            return JsonUtility.FromJson<PlotBeatConfigData>(json);
        }
    }
    
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
    private GameObject plotBeatsObject;
    private PlotBeat[] plotBeats;

    void Start()
    {
        this.playerObject = GameObject.Find("Player");
        this.playerController = this.playerObject.GetComponent<PlayerController>();
        this.enemiesObject = GameObject.Find("Enemies");
        this.enemyControllers = enemiesObject.GetComponentsInChildren<AnimationController>();
        this.tokensObject = GameObject.Find("Tokens");
        this.plotBeatsObject = GameObject.Find("PlotBeats");
        this.plotBeats = plotBeatsObject.GetComponentsInChildren<PlotBeat>();
        this.tokens = tokensObject.GetComponentsInChildren<SpriteRenderer>();

        readFromConfigFile();
        configurePlotBeats();
        
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

    private void configurePlotBeats()
    {
        for(int i = 0; i < 3; i++)
        {
            PlotBeatConfigData data = readFromPlotBeatsFile(i + 1);
            plotBeats[i].transform.position = new Vector3(data.x, data.y, 0);
            plotBeats[i].text.GetComponent<TextMeshProUGUI>().text = data.text;
        }
    }

    private PlotBeatConfigData readFromPlotBeatsFile(int fileNumber)
    {
        string json = Resources.Load<TextAsset>("plot" + fileNumber.ToString()).text;
        return PlotBeatConfigData.CreateFromJson(json);
    }

    private void readFromConfigFile()
    {
        string json = Resources.Load<TextAsset>("config").text;
        GameplayConfigData data = GameplayConfigData.CreateFromJson(json);
        playerSpeedMultiplier = data.playerSpeedMultiplier;
        playerJumpMultiplier = data.playerJumpMultiplier;
        enemySpeedMultiplier = data.enemySpeedMultiplier;
        playerCanBop = data.playerCanBop;
        enemiesEnabled = data.enemiesEnabled;
        tokensEnabled = data.tokensEnabled;
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
