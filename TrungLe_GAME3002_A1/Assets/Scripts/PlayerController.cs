using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform orbiting_obj;
    public Transform look_at_target;
    public Transform indicator_obj;
    
    public float distance = 50.0f;
    public float zoom_speed = 2.0f;

    public float x_speed = 150.0f;
    public float y_speed = 50.0f;

    public int y_min_limit = -723;
    public int y_max_limit = 877;

    private float x_movement = 0.0f;
    private float y_movement = 0.0f;

    public void Start()
    {
        Vector3 angles = orbiting_obj.eulerAngles;
        x_movement = angles.y;
        y_movement = angles.x;

        //// Make the rigid body not change rotation
        //if (rb)
        //    rb.freezeRotation = true;
    }

    public void LateUpdate()
    {
        if (look_at_target)
        {
            x_movement -= Input.GetAxis("Horizontal") * x_speed * 0.02f;
            y_movement += Input.GetAxis("Vertical") * y_speed * 0.02f;

            y_movement = ClampAngle(y_movement, y_min_limit, y_max_limit);

            distance -= Input.GetAxis("Fire1") * zoom_speed * 0.02f;
            distance += Input.GetAxis("Fire2") * zoom_speed * 0.02f;

            Quaternion rotation = Quaternion.Euler(y_movement, x_movement, 0.0f);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + look_at_target.position;

            orbiting_obj.rotation = rotation;
            orbiting_obj.position = position;

            Quaternion reverse_rotation = Quaternion.Euler(-y_movement, x_movement, 0.0f);
            look_at_target.rotation = reverse_rotation;
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

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}