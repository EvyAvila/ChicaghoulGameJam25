using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyingPlayer : MonoBehaviour
{
    /// <summary>
    /// Inputs
    /// </summary>
    private PlayerInputs flyingInputs;

    [Header("Play Bounds")]
    /// <summary>
    /// Bounds limiting player movement
    /// </summary>
    [SerializeField] private FlyingBounds flightBounds;
    private float X_AimBoundsOffset;
    private float Y_AimBoundsOffset;
    private float X_CurrentBounds;
    private float Y_CurrentBounds;
    private Vector3 aimingBounds;

    [Header("Position Hint")]
    /// <summary>
    /// Object that the player will Lerp to
    /// </summary>
    [SerializeField] private Transform trackingObject;
    private Vector3 defaultTargetPosition;
    private Vector3 targetAxisPosition;
    private Vector3 dirToTarget;


    [Header("Player Body")]
    /// <summary>
    /// The player object for collision
    /// </summary>
    [SerializeField] private Transform playerObject;
    [SerializeField] private float MatchToTargetPosSpeed;

    [Header("Player Rotations")]
    [SerializeField] private float horizontalSmoothTime = 5;
    [SerializeField] private float verticalSmoothTime = 5;
    [SerializeField] private float MatchToTargetRotSpeed = 5;
    [SerializeField] private float MaxHorizontalLeanAngle = 60.0f;
    [SerializeField] private float MaxVerticalLeanAngle = 30.0f;
    private float currentHorizontalTilt;
    private float currentVerticalTilt;
    private Vector3 currentTiltEuler;
    private Vector3 finalEuler;

    [Header("Sensitivity")]
    [SerializeField] private float TargetMoveSpeed = 5;
    [SerializeField] private float ModifiedTargetMoveSpeed = 5;
    [SerializeField] private float HorizontalInputMultiplier = 1;
    [SerializeField] private float VerticalInputMultiplier = 1;

    /// <summary>
    /// Raw player direction input
    /// </summary>
    private Vector2 inputDir;

    /// <summary>
    /// A deadzone filter for inputDir
    /// </summary>
    [SerializeField] private float verticalDeadzone = 0.05f;
    [SerializeField] private float horizontalDeadzone = 0.05f;

    private void Start()
    {
        flyingInputs = new PlayerInputs();

        defaultTargetPosition = trackingObject.localPosition;

        //Define the bounds for aim hint
        X_AimBoundsOffset = flightBounds.GetWidth();
        Y_AimBoundsOffset = flightBounds.GetHeight();

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        if (flyingInputs == null)
            flyingInputs = new PlayerInputs();

        flyingInputs.Enable();
        flyingInputs.FlyingMap.MouseMovement.performed += OnMouseMoved;
        flyingInputs.FlyingMap.MouseMovement.canceled += OnMouseStopped;
        
    }


    private void OnDisable()
    {
        flyingInputs.Disable();
        flyingInputs.FlyingMap.MouseMovement.performed -= OnMouseMoved;
        flyingInputs.FlyingMap.MouseMovement.canceled -= OnMouseStopped;
    }

    private void OnMouseMoved(InputAction.CallbackContext obj)
    {
        inputDir = obj.ReadValue<Vector2>();

        if (Mathf.Abs(inputDir.x) < horizontalDeadzone)
            inputDir.x = 0;

        if (Mathf.Abs(inputDir.y) < verticalDeadzone)
            inputDir.y = 0;

    }
    private void OnMouseStopped(InputAction.CallbackContext obj)
    {
        inputDir = Vector2.zero;
    }
    private void Update()
    {
        MovementOnAxis();
    }

    private void MovementOnAxis()
    {
        //Update the tracking target
        UpdateTrackingTarget();

        //Match playerObj -> trackingObj with delay
        MatchPlayerToTracker();

        //Leaning and rotations for camera feel
        AimPlayerTowardsTarget();
        //HorizontalInputLean();
        //VerticalInputLean();
    }

    /// <summary>
    /// Moves the tracking target. Limited by FlightBounds
    /// </summary>
    private void UpdateTrackingTarget()
    {
        if (inputDir != Vector2.zero)
        {
            inputDir.Normalize();
            aimingBounds = inputDir;

            X_CurrentBounds += aimingBounds.x * HorizontalInputMultiplier * Time.deltaTime;
            Y_CurrentBounds += aimingBounds.y * VerticalInputMultiplier * Time.deltaTime;

            X_CurrentBounds = Mathf.Clamp(X_CurrentBounds, -X_AimBoundsOffset, X_AimBoundsOffset);
            Y_CurrentBounds = Mathf.Clamp(Y_CurrentBounds, -Y_AimBoundsOffset, Y_AimBoundsOffset);

            aimingBounds.x = X_CurrentBounds;
            aimingBounds.y = Y_CurrentBounds;
            aimingBounds.z = defaultTargetPosition.z;
        }
        else
        {
            X_CurrentBounds = 0.0f;
            Y_CurrentBounds = 0.0f;
        }
        
        trackingObject.localPosition = Vector3.Lerp(trackingObject.localPosition, aimingBounds, TargetMoveSpeed * Time.deltaTime);
        aimingBounds = trackingObject.localPosition;
    }

    private void MatchPlayerToTracker()
    {
        targetAxisPosition = trackingObject.localPosition;
        targetAxisPosition.z = playerObject.localPosition.z;

        playerObject.localPosition = Vector3.Lerp(playerObject.localPosition, targetAxisPosition, MatchToTargetPosSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Sets PlayerObjects rotation to face direction of tracking target
    /// </summary>
    private void AimPlayerTowardsTarget()
    {
        currentTiltEuler = playerObject.localEulerAngles;
        dirToTarget = trackingObject.localPosition - playerObject.localPosition;

        float newY = Quaternion.LookRotation(dirToTarget).eulerAngles.y;
        currentTiltEuler.y = Mathf.LerpAngle(currentTiltEuler.y, newY, MatchToTargetRotSpeed * Time.deltaTime);
        playerObject.localEulerAngles = currentTiltEuler;
    }

    private void HorizontalInputLean()
    {
        currentTiltEuler = playerObject.localEulerAngles;

        if (inputDir.x != 0)
        {
            currentHorizontalTilt = -inputDir.x * MaxHorizontalLeanAngle;
        }
        else
        {
            if (currentHorizontalTilt != 0)
            {
                //currentTilt = Mathf.Lerp(currentTilt, 0, horizontalSmoothTime * Time.deltaTime);
                currentHorizontalTilt = 0;
            }
        }

        currentTiltEuler.z = Mathf.LerpAngle(currentTiltEuler.z, currentHorizontalTilt, horizontalSmoothTime * Time.deltaTime);
        playerObject.localEulerAngles = currentTiltEuler;
    }
    private void VerticalInputLean()
    {
        currentTiltEuler = playerObject.localEulerAngles;

        if (inputDir.y != 0)
        {
            currentVerticalTilt = -inputDir.y * MaxVerticalLeanAngle;
        }
        else
        {
            if (currentVerticalTilt != 0)
            {
                //currentTilt = Mathf.Lerp(currentTilt, 0, verticalSmoothTime * Time.deltaTime);
                currentVerticalTilt = 0;
            }
        }

        currentTiltEuler.x = Mathf.LerpAngle(currentTiltEuler.x, currentVerticalTilt, verticalSmoothTime * Time.deltaTime);
        playerObject.localEulerAngles = currentTiltEuler;
    }
}
