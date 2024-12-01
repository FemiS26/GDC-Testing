using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform playerTR;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform cameraTR;
    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 screenPosition = mainCamera.WorldToScreenPoint(playerTR.position);
        Vector3 newPost = new Vector3(math.clamp(playerTR.position.x, -2.6f, 2.6f), playerTR.position.y, - 10);
        Vector3 smoothPosition = Vector3.Lerp(cameraTR.position, newPost, 0.025f);
        cameraTR.position = smoothPosition;
    }
}
