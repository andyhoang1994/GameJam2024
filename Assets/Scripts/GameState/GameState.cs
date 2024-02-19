using UnityEngine;

public class GameState : MonoBehaviour
{
    private SceneLoader sceneLoader = new SceneLoader();
    public static GameState Instance { get; private set; }

    [SerializeField]
    private int level = 1;

    private SceneLoader SceneLoader { get { return this.sceneLoader; } }

    public int CurrentMoney { get; set; } = 0;

    public int RoundMoney { get; set; } = 0;

    public int GoalMoney { get; set; } = 10;

    public int Level { get { return level; } set { level = value; } }

    private void GameOver()
    {
        this.SceneLoader.LoadGameOverScene();
    }

    private void InitializeNextLevel()
    {
        this.Level += 1;
        this.CurrentMoney += this.RoundMoney - this.GoalMoney;
        this.RoundMoney = 0;
        this.GoalMoney = this.Level * 10;
    }

    private void Awake()
    {
        Instance = this;
    }
    public void AddMoney(int money)
    {
        this.CurrentMoney += money;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Update()
    {
        Time.timeScale = 1;
    }

    public void EndRound()
    {
        if (RoundMoney < GoalMoney)
        {
            this.GameOver();
        }
        else
        {
            this.InitializeNextLevel();
            GameStateUI.Instance.OpenStore();
        }
    }
}
