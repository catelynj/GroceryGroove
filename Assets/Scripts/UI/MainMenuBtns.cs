using UnityEngine;

public class MainMenuBtns : MonoBehaviour
{
    public void PlayGame()
    {
        UIController.Instance.LoadLevelSelect();
    }

    public void ExitGame()
    {
        UIController.Instance.ExitGame();
    }
}
