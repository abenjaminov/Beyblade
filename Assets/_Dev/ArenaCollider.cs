using UnityEngine;

public class ArenaCollider : MonoBehaviour
{
    public Transform CenterOfArena;

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            Debug.Log("Trigger exit" + other.name);
            // Calculate the direction from the center to the object
            Vector3 directionToCenter = transform.position - other.transform.position;
            directionToCenter.Normalize();

            // Calculate the radial component of the velocity
            Vector3 radialVelocity = Vector3.Dot(rb.velocity, directionToCenter) * directionToCenter;

            // Invert the radial component
            Vector3 newVelocity = rb.velocity - 2 * radialVelocity;

            // Apply the new velocity
            rb.velocity = newVelocity;

            rb.AddForce(directionToCenter, ForceMode.Impulse);
        }
    }

    // Optional: Visualize the boundary in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3.2f);
    }
}