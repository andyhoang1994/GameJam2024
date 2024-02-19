using UnityEngine;
public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    [SerializeField]
    private float timePerLevel = 30f;

    public float TotalTime { get; private set; }
    public float TimeRemaining { get; private set; }
    private bool TimerIsRunning { get; set; }

    private void Start()
    {
        int currentLevel = GameState.Instance.Level;
        float totalTime = this.timePerLevel * currentLevel;

        this.TotalTime = totalTime;
        this.TimeRemaining = totalTime;
        this.TimerIsRunning = true;
    }
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (this.TimerIsRunning)
        {
            if (this.TimeRemaining > 0)
            {
                this.TimeRemaining -= Time.deltaTime;
            }
            else
            {
                this.TimeRemaining = 0;
                this.TimerIsRunning = false;

                GameState.Instance.EndRound();
            }
        }
    }
}