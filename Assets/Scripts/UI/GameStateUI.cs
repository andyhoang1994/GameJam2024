using UnityEngine;
using UnityEngine.UI;

public class GameStateUI : MonoBehaviour
{
    const int DEGREES_IN_CIRCLE = 360;
    public static GameStateUI Instance { get; private set; }

    [SerializeField]
    private Transform hand;

    [SerializeField]
    private Slider progressBar;

    [SerializeField]
    private Image progressOverlay;

    [SerializeField]
    private Transform heldObjectPosition;

    private Transform heldObject;

    private Slider ProgressBar { get { return this.progressBar; } set { this.progressBar = value; } }

    private Transform Hand { get { return this.hand; } set { this.hand = value; } }

    private Image ProgressOverlay { get { return this.progressOverlay; } set { this.progressOverlay = value; } }

    private Transform HeldObject { get { return this.heldObject; } set { this.heldObject = value; } }

    private Transform HeldObjectPosition { get { return this.heldObjectPosition; } set { this.heldObject = value; } }

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

    public void UpdateHeldItem(Transform item)
    {
        if (item == null)
        {
            Destroy(this.HeldObject.gameObject);
            this.HeldObject = null;

            return;
        }

        item.TryGetComponent(out HoldableObject holdableObject);

        Transform holdableObjectTransform = Instantiate(holdableObject.HoldableObjectSO.Sprite, this.HeldObjectPosition);

        this.HeldObject = holdableObjectTransform;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances");
            Debug.Break();
        }
        Instance = this;
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
