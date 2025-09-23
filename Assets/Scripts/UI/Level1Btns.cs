using UnityEngine;

public class Level1Btns : MonoBehaviour
{
    public GameObject PausePanel;

    private void Start()
    {
        if (PausePanel != null)
        {
            PausePanel.SetActive(false);
        }
    }

    public void TogglePauseMenu()
    {
        UIController.Instance.TogglePause();
        if (PausePanel != null)
        {
            PausePanel.SetActive(UIController.Instance.isPaused);
        }
    }

    public void ExitLevel()
    {
        UIController.Instance.LoadMainMenu();
    }
}
