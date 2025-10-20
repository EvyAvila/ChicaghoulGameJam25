using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAimer : MonoBehaviour
{
    /// <summary>
    /// Camera anchor attatched to player
    /// </summary>
    [SerializeField] private Transform cameraAnchor;

    /// <summary>
    /// Player body to rotate
    /// </summary>
    [SerializeField] private GameObject playerBody;
    [SerializeField] private float bodyRotateSpeed;
    [SerializeField] private bool rotateBody;


    /// <summary>
    /// Aiming rotation vector 
    /// </summary>
    private Vector2 anchorRotationVector;
    [Range(0f, 100f)]
    [SerializeField] private float aimSensitvityX = 3.0f;
    [SerializeField] private float aimSensitvityY = 3.0f;
    [SerializeField] private float topLookClampAngle = 90;
    [SerializeField] private float bottomLookClampAngle = -90;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void SetMouseAim(Vector2 aimDelta)
    {
        //anchorRotationVector.x -= aimDelta.y * aimSensitvityX * Time.deltaTime;
        //anchorRotationVector.y += aimDelta.x * aimSensitvityY * Time.deltaTime;

        anchorRotationVector.x -= aimDelta.y * MouseSensitivity.Instance.sensitivity * Time.deltaTime;
        anchorRotationVector.y += aimDelta.x * MouseSensitivity.Instance.sensitivity * Time.deltaTime;

        anchorRotationVector.x = Mathf.Clamp(anchorRotationVector.x,
            bottomLookClampAngle,
            topLookClampAngle);
    }

    private void UpdateAnchorRotation()
    {
        if (cameraAnchor == null)
            return;

        cameraAnchor.rotation = Quaternion.Euler(anchorRotationVector);
    }

    private void RotatePlayerBody()
    {
        if (playerBody == null) 
            return;

        if (!rotateBody)
            return;

        playerBody.transform.rotation =
            Quaternion.Slerp(playerBody.transform.rotation,
                Quaternion.Euler(0, cameraAnchor.eulerAngles.y, 0),
                bodyRotateSpeed * Time.deltaTime);
    }
    private void Update()
    {
        UpdateAnchorRotation();
        RotatePlayerBody();
    }
}
