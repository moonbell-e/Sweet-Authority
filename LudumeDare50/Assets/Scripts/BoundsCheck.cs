using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Предотвращает выход игрового объекта за границы экрана.
/// Важно: работает ТОЛЬКО с ортографической камерой Main Camera в [ 0, 0, 0 ].
/// </summary>

public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public bool keepOnScreen = true;

    [Header("Set Dynamically")]
    private bool isOnScreen = true;
    private float camWidth;
    private float camHeight;

    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;

    private float borderHeight, borderWidth, borderDepth;
    private float radiusH, radiusW;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;

        SetRadius();
    }


    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;


        //Выход за границы экрана
        if (pos.x > camWidth - radiusW)
        {
            pos.x = camWidth - radiusW;
            offRight = true;
        }
        if (pos.x < -camWidth + radiusW)
        {
            pos.x = -camWidth + radiusW;
            offLeft = true;
        }
        if (pos.z > camHeight - radiusH)
        {
            pos.z = camHeight - radiusH;
            offUp = true;
        }
        if (pos.z < -camHeight + radiusH)
        {
            pos.z = -camHeight + radiusH;
            offDown = true;
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }
    }

    public void SetRadius()
    {
        radiusH = Camera.main.orthographicSize;
        radiusW = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2f, 0.1f, camHeight * 2f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boundSize * 1.01f);
    }
}