using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;
    
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
        
        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {

    }
}
