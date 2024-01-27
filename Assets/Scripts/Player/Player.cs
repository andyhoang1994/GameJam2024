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

    [Header("Player attributes")]
    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float playerHeight = 1.4f;

    [SerializeField]
    private float playerWidth = 0.7f;

    [SerializeField]
    private float playerDepth = 0.3f;

    [SerializeField]
    private float collisionDistance = 0.24f;

    [SerializeField]
    private float interactDistance = 5f;

    public bool IsWalking { get; private set; }

    public Vector3 LastInteractDirection { get; private set; }

    public Transform NearbyObject { get; private set; }

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
        Debug.Log(this.NearbyObject);
        if (this.NearbyObject != null)
        {
            this.NearbyObject.gameObject.TryGetComponent(out BananaCrate bananaCrate);
            Debug.Log(bananaCrate);
            if (bananaCrate != null)
            {
                bananaCrate.Interact();
            }
        }
    }

    private bool GetCanMove(Vector3 moveDirection)
    {
        Vector3 halfExtends = new Vector3(this.playerWidth, this.playerHeight, this.playerDepth) / 2;
        return !Physics.BoxCast(transform.position, halfExtends, moveDirection, Quaternion.identity, this.collisionDistance);
    }

    private void SetNearbyObject(Transform gameObject)
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

        Vector3 halfExtends = new Vector3(this.playerWidth, this.playerHeight, this.playerDepth) / 2;
        if (Physics.BoxCast(transform.position, halfExtends, this.LastInteractDirection, out RaycastHit raycastHit, Quaternion.identity, this.interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out Transform gameObject))
            {
                if (gameObject != this.NearbyObject)
                {
                    Debug.Log("Setting nearby game object");
                    this.SetNearbyObject(gameObject);
                }
            }
            else
            {
                this.SetNearbyObject(null);
            }
        }
        else
        {
            this.SetNearbyObject(null);
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

        if (moveDirection.x < 0f)
        {
            this.sprite.flipX = true;
        }
        else if (moveDirection.x > 0f)
        {
            this.sprite.flipX = false;
        }
    }

    private void Update()
    {
        this.HandleMovement();
        this.HandleInteractions();
    }
}
