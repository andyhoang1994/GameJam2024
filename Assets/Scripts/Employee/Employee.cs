using UnityEngine;

public class Employee : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField]
    private SpriteRenderer sprite;

    [Header("Attributes")]
    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float maxMoveSpeed = 6f;

    [SerializeField]
    private int strength = 1;

    [SerializeField]
    private int maxStrength = 3;

    [SerializeField]
    private float mood = 50f;

    [SerializeField]
    private float maxMood = 100f;

    [SerializeField]
    private float height = 1.4f;

    [SerializeField]
    private float width = 0.7f;

    [SerializeField]
    private float depth = 0.3f;

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
    private float DistanceToWaypoint { get; set; }

    public Vector3 MoveDirection { get; private set; }

    public bool IsWalking { get; private set; }

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
