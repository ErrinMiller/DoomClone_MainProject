using UnityEngine;
using System.Collections;

public class Player_Rotation : MonoBehaviour
{
    [Header("Player Information")]
    public Transform player_CameraTransform;
    public Transform player_Transform;

    [Header("Player Variables")]
    public Quaternion player_TransverseRotation;
    public Quaternion player_SagittalRotation;

    [Header("Mouse Variables")]
    [Range(0, 10)] public float mouse_XLookSensitivity = 1;
    [Range(0, 10)] public float mouse_YLookSensitivity = 1;

    [Header("Boolean Variables")]
    public bool player_IsIdle;
    [Space(5)]
    public bool player_IsLookingAround;


    void Start()
    {
        player_TransverseRotation = player_Transform.localRotation;
        player_SagittalRotation = player_CameraTransform.localRotation;
    }

    void Update()
    {
        VariableUpdates();
    }

    void VariableUpdates()
    {
        player_IsLookingAround = (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y") != 0) ? true : false;

        player_IsIdle = (!player_IsLookingAround) ? true : false;
    }

    void FixedUpdate()
    {
        PlayerRotation();
    }


    void PlayerRotation()
    {
        //Set the variables for player rotation and adjust for mouse sensitivity
        float xRotation;
        float yRotation;

        xRotation = Input.GetAxis("Mouse X") * mouse_XLookSensitivity;
        yRotation = Input.GetAxis("Mouse Y") * mouse_YLookSensitivity;

        //Transverse and Sagittal rotation must both be set to local rotation of the player and camera transforms in the start function

        //Sets the value used to rotate the player horizontally
        player_TransverseRotation *= Quaternion.Euler(0.0f, xRotation, 0.0f);

        //Sets the value used to rotate the camera vertically and applies a rotation clamp
        player_SagittalRotation *= Quaternion.Euler(-yRotation, 0.0f, 0.0f);
        player_SagittalRotation = ClampSagittalRotation(player_SagittalRotation);

        //Rotate player and player camera
        player_Transform.localRotation = player_TransverseRotation;
        player_CameraTransform.localRotation = player_SagittalRotation;
    }


    Quaternion ClampSagittalRotation(Quaternion quaternion)
    {
        //Clamps the sagitattal rotation (X) of the player's camera, preventing in from going higher than 90 and lower than -90. Sets the roots of Y and Z to 0, giving X a range of 0-1. That X value can then be repersented as an angle by using ArcTan and a conversion from radians to degrees. That angle of X is then clamped to rotate no more than 90 or -90 degrees and is re-entered into the quaternion as a radian tangent.

        quaternion.x /= quaternion.w;
        quaternion.y /= quaternion.w;
        quaternion.z /= quaternion.w;
        quaternion.w = 1.0f;

        float angle = 2.0f * Mathf.Rad2Deg * Mathf.Atan(quaternion.x);

        angle = Mathf.Clamp(angle, -90f, 90f);

        quaternion.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angle);

        return quaternion;
    }
}
