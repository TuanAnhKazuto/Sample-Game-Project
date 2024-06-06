using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage2 : MonoBehaviour
{
    public string stageName = "Scene3";

    public void ChangeStage()
    {
        SceneManager.LoadScene(stageName);
    }

    public void OnTriggerEnter2D(Collider2D change)
    {
        if (change.CompareTag("Player"))
        {
            ChangeStage();
        }
    }
}
