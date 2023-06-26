using System;
using UnityEngine;

public class Player : MonoBehaviour {
    public event EventHandler<OnPurchasableTileZoneSteppedEventArgs> OnPurchasableTileZoneStepped;
    public event EventHandler OnPurchasibleTileZoneUnstepped;
    public class OnPurchasableTileZoneSteppedEventArgs : EventArgs {
        public HexTile hexTile;
    }

    public static Player Instance { get; private set; }

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float fallSpeed = 1000f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float groundDrag = 5;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask purchaseTileZone;
    private float groundRayDistance = 0.5f;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private bool isWalking;
    private Vector2 inputVector;
    private Vector3 moveDir;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        if (Instance == null) {
            Instance = this;
        }
        else {
            Debug.Log("There is more than one player instance");
        }
    }

    private void Update() {

        HandleInput();

        if (IsGrounded()) {
            rb.drag = groundDrag;
        }
        else {
            rb.drag = 0;
        }

        HandleHexTileInteractions();
    }
    private void FixedUpdate() {

        HandleMovement();

    }

    public bool IsWalking() {
        return isWalking;
    }

    private bool IsGrounded() {
        Physics.Raycast(capsuleCollider.bounds.center, Vector3.down, out RaycastHit rayCastHit, capsuleCollider.bounds.extents.y + groundRayDistance, whatIsGround);
        return rayCastHit.collider != null;
    }
    private void HandleMovement() {
        isWalking = moveDir != Vector3.zero;
        if (isWalking) {
            float moveDistance = movementSpeed * Time.fixedDeltaTime;

            //Movement
            float rotateDistance = rotationSpeed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + (moveDir * moveDistance));

            //Rotation
            Quaternion tr = Quaternion.LookRotation(moveDir);
            Quaternion targeRotation = Quaternion.Slerp(rb.rotation, tr, rotateDistance);
            rb.MoveRotation(targeRotation);
        }

        if (!IsGrounded()) {
            rb.AddForce(Vector3.down * (fallSpeed * Time.fixedDeltaTime), ForceMode.Force);
        }

    }

    private void HandleInput() {
        inputVector = gameInput.GetMovementVectorNormalized();
        moveDir = new Vector3(inputVector.x, 0, inputVector.y);
    }

    private void HandleHexTileInteractions() {
        if (Physics.Raycast(capsuleCollider.bounds.center, Vector3.down, out RaycastHit rayCastHit, capsuleCollider.bounds.extents.y + groundRayDistance, purchaseTileZone)) {
            if (rayCastHit.transform.TryGetComponent(out HexTile hextile)) {
                //Has HexTile
                OnPurchasableTileZoneStepped?.Invoke(this, new OnPurchasableTileZoneSteppedEventArgs {
                    hexTile = hextile
                });
            }

        } else {
            OnPurchasibleTileZoneUnstepped?.Invoke(this, EventArgs.Empty);
        }
    }
}
