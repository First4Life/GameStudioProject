using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float Angle = 90.0f;
    public float openSpeed = 2.0f;

    public bool open = false;

    float defaultRoatationAngle;
    float currentRotationAngle;
    float openTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultRoatationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeed;
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRoatationAngle + (open ? Angle : 0), openTime), transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.F))
        {
            open = !open;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
        }
    }
}
