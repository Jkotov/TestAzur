using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Camera cameraToPlayer;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private string isRunningAnimationVariable = "isRunning";
    [SerializeField] private float mouseSensitivity;
    private float speed;
    private Vector3 prevWorldPoint;
    private Vector3 prevMousePos;
    private Animator animator;
    private int isRunningAnimationVariableId;
    private bool isRunning;

    public bool CanRun { get; set; }
    
    private float Speed
    {
        get => speed;
        set
        {
            speed = Mathf.Clamp(value, 0, maxSpeed);
            IsRunning = !(speed <= Mathf.Epsilon);
        }
    }

    private bool IsRunning
    {
        get => isRunning;
        set
        {
            if (value != IsRunning)
            {
                isRunning = value;
                animator.SetBool(isRunningAnimationVariableId, value);
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isRunningAnimationVariableId = Animator.StringToHash(isRunningAnimationVariable);
    }

    private void Update()
    {
        var mousePos = Input.mousePosition;
        var screenPos = mousePos;
        var newPos = transform.position;
        screenPos.z = newPos.z - cameraToPlayer.transform.position.z;
        var worldPoint = cameraToPlayer.ScreenToWorldPoint(screenPos);
        if (Input.GetMouseButton(0) && CanRun)
        {
            Speed += acceleration * Time.deltaTime;
            if (mousePos != prevMousePos)
                newPos.x += (worldPoint.x - prevWorldPoint.x) * mouseSensitivity;
            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        }
        else
        {
            Speed -= acceleration * Time.deltaTime;
        }
        newPos.z += Speed * Time.deltaTime;
        transform.position = newPos;
        prevWorldPoint = worldPoint;
        prevMousePos = mousePos;
    }
}
