using UnityEngine;
using TMPro;


public class UIDisplayBehavior : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private GameObject goTitle;
    [SerializeField] private TextMeshProUGUI textRetry;


    ///show title and hide score
    public void DisplayTitle() {

        goTitle.gameObject.SetActive(true);
        textScore.gameObject.SetActive(false);
        textRetry.gameObject.SetActive(false);
    }

    ///show score if not zero and hide title
    public void DisplayScore(int score) {

        goTitle.gameObject.SetActive(false);
        textScore.gameObject.SetActive(true);
        textRetry.gameObject.SetActive(false);

        if (score <= 0) {
            textScore.text = "";
        } else {
            textScore.text = score.ToString();
        }
    }

    ///show retry text and hide title and score
    public void DisplayRetry() {

        textRetry.gameObject.SetActive(true);
    }

}
