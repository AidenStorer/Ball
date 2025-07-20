using UnityEngine;

public class GyroDebugger : MonoBehaviour
{
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            Debug.Log("Gyro enabled.");
        }
        else
        {
            Debug.Log("Gyro not supported on this device.");
        }
    }

    void Update()
    {
        if (!SystemInfo.supportsGyroscope) return;

        Debug.Log("Gyro values:"
            + " RotationRate: " + Input.gyro.rotationRate
            + " Gravity: " + Input.gyro.gravity
            + " UserAcceleration: " + Input.gyro.userAcceleration
            + " Attitude (Euler): " + Input.gyro.attitude.eulerAngles);
    }
}