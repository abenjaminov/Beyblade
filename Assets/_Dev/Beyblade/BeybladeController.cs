using UnityEngine;

public class BeybladeController : MonoBehaviour
{
    BeybladeLiveConfiguration config;

    [Header("Physics Values")]
    [Tooltip("Determines the linear velocity force magnitude of the beyblade")]
    public float launchForce = 1f;
    [Tooltip("Determines how much the beyblade is drawn to the center")]
    public float inwardForce = 10f;
    [Tooltip("Sets the direction that the beyblade will start")]
    public bool isStartRight = true;
    [Tooltip("The max angle that the beyblade can tilt from its UP direction")]
    public float maxTiltAngle = 35f;

    [Header("Environment")]
    [Tooltip("Indicates the center of the arena")]
    public Transform ArenaCenter;
    [Tooltip("Layer the the arena has, so the beyblade can detect if its above it")]
    public LayerMask arenaLayer;
    [Tooltip("This is the transform for the leg of the beyblade, the contact point with the arena")]
    public Transform legTransform;

    [Header("Effects")]
    [Tooltip("Effect that shows on collision")]
    public GameObject SparksEffect;

    private Rigidbody rigidBody;
    private float legOffset;
    private float raycastDistance = 15f;

    void Awake()
    {
        config = GetComponent<BeybladeLiveConfiguration>();
        config.CurrentHealth.Value = config.MaxHealth.Value;

        rigidBody = GetComponent<Rigidbody>();

        legOffset = transform.position.y - legTransform.position.y;
    }

    private void Start()
    {
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

    void PlaceOnBowlSurface(RaycastHit hit)
    {
        // Calculate the target position based on the hit point and leg offset
        Vector3 targetPosition = hit.point + transform.up * legOffset;

        // Smoothly interpolate between the current position and the target position
        transform.position = targetPosition;
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
        }
    }

    public void ChangeHealth(float diff)
    {
        config.CurrentHealth.Value = Mathf.Clamp(config.CurrentHealth + diff, 0, config.MaxHealth.Value);

        config.RoundsPerMinute.Value = 100 + 5 * config.CurrentHealth.Value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (!collision.gameObject.TryGetComponent<BeybladeController>(out var beybladeController)) return;

        //float impactForce = collision.impulse.magnitude;
        float impactForce = 2 + 3 * Random.value;
        Debug.Log(impactForce);
        var directionToOther = collision.collider.transform.position - transform.position;

        //if (impactForce == 0) impactForce = .5f;

        rigidBody.AddForce(impactForce * -3f * directionToOther, ForceMode.Impulse);

        Instantiate(SparksEffect, collision.contacts[0].point, Quaternion.identity);

        var healthVariable = beybladeController.config.Attack.Value / (beybladeController.config.Attack.Value + config.Defense.Value);

        var healthChange = 50 + healthVariable;

        ChangeHealth(-healthChange);

        rigidBody.angularVelocity = Vector3.zero;
    }
}
