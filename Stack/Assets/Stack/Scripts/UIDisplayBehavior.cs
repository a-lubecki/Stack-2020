using UnityEngine;
using TMPro;


public class UIDisplayBehavior : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private GameObject goTitle;
    [SerializeField] private TextMeshProUGUI textRetry;
    [SerializeField] private TextMeshProUGUI textHighScore;



    ///show title and hide score
    public void DisplayTitle() {

        goTitle.gameObject.SetActive(true);
        textScore.gameObject.SetActive(false);
        textRetry.gameObject.SetActive(false);
        textHighScore.gameObject.SetActive(true);
    }

    ///show score if not zero and hide title
    public void DisplayScore(int score) {

        goTitle.gameObject.SetActive(false);
        textScore.gameObject.SetActive(true);
        textRetry.gameObject.SetActive(false);
        textHighScore.gameObject.SetActive(false);
        if (score <= 0) {
            textScore.text = "";
        } else {
            textScore.text = score.ToString();
        }
    }

    ///show retry text whitouh hiding title or score
    public void DisplayRetry() {

        textRetry.gameObject.SetActive(true);
    }

    public void DisplayHighScore(int highScore)
    {
        textHighScore.gameObject.SetActive(true);
        textHighScore.text = "High Score: " + highScore.ToString();
    }

}
