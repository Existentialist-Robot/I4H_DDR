using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigurationMenu : MonoBehaviour
{
    GameManager gameManager;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void PlayGame ()
    {
        Debug.Log("Moving from Configuration Scene to next scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Back()
    {
        Debug.Log("Moving from Configuration Scene to previous scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ArrowButtonOnClick(int idx) {
        bool valid = ValidateArrowConfiguration(idx);

        if (valid) {
            gameManager.UpdateArrowConfiguration(idx);
        }
    }

    bool ValidateArrowConfiguration(int idx) {
        // 

        return false;
    }
}
