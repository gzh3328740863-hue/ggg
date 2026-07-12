using UnityEngine;

/// <summary>
/// 分数管理器 - 负责游戏中的分数计算和管理
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private int pointsPerSecond = 10;
    [SerializeField] private int pointsPerObstacleAvoided = 50;

    private int currentScore = 0;
    private int highScore = 0;
    private float timeSinceLastPoint = 0f;

    public int CurrentScore => currentScore;
    public int HighScore => highScore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LoadHighScore();
    }

    private void Start()
    {
        currentScore = 0;
        timeSinceLastPoint = 0f;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameActive || GameManager.Instance.IsPaused) return;

        // 每秒增加分数
        timeSinceLastPoint += Time.deltaTime;
        if (timeSinceLastPoint >= 1f)
        {
            AddScore(pointsPerSecond);
            timeSinceLastPoint = 0f;
        }
    }

    /// <summary>
    /// 添加分数
    /// </summary>
    public void AddScore(int points)
    {
        currentScore += points;
        UIManager.Instance?.UpdateScoreDisplay(currentScore);
    }

    /// <summary>
    /// 记录躲避障碍获得的分数
    /// </summary>
    public void OnObstacleAvoided()
    {
        AddScore(pointsPerObstacleAvoided);
    }

    /// <summary>
    /// 检查并更新最高分
    /// </summary>
    public void CheckAndUpdateHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
        }
    }

    /// <summary>
    /// 保存最高分到本地
    /// </summary>
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 从本地加载最高分
    /// </summary>
    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    /// <summary>
    /// 重置分数
    /// </summary>
    public void ResetScore()
    {
        currentScore = 0;
        timeSinceLastPoint = 0f;
        UIManager.Instance?.UpdateScoreDisplay(currentScore);
    }
}
