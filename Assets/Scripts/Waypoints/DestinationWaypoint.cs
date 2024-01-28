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

    private void HandleNearbyObjects()
    {
        Debug.Log(this.Waypoint);
        if (Physics.BoxCast(transform.position, new Vector3(3f, 3f, 3f), Vector3.one, out RaycastHit raycastHit, Quaternion.identity, 3f))
        {
            if (raycastHit.transform.TryGetComponent(out Employee employee))
            {
                if (this.Employee != employee)
                {
                    this.OnEnterWaypointRange?.Invoke(this, new OnEnterWaypointRangeArgs { waypoint = this.Waypoint });
                    this.Employee = employee.gameObject;
                }
            }
            else
            {
                this.Employee = null;
            }
        }
    }

    private void Update()
    {
        HandleNearbyObjects();
    }
}
