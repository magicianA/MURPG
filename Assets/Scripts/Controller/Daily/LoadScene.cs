using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "15")
        {
            Destroy(GameObject.FindWithTag("Player"));
            Destroy(GameObject.FindWithTag("Canvas"));
            Destroy(GameObject.FindWithTag("MainCamera"));
            Destroy(GameObject.Find("EventSystem"));
        }
        SceneManager.LoadScene(sceneName);
    }
}
