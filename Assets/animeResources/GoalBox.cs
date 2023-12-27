using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalBox : MonoBehaviour
{
    public string transferMapName;
    public Vector3 ArrivePosition;
    private manChaMove thePlayer;
    private bool boolChangeCameraPosition;
    manChaMove manCha;
    void Start()
    {
        if (thePlayer == null)
            thePlayer = FindObjectOfType<manChaMove>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            manCha = collision.gameObject.GetComponent<manChaMove>();

            manCha.MapChange = true;
            SceneTransformerManager.Instance.LoadScene(transferMapName);
            Invoke("lateTime", 0.5f);
        }
    }

    private void lateTime()
    {
        Vector3 newManCharacterPosition = ArrivePosition;
        manCha.transform.position = newManCharacterPosition;
    }
}