using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraO : MonoBehaviour
{
    public Transform target;
    public float distance = -12.01f; 
    public float rotationSpeed = 10.0f; 

    private float currentAngle = 0.0f;

    void Update()
    {
        if (target)
        {
            currentAngle += rotationSpeed * Time.deltaTime;

            float radians = currentAngle * Mathf.Deg2Rad; 
            float x = Mathf.Sin(radians) * distance;
            float z = Mathf.Cos(radians) * distance;

            Vector3 position = new Vector3(x, 3.38f, z) + target.position; 
            transform.position = position;

            transform.LookAt(target);
        }
    }
}
