using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private Vector3 impulse_dir;
    [SerializeField]
    private float impulse_multiplier = 0.0f;
    [SerializeField]
    private bool has_attempted_impulse = false;

    private Rigidbody rb = null;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (has_attempted_impulse)
        {
            has_attempted_impulse = false;
            AttemptImpulse();
        }
    }

    private void AttemptImpulse()
    {
        rb.AddForce(impulse_dir.normalized * impulse_multiplier, ForceMode.Impulse);
    }
}
