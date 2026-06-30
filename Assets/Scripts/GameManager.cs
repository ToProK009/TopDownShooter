using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int coins;
    private int score;
    private int highScore;
    private Text coinText;
    private Text scoreText;
    private Text highScoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Bullet.globalSpeedBonus = 0f;
        Bullet.killSound = Resources.Load<AudioClip>("Sound/KillMeteor");
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        CreateUI();
    }

    void CreateUI()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }

        coinText = MakeText(canvas, "CoinText", new Vector2(-15, -10), 28);
        coinText.text = "$0";
        coinText.fontSize = 28;

        scoreText = MakeText(canvas, "ScoreText", new Vector2(-15, -50), 20);
        scoreText.text = "Score\n0";
        scoreText.fontSize = 20;

        highScoreText = MakeText(canvas, "HighScoreText", new Vector2(-15, -90), 16);
        highScoreText.text = "Best\n" + highScore.ToString("N0");
        highScoreText.fontSize = 16;
        highScoreText.color = new Color(1, 0.8f, 0.2f);

        RectTransform hr = highScoreText.rectTransform;
        hr.sizeDelta = new Vector2(200, 40);
    }

    Text MakeText(Canvas canvas, string name, Vector2 pos, int size)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(canvas.transform, false);

        Text txt = obj.AddComponent<Text>();
        txt.fontSize = size;
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.color = Color.white;
        txt.alignment = TextAnchor.UpperRight;

        RectTransform rect = txt.rectTransform;
        rect.anchorMin = new Vector2(1, 1);
        rect.anchorMax = new Vector2(1, 1);
        rect.pivot = new Vector2(1, 1);
        rect.anchoredPosition = pos;
        rect.sizeDelta = new Vector2(200, 50);

        return txt;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            SceneManager.LoadScene("Menu");
    }

    public int GetCoins() { return coins; }

    public bool SpendCoins(int amount)
    {
        if (coins < amount) return false;
        coins -= amount;
        if (coinText != null)
            coinText.text = "$" + coins.ToString("N0");
        return true;
    }

    public void AddCoins(int amount)
    {
        coins += Mathf.RoundToInt(amount * DifficultyManager.scoreMultiplier);
        if (coinText != null)
            coinText.text = "$" + coins.ToString("N0");
    }

    public void AddScore(int amount)
    {
        score += Mathf.RoundToInt(amount * DifficultyManager.scoreMultiplier);
        if (scoreText != null)
            scoreText.text = "Score\n" + score.ToString("N0");

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            if (highScoreText != null)
                highScoreText.text = "Best\n" + highScore.ToString("N0");
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Menu");
    }
}
