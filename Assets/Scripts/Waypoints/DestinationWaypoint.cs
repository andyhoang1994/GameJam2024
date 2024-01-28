using System;
using UnityEngine;

public class DestinationWaypoint : MonoBehaviour
{
    public static DestinationWaypoint Instance { get; private set; }

    [SerializeField]
    private GameObject waypoint;

    public GameObject Waypoint { get { return this.waypoint; } }

    public GameObject Employee { get; set; }

    public event EventHandler<OnEnterWaypointRangeArgs> OnEnterWaypointRange;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances");
            Debug.Break();
        }
        Instance = this;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Employee employee))
        {
            Debug.Log("On trigger enter");
            this.OnEnterWaypointRange?.Invoke(this, new OnEnterWaypointRangeArgs { waypoint = this.Waypoint });
        }
    }
}
