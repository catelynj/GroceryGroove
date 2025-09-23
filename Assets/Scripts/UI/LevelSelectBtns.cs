using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectBtns : MonoBehaviour
{
    public Slider difficultySlider;

    public void PlayLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangeDifficulty()
    {
        if(difficultySlider.value == 1)
        {
            GameManager.Instance.SetDifficulty(1); //hard mode
        }
        else
        {
            GameManager.Instance.SetDifficulty(0); //normal mode
        }
    }
}
