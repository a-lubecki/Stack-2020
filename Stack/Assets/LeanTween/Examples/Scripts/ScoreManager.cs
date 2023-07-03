using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int highScore;

    private void Start()
    {
        // Cargar la puntuación más alta almacenada previamente
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public static void SaveHighScore(int score)
    {
        if (score > highScore)
        {
            // Guardar la nueva puntuación más alta
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            print(highScore);
        }
    }
}
