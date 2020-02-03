using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public bool changeSceneOnKey = false;
    public string changeSceneTo = "";
    private float startTime = 0f;

    private void Awake()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > startTime + 3f && changeSceneOnKey && Input.anyKeyDown)
        {
            SceneManager.LoadScene(changeSceneTo);
        }
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
