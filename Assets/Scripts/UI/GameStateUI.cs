using UnityEngine;
using UnityEngine.UI;

public class GameStateUI : MonoBehaviour
{
    const int DEGREES_IN_CIRCLE = 360;

    [SerializeField]
    private Transform hand;

    [SerializeField]
    private Slider progressBar;

    [SerializeField]
    private Image progressOverlay;

    private Slider ProgressBar { get { return this.progressBar; } set { this.progressBar = value; } }

    private Transform Hand { get { return this.hand; } set { this.hand = value; } }

    private Image ProgressOverlay { get { return this.progressOverlay; } set { this.progressOverlay = value; } }

    private void UpdateTimer()

    {
        float totalTime = Timer.Instance.TotalTime;
        float timeRemaining = Timer.Instance.TimeRemaining;

        float timePercentage = timeRemaining / totalTime;
        float angle = DEGREES_IN_CIRCLE * timePercentage - DEGREES_IN_CIRCLE;

        Vector3 handPosition = this.Hand.position;
        this.Hand.SetPositionAndRotation(handPosition, Quaternion.AngleAxis(angle, Vector3.forward));

        this.ProgressOverlay.fillAmount = 1 - timePercentage;
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
