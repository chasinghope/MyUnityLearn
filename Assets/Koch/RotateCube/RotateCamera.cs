using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Transform Camera;
    public AudioPeer _audioPeer;

    public Vector3 RotateAxis;
    public Vector3 RotateSpeed;

    void Update()
    {
        Camera.LookAt(this.transform);

        this.transform.Rotate(RotateAxis.x * RotateSpeed.x * Time.deltaTime * _audioPeer._AmlitudeBuffer,
            RotateAxis.y * RotateSpeed.y * Time.deltaTime * _audioPeer._AmlitudeBuffer,
            RotateAxis.z * RotateAxis.z * Time.deltaTime * _audioPeer._AmlitudeBuffer);
    }
}
