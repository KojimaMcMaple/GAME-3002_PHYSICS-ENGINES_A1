using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private Vector3 impulse_dir = Vector3.zero;
    
    
    [SerializeField]
    private bool has_attempted_impulse = false;
   
    
    [SerializeField]
    private float jump_input = 0.0f;
    [SerializeField]
    private bool is_grounded = false;

    private Rigidbody rb = null;
    private Collider projectile_collider = null;

    public float impulse_multiplier = 0.0f;
    public Vector3 ground_parallel_dir = Vector3.zero;
    public float launch_angle = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        projectile_collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 collider_pos = projectile_collider.transform.position;
        Vector3 raycast_pos = new Vector3(collider_pos.x, projectile_collider.bounds.min.y, collider_pos.z);
        Debug.DrawRay(raycast_pos, Vector3.down * 0.2f, Color.green);
    }

    private void FixedUpdate()
    {
        impulse_dir = transform.forward;
        jump_input = Input.GetAxis("Jump");

        ground_parallel_dir = Vector3.Cross(transform.right, Vector3.up);
        launch_angle = Mathf.Abs(Vector3.SignedAngle(ground_parallel_dir, impulse_dir, transform.up));
        if (jump_input != 0)
        {
            has_attempted_impulse = true;
        }

        if (has_attempted_impulse)
        {
            has_attempted_impulse = false;
            is_grounded = false;
            AttemptImpulse();
        }

        if (IsGrounded())
        {
            is_grounded = true;
            rb.velocity = Vector3.zero;
        }
    }

    private void AttemptImpulse()
    {
        //rb.AddForce(impulse_dir.normalized * 2, ForceMode.Impulse);
        rb.velocity = impulse_dir.normalized * impulse_multiplier;
    }

    private bool IsGrounded()
    {
        Vector3 collider_pos = projectile_collider.transform.position;
        Vector3 raycast_pos = new Vector3(collider_pos.x, projectile_collider.bounds.min.y, collider_pos.z);
        return Physics.Raycast(raycast_pos, Vector3.down, 0.2f);
    }
}
