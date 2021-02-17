using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform orbiting_obj;
    public Transform look_at_target;
    public Transform indicator_obj;

    [SerializeField]
    private float distance = 50.0f;
    [SerializeField]
    private float zoom_speed = 15.0f;

    [SerializeField]
    private float x_speed = 150.0f;
    [SerializeField]
    private float y_speed = 50.0f;

    [SerializeField]
    private int y_min_limit = -723;
    [SerializeField]
    private int y_max_limit = 877;

    [SerializeField]
    private float x_movement = 0.0f;
    [SerializeField]
    private float y_movement = 0.0f;

    [SerializeField]
    private float x_movement2 = 0.0f;
    [SerializeField]
    private float y_movement2 = 0.0f;

    [SerializeField]
    private int y_min_limit2 = -90;
    [SerializeField]
    private int y_max_limit2 = -10;

    // Start is called before the first frame update
    public void Start()
    {
        Vector3 angles = orbiting_obj.eulerAngles;
        x_movement = angles.y;
        y_movement = angles.x;

        Vector3 target_angles = look_at_target.eulerAngles;
        y_movement2 = target_angles.x;
        //// Make the rigid body not change rotation
        //if (rb)
        //    rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // ADJUST ANGLE
        y_movement2 += Input.GetAxis("Vertical2") * y_speed * 0.02f;

        y_movement2 = ClampAngle(y_movement2, y_min_limit2, y_max_limit2);

        Quaternion rotation = Quaternion.Euler(y_movement2, 0.0f, 0.0f);

        look_at_target.rotation = new Quaternion(rotation.x, look_at_target.rotation.y, look_at_target.rotation.z, look_at_target.rotation.w);

        // ADJUST STRENGTH
        x_movement2 = Input.GetAxis("Horizontal2");
    }

    public void LateUpdate()
    {
        if (look_at_target)
        {
            // ADJUST CAMERA
            x_movement -= Input.GetAxis("Horizontal") * x_speed * 0.02f;
            y_movement += Input.GetAxis("Vertical") * y_speed * 0.02f;

            y_movement = ClampAngle(y_movement, y_min_limit, y_max_limit);

            distance -= Input.GetAxis("Fire1") * zoom_speed * 0.02f;
            distance += Input.GetAxis("Fire2") * zoom_speed * 0.02f;

            Quaternion rotation = Quaternion.Euler(-y_movement, -x_movement, 0.0f);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + look_at_target.position;

            orbiting_obj.rotation = rotation;
            orbiting_obj.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360.0f)
            angle += 360.0f;
        if (angle > 360.0f)
            angle -= 360.0f;
        return Mathf.Clamp(angle, min, max);
    }

    
}