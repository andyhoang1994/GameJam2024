using UnityEngine;

public class Employee : MonoBehaviour
{

    [Header("Assets")]
    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private Transform[] moodSprites;

    [Header("Properties")]
    [SerializeField]
    private float height = 1.4f;

    [SerializeField]
    private float width = 0.7f;

    [SerializeField]
    private float depth = 1f;

    [Header("Attributes")]
    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float maxMoveSpeed = 6f;

    [SerializeField]
    private int strength = 1;

    [SerializeField]
    private float mood = 50f;

    [SerializeField]
    private float maxMood = 100f;

    [Header("Decrease rate")]
    private float moodDecreaseRate = 2f;

    [SerializeField]
    private HoldableObject like;

    [SerializeField]
    private HoldableObject dislike;

    [SerializeField]
    private float collisionDistance = 0.25f;

    [Header("Pathing")]
    [SerializeField]
    private Transform[] wayPoints;

    private bool IsReturning { get; set; } = false;

    private int WayPointIndex { get; set; } = 0;

    public Vector3 MoveDirection { get; private set; }

    public bool IsWalking { get; private set; }

    public float Mood { get { return this.mood; } set { this.mood = value; } }

    public int Strength { get { return this.strength; } set { this.strength = value; } }

    public HoldableObject Like { get { return this.like; } }

    public HoldableObject Dislike { get { return this.dislike; } }

    private void InstanceOnEnterWaypointRange(object sender, OnEnterWaypointRangeArgs e)
    {
        if (e.waypoint && e.waypoint.transform.position.Equals(this.GetWayPoint().transform.position))
        {
            int money = this.Strength;
            GameState.Instance.AddMoney(money);
        }
    }

    private bool GetCanMove(Vector3 moveDirection)
    {
        Vector3 halfExtends = new Vector3(this.width, this.height, this.depth) / 2;
        return !Physics.BoxCast(transform.position, halfExtends, moveDirection, Quaternion.identity, this.collisionDistance);
    }

    private Transform[] WayPoints { get { return this.wayPoints; } }

    private Transform GetWayPoint()
    {
        return this.WayPoints[this.WayPointIndex];
    }

    private void MoveToNextWaypoint()
    {
        if (this.WayPointIndex == this.WayPoints.Length - 1)
        {
            this.IsReturning = true;
        }
        else if (this.WayPointIndex == 0)
        {
            this.IsReturning = false;
        }


        int newWayPointIndex = this.WayPointIndex + (this.IsReturning ? -1 : 1);
        this.WayPointIndex = newWayPointIndex;
    }

    private Transform GetMoodSprite(Mood mood)
    {
        return this.moodSprites[(int)mood];
    }

    private void UpdateMoodSprite()
    {
        switch (this.Mood)
        {
            case >= 70:
                this.GetMoodSprite(global::Mood.Okay).gameObject.SetActive(false);
                this.GetMoodSprite(global::Mood.Bad).gameObject.SetActive(false);
                this.GetMoodSprite(global::Mood.Good).gameObject.SetActive(true);
                break;
            case >= 40 and < 70:
                this.GetMoodSprite(global::Mood.Good).gameObject.SetActive(false);
                this.GetMoodSprite(global::Mood.Bad).gameObject.SetActive(false);
                this.GetMoodSprite(global::Mood.Okay).gameObject.SetActive(true);
                break;
            case >= 0 and < 40:
                this.GetMoodSprite(global::Mood.Good).gameObject.SetActive(false);
                this.GetMoodSprite(global::Mood.Okay).gameObject.SetActive(false);
                this.GetMoodSprite(global::Mood.Bad).gameObject.SetActive(true);
                break;
        }
    }
    private void UpdateMood()
    {
        switch (this.Mood)
        {
            case > 100:
                this.Mood = 100;
                break;
            case < 0:
                this.Mood = 0;
                break;
            case not 100 and not 0:
                this.Mood -= Time.deltaTime * this.moodDecreaseRate;
                break;
        }
    }

    private void UpdateStrength()
    {
        switch (this.Mood)
        {
            case >= 70:
                this.Strength = 3;
                break;
            case >= 40 and < 70:
                this.Strength = 2;
                break;
            case >= 0 and < 40:
                this.Strength = 1;
                break;
        }
    }

    private void HandleEnterWaypoint()
    {
        Vector3 waypointPosition = this.GetWayPoint().position;
        float distance = Vector3.Distance(transform.position, waypointPosition);

        if (distance < 1.5f)
        {
            this.MoveToNextWaypoint();
        }
    }

    private void HandleMovement()
    {
        Transform waypoint = this.GetWayPoint();
        Vector3 moveDirection = waypoint.position - transform.position;

        this.MoveDirection = new Vector3(moveDirection.x, 0, moveDirection.z).normalized;

        float moveDistance = this.moveSpeed * Time.deltaTime;
        bool canMove = this.GetCanMove(this.MoveDirection);

        if (canMove)
        {
            transform.position += this.MoveDirection * moveDistance;
        }

        this.IsWalking = canMove;

        if (this.MoveDirection.x < 0f)
        {
            this.sprite.flipX = false;
        }
        else if (this.MoveDirection.x > 0f)
        {
            this.sprite.flipX = true;
        }
    }

    private void Start()
    {
        DestinationWaypoint.Instance.OnEnterWaypointRange += InstanceOnEnterWaypointRange;
        this.WayPointIndex = 0;
    }

    private void FixedUpdate()
    {
        this.HandleMovement();
        this.HandleEnterWaypoint();
        this.UpdateMood();
        this.UpdateMoodSprite();
        this.UpdateStrength();
    }

    public void ActionOne(Player player)
    {
        if (player.HasHeldObject())
        {
            Transform heldObject = player.HeldObject;
            heldObject.TryGetComponent(out HoldableObject holdableObject);
            string objectName = holdableObject.HoldableObjectSO.ObjectName;

            if (this.Like.HoldableObjectSO.ObjectName == objectName)
            {
                this.Mood += 40f;
            }
            else if (this.Dislike.HoldableObjectSO.ObjectName == objectName)
            {
                this.Mood -= 10f;
            }
            else
            {
                this.Mood += 20f;
            }

            player.ClearHeldObject();
        }
    }
}
