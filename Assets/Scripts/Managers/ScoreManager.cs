using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public double blueScore = 1.0d;
    public double redScore = 1.0d;
    public double totalScore;

    public double CalculateScore()
    {
        return blueScore * redScore;
    }

    private void Awake()
    {
        instance = this;
    }
}
