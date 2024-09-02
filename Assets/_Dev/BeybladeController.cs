using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BeybladeController : MonoBehaviour
{
    public float launchForce = 1f;
    public float inwardForce = 10f;
    public float raycastDistance = 15f;
    public Transform ArenaCenter;
    public bool isStartRight = true;

    private Rigidbody rigidBody;

    public float maxTiltAngle = 35f;
    public LayerMask arenaLayer;

    public Transform legTransform;
    public Transform oponentBeybladeTransform;
    
    private float legOffset;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        legOffset = transform.position.y - legTransform.position.y;

        PlaceOnBowlSurface();
        ApplyLaunchForce();
    }

    void PlaceOnBowlSurface()
    {
        RaycastHit hit;
        Vector3 rayStart = new Vector3(legTransform.position.x, ArenaCenter.position.y + raycastDistance, legTransform.position.z);
        if (Physics.Raycast(rayStart, Vector3.down, out hit, raycastDistance * 2, arenaLayer))
        {
            PlaceOnBowlSurface(hit);
        }
    }

    public float positionSmoothSpeed = 150f; // Adjust this to control the speed of position change

    void PlaceOnBowlSurface(RaycastHit hit)
    {
        // Calculate the target position based on the hit point and leg offset
        Vector3 targetPosition = hit.point + transform.up * legOffset;

        // Smoothly interpolate between the current position and the target position
        transform.position = targetPosition;//Vector3.Lerp(transform.position, targetPosition, positionSmoothSpeed * Time.deltaTime);
    }

    void AlignWithBowlSurface(RaycastHit arenaHit)
    {
        // Align the up vector with the surface normal
        Vector3 targetUp = arenaHit.normal;

        // Limit the tilt angle
        targetUp = Vector3.RotateTowards(Vector3.up, targetUp, maxTiltAngle * Mathf.Deg2Rad, 0f);

        // Smoothly rotate to the target orientation
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, targetUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 15f);
    }

    void ApplyLaunchForce()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance, arenaLayer))
        {
            Vector3 normal = hit.normal;

            // Calculate the vector from the contact point to the center of the bowl
            Vector3 vectorToCenter = ArenaCenter.position - hit.point;

            // Project the vectorToCenter onto the tangent plane defined by the normal
            Vector3 tangentDirection = vectorToCenter - Vector3.Dot(vectorToCenter, normal) * normal;

            // Normalize the tangent direction
            Vector3 normalizedTangentDirection = tangentDirection.normalized;

            Vector3 leftDirection = Vector3.Cross(normal, normalizedTangentDirection).normalized * launchForce;
            Vector3 rightDirection = -1f * leftDirection;

            rigidBody.AddForce(isStartRight ? rightDirection : leftDirection, ForceMode.Impulse);

            transform.up = normal;
        }
    }

    void GoTowardsOponent(RaycastHit hit)
    {
        var directionToOponent = (oponentBeybladeTransform.position - transform.position).normalized;

        Vector3 projectedDirection = Vector3.ProjectOnPlane(directionToOponent, hit.normal).normalized;

        rigidBody.AddForce(5f * projectedDirection, ForceMode.Force);
    }

    void FixedUpdate()
    {
        //Raycast downward to detect the surface
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance, arenaLayer))
        {
            // Get the point and normal from the raycast
            Vector3 contactPoint = hit.point;
            Vector3 normal = hit.normal;

            // Calculate the vector from the contact point to the center of the bowl
            Vector3 vectorToCenter = ArenaCenter.position - contactPoint;

            // Project the vectorToCenter onto the tangent plane defined by the normal
            Vector3 tangentDirection = vectorToCenter - Vector3.Dot(vectorToCenter, normal) * normal;

            // Normalize the tangent direction
            Vector3 normalizedTangentDirection = tangentDirection.normalized;

            rigidBody.AddForce(normalizedTangentDirection * inwardForce, ForceMode.Force);

            AlignWithBowlSurface(hit);
            PlaceOnBowlSurface(hit);
            //GoTowardsOponent(hit);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Beyblade")) return;

        float impactForce = collision.impulse.magnitude;

        var directionToOther = collision.collider.transform.position - transform.position;

        rigidBody.AddForce(impactForce * -5f * directionToOther, ForceMode.Impulse);

        Debug.Log("Collision " + name);
    }
}
