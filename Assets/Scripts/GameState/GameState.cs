using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [SerializeField]
    private int level = 1;

    public int CurrentMoney { get; set; } = 0;

    public int RoundMoney { get; set; } = 0;

    public int GoalMoney { get; set; } = 10;

    public int Level { get { return level; } set { level = value; } }

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
}
