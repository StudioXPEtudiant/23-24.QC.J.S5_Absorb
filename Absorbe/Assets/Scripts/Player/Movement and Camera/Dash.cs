using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVelocity = true;

    [Header("CameraEffects")]
    public CameraMovement cam;
    public float dashFov;


    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    public bool dashing;

    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private PlayerMovementAdvanced pm;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovementAdvanced>();
    }

    void Update()
    {
        if(Input.GetKeyDown(dashKey))
            DashMovement();

        if(dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    public void DashMovement()
    {
        if(dashCdTimer > 0) return;
        else dashCdTimer = dashCd;
        
        pm.dashing = true;
        pm.maxYSpeed = maxDashYSpeed;

        cam.DoFov(dashFov);

        Transform forwardT;

        if(useCameraForward)
            forwardT = playerCam;
        else
            forwardT = orientation;
        
        Vector3 direction = GetDirection(forwardT);

        Vector3 forceToApply = direction * dashForce + orientation.up;

        if(disableGravity)
            rb.useGravity = false;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);

        if(Input.GetKeyDown("space") && dashing && resetVelocity == true)
        {
            resetVelocity = false;
            rb.AddForce(direction * dashForce + orientation.forward * dashUpwardForce);
        }
    }

    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {
        if(resetVelocity)
            rb.velocity = Vector3.zero;
        
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);

        if(Input.GetKeyDown("space") && dashing)
        {
            rb.AddForce(delayedForceToApply * dashUpwardForce, ForceMode.Force);
        }
    }

    private void ResetDash()
    {
        pm.dashing = false;
        pm.maxYSpeed = 0;

        cam.DoFov(85f);

        if(disableGravity)
            rb.useGravity = true;
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if(allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;

        if(verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;

    }
}
