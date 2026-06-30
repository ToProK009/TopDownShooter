using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultyMenu : MonoBehaviour
{
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public Button impossibleButton;

    void Start()
    {
        if (easyButton != null)
            easyButton.onClick.AddListener(() => StartGame(0.1f, 0.75f));
        if (normalButton != null)
            normalButton.onClick.AddListener(() => StartGame(0.3f, 1f));
        if (hardButton != null)
            hardButton.onClick.AddListener(() => StartGame(0.6f, 1.5f));
        if (impossibleButton != null)
            impossibleButton.onClick.AddListener(() => StartGame(1f, 2f));
    }

    void StartGame(float rate, float mult)
    {
        DifficultyManager.difficultyRate = rate;
        DifficultyManager.scoreMultiplier = mult;
        SceneManager.LoadScene("SampleScene");
    }
}
