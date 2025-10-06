using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private Button button;
    private bool isPaused = false;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(TogglePause);
    }

    void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f; 
        }
        else
        {
            Time.timeScale = 0f; 
        }

        isPaused = !isPaused;
    }
}
