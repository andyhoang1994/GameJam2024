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

    private int WayPointIndex { get; set; } = 0;

    public Vector3 MoveDirection { get; private set; }

    public bool IsWalking { get; private set; }

    public float Mood { get { return this.mood; } set { this.mood = value; } }

    public int Strength { get { return this.strength; } set { this.strength = value; } }

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
        int newWayPointIndex = this.WayPointIndex + 1;

        if (newWayPointIndex >= this.WayPoints.Length)
        {
            this.WayPointIndex = 0;
        }
        else
        {
            this.WayPointIndex = newWayPointIndex;
        }
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
        if (this.Mood > 100)
        {
            this.Mood = 100;
        }
        this.Mood -= Time.deltaTime * this.moodDecreaseRate;
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
        float distance = Vector3.Distance(transform.position, this.GetWayPoint().position);

        if (distance < 1.7f)
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

        this.IsWalking = this.MoveDirection != Vector3.zero;

        if (canMove is not true)
        {
            Vector3 moveDirectionX = new Vector3(this.MoveDirection.x, 0, 0).normalized;
            canMove = this.GetCanMove(moveDirectionX);

            if (canMove is true)
            {
                this.MoveDirection = moveDirectionX;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, this.MoveDirection.z).normalized;
                canMove = this.GetCanMove(moveDirectionZ);

                if (canMove is true)
                {
                    this.MoveDirection = moveDirectionZ;
                }
            }
        }
        if (canMove)
        {
            transform.position += this.MoveDirection * moveDistance;
        }

        if (this.MoveDirection.x < 0f)
        {
            this.sprite.flipX = false;
        }
        else if (this.MoveDirection.x > 0f)
        {
            this.sprite.flipX = true;
        }
    }

    public void Start()
    {
        this.WayPointIndex = 0;
    }

    private void Update()
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
            player.ClearHeldObject();
            this.mood += 10f;
        }
    }
}
