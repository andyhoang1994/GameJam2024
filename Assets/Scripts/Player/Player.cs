using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("Assets")]
    [SerializeField]
    private GameInput gameInput;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private Transform heldObjectPoint;

    [Header("Player attributes")]
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float height = 1.4f;

    [SerializeField]
    private float width = 0.7f;

    [SerializeField]
    private float depth = 0.3f;

    [SerializeField]
    private float collisionDistance = 0.25f;

    [SerializeField]
    private float interactDistance = 4f;

    private Transform heldObject;

    public bool IsWalking { get; private set; }

    public Vector3 LastInteractDirection { get; private set; }

    public BaseObject NearbyObject { get; private set; }

    public Employee NearbyEmployee { get; private set; }

    public Transform HeldObject
    {
        get { return this.heldObject; }
        set
        {
            this.heldObject = value;
            GameStateUI.Instance.UpdateHeldItem(value);
        }
    }

    public event EventHandler<OnEnterInteractRangeArgs> OnEnterInteractRange;

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
        this.gameInput.OnActionOne += GameInputOnActionOne;
        this.gameInput.OnActionTwo += GameInputOnActionTwo;
    }

    private void GameInputOnActionTwo(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void GameInputOnActionOne(object sender, EventArgs e)
    {
        if (this.NearbyEmployee != null)
        {
            this.NearbyEmployee.ActionOne(this);
        }
        else if (this.NearbyObject != null)
        {
            this.NearbyObject.ActionOne(this);
        }
    }

    private bool GetCanMove(Vector3 moveDirection)
    {
        Vector3 halfExtends = new Vector3(this.width, this.height / 2, this.depth);
        return !Physics.BoxCast(transform.position, halfExtends, moveDirection, Quaternion.identity, this.collisionDistance);
    }

    private void SetNearbyObject(BaseObject gameObject)
    {
        this.NearbyObject = gameObject;
        this.OnEnterInteractRange?.Invoke(this, new OnEnterInteractRangeArgs { nearbyObject = gameObject });
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVector().normalized;
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            this.LastInteractDirection = moveDirection;
        }

        Vector3 halfExtends = new Vector3(this.width, this.height, this.depth) / 2;
        if (Physics.BoxCast(transform.position, halfExtends, this.LastInteractDirection, out RaycastHit raycastHit, Quaternion.identity, this.interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out Employee employee))
            {
                if (employee != this.NearbyEmployee)
                {
                    this.NearbyEmployee = employee;
                }
            }
            else if (raycastHit.transform.TryGetComponent(out BaseObject gameObject))
            {
                this.NearbyEmployee = null;

                if (gameObject != this.NearbyObject)
                {
                    this.SetNearbyObject(gameObject);
                }
            }
            else
            {
                this.SetNearbyObject(null);
                this.NearbyEmployee = null;
            }
        }
        else
        {
            this.SetNearbyObject(null);
            this.NearbyEmployee = null;
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = this.GetCanMove(moveDirection);

        this.IsWalking = moveDirection != Vector3.zero;

        if (canMove is not true)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = this.GetCanMove(moveDirectionX);

            if (canMove is true)
            {
                moveDirection = moveDirectionX;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = this.GetCanMove(moveDirectionZ);

                if (canMove is true)
                {
                    moveDirection = moveDirectionZ;
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        Vector3 heldObjectPosition = this.HeldObjectPoint.localPosition;
        if (moveDirection.x < 0f)
        {
            this.sprite.flipX = false;
            this.HeldObjectPoint.SetLocalPositionAndRotation(new Vector3(-Math.Abs(heldObjectPosition.x), heldObjectPosition.y, heldObjectPosition.z), Quaternion.AngleAxis(180, Vector3.up));
        }
        else if (moveDirection.x > 0f)
        {
            this.sprite.flipX = true;
            this.HeldObjectPoint.SetLocalPositionAndRotation(new Vector3(Math.Abs(heldObjectPosition.x), heldObjectPosition.y, heldObjectPosition.z), Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        this.HandleMovement();
        this.HandleInteractions();
    }

    public Transform HeldObjectPoint { get { return this.heldObjectPoint; } }

    public void ClearHeldObject()
    {
        Destroy(this.HeldObject.gameObject);
        this.HeldObject = null;
    }

    public bool HasHeldObject()
    {
        return this.HeldObject != null;
    }
}
