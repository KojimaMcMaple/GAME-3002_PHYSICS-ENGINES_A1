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
    [SerializeField]
    private float fall_multiplier = 5.0f; //from https://youtu.be/7KiK0Aqtmzc (Better Jumping in Unity With Four Lines of Code)

    private Rigidbody rb = null;
    private Collider projectile_collider = null;

    public static float impulse_multiplier = 30.0f;
    public Vector3 ground_parallel_dir = Vector3.zero;
    public static float launch_angle = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        projectile_collider = GetComponent<SphereCollider>();

        launch_angle = GetAngleBetweenFwdVectAndGround(transform);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 collider_pos = projectile_collider.transform.position;
        Vector3 raycast_pos = new Vector3(collider_pos.x, projectile_collider.bounds.min.y, collider_pos.z);
        Debug.DrawRay(raycast_pos, Vector3.down * 0.2f, Color.green);

        impulse_dir = transform.forward;
        jump_input = Input.GetAxis("Jump");

        launch_angle = GetAngleBetweenFwdVectAndGround(transform);
        if (jump_input != 0)
        {
            has_attempted_impulse = true;
        }

        
    }

    private void FixedUpdate()
    {
        //// JUMP MODIFIERS FOR BETTER FEEL
        //if (rb.velocity.y < 0)
        //{
        //    rb.velocity += Vector3.up * Physics.gravity.y * fall_multiplier * Time.deltaTime; //using Time.deltaTime due to acceleration
        //}

        if (has_attempted_impulse && is_grounded)
        {
            has_attempted_impulse = false;
            is_grounded = false;
            AttemptImpulse();
        }

        // STOP MOVING
        if (is_grounded)
        {
            rb.velocity = Vector3.zero;
        }

        is_grounded = IsGrounded();
    }

    private void AttemptImpulse()
    {
        //rb.AddForce(impulse_dir.normalized * 2, ForceMode.Impulse);
        rb.velocity = impulse_dir.normalized * impulse_multiplier;
    }

    private bool IsGrounded()
    {
        Vector3 collider_pos = projectile_collider.transform.position;
        Vector3 raycast_pos = new Vector3(collider_pos.x, projectile_collider.bounds.min.y+0.1f, collider_pos.z); //Raycast needs to be INSIDE source collider https://forum.unity.com/threads/raycast-not-finding-objects-collider.323109/
        return Physics.Raycast(raycast_pos, Vector3.down, 0.2f);
    }

    public static float GetAngleBetweenFwdVectAndGround(Transform game_obj)
    {
        Vector3 ground_parallel_dir = Vector3.Cross(game_obj.transform.right, Vector3.up);
        return Mathf.Abs(Vector3.SignedAngle(ground_parallel_dir, game_obj.transform.forward, Vector3.up));
    }
}
