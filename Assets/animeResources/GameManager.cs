using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Vector3 manCharacterPosition;
    private Vector3 cameraPosition = new Vector3(0,0,0);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetManCharacterPosition(Vector3 position)
    {
        manCharacterPosition = position;
    }

    public Vector3 GetManCharacterPosition()
    {
        return manCharacterPosition;
    }
    public void SetCameraPosition(Vector3 position)
    {
        cameraPosition = position;
    }

    public Vector3 GetCameraPosition()
    {
        return cameraPosition;
    }
}