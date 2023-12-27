using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera mainCamera;
    static public CameraFollow instance;
    private Vector3 cameraPosition;
    public bool boolChangeCameraPosition = false;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();

        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (boolChangeCameraPosition)
        {
            cameraPosition = GameManager.Instance.GetCameraPosition();
            positionUpdate(cameraPosition);
        }
    }

    public void positionUpdate(Vector3 changedPosition)
    {
        transform.position = Vector3.Lerp(transform.position, changedPosition, Time.deltaTime * 3);
    }
}
