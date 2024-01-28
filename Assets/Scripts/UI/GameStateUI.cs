using UnityEngine;
using UnityEngine.UI;

public class GameStateUI : MonoBehaviour
{
    const int DEGREES_IN_CIRCLE = 360;

    [SerializeField]
    private Transform hand;

    [SerializeField]
    private Slider progressBar;

    private Slider ProgressBar { get { return progressBar; } }
    private Transform Hand { get { return hand; } }

    private void UpdateTimer()

    {
        float totalTime = Timer.Instance.TotalTime;
        float timeRemaining = Timer.Instance.TimeRemaining;

        float angle = DEGREES_IN_CIRCLE * (timeRemaining / totalTime) - DEGREES_IN_CIRCLE;

        Vector3 handPosition = this.Hand.position;
        this.Hand.SetPositionAndRotation(handPosition, Quaternion.AngleAxis(angle, Vector3.forward));
    }

    private void UpdateMoney()
    {
        float currentMoney = GameState.Instance.CurrentMoney;
        this.ProgressBar.value = currentMoney;
    }

    private void Start()
    {
        float goalMoney = GameState.Instance.GoalMoney;
        this.ProgressBar.maxValue = goalMoney;
        this.ProgressBar.value = 0;
    }

    private void Update()
    {
        this.UpdateTimer();
        this.UpdateMoney();
    }
}
