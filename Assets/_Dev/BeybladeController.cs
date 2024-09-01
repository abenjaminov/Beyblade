using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BeybladeController : MonoBehaviour
{
    public float spinSpeed = 500f;
    public float inwardForce = 10f;
    public float raycastDistance = 15f;
    public Transform ArentCenter;

    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Raycast downward to detect the surface
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance))
        {
            // Get the point and normal from the raycast
            Vector3 contactPoint = hit.point;
            Vector3 normal = hit.normal;

            // Calculate the vector from the contact point to the center of the bowl
            Vector3 vectorToCenter = ArentCenter.position - contactPoint;

            // Project the vectorToCenter onto the tangent plane defined by the normal
            Vector3 tangentDirection = vectorToCenter - Vector3.Dot(vectorToCenter, normal) * normal;

            // Normalize the tangent direction
            Vector3 normalizedTangentDirection = tangentDirection.normalized;

            rigidBody.AddForce(normalizedTangentDirection * inwardForce, ForceMode.Force);

            Vector3 leftDirection = Vector3.Cross(normal, normalizedTangentDirection).normalized;

            rigidBody.AddForce(leftDirection * inwardForce, ForceMode.Force);

            transform.up = normal;
            //transform.position = contactPoint + normal * 10f;

            /// Apply torque to spin the Beyblade around its Y-axis
            //rigidBody.AddTorque(Vector3.up * spinSpeed, ForceMode.Force);
        }
    }
}
