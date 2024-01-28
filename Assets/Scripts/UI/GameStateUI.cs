using UnityEngine;


public class GameStateUI : MonoBehaviour
{
    const int DEGREES_IN_CIRCLE = 360;

    [SerializeField]
    private Transform hand;

    private Transform Hand { get { return hand; } }

    private void Update()
    {
        float totalTime = Timer.Instance.TotalTime;
        float timeRemaining = Timer.Instance.TimeRemaining;

        float angle = DEGREES_IN_CIRCLE * (timeRemaining / totalTime) - DEGREES_IN_CIRCLE;

        Debug.Log(angle);

        Vector3 handPosition = this.Hand.position;
        this.Hand.SetPositionAndRotation(handPosition, Quaternion.AngleAxis(angle, Vector3.forward));
    }
}
