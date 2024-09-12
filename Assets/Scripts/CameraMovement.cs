using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 lastMousePos;
    [SerializeField] float mouseSensativity = 1;
    [SerializeField] float scrollSensativity = 1;
    [SerializeField] float keyboardSpeed = 1;   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            Vector2 mousePosDiff = Camera.main.ScreenToWorldPoint(lastMousePos) - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = mousePosDiff * mouseSensativity;
            transform.position += offset * Time.deltaTime;
        }

        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Vector3 offset = new (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            offset *= keyboardSpeed;
            offset *= Camera.main.orthographicSize;
            transform.position += offset * Time.deltaTime;
        }

        lastMousePos = Input.mousePosition;

        if(Input.mouseScrollDelta.y < 0 || Input.mouseScrollDelta.y > 0 )
        {
            float newCameraSize = Mathf.Clamp(Camera.main.orthographicSize + (Input.mouseScrollDelta.y * scrollSensativity * -1), 0.1f, 1000f);
            Camera.main.orthographicSize = newCameraSize;
        }
    }
}
