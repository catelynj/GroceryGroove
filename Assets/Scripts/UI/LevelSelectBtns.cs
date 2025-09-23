using UnityEngine;

public class LevelSelectBtns : MonoBehaviour
{
    public void PlayLevel()
    {
        UIController.Instance.LoadLevel();
    }

    public void ReturnHome()
    {
        UIController.Instance.LoadMainMenu();
    }
}
