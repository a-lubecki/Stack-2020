using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIDisplayBehavior : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private GameObject goTitle;
    [SerializeField] private TextMeshProUGUI textRetry;
    [SerializeField] private TextMeshProUGUI textHighScore;
    [SerializeField] private TextMeshProUGUI messageText;

    private bool hasShownStartMessage = false;
    private int previousHighScore = 0;

    ///show title and hide score
    public void DisplayTitle() {

        goTitle.gameObject.SetActive(true);
        textScore.gameObject.SetActive(false);
        textRetry.gameObject.SetActive(false);
        textHighScore.gameObject.SetActive(true);
        
    }

    ///show score if not zero and hide title
    public void DisplayScore(int score) {
        hasShownStartMessage=true;
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

    public void UpdateHighScore(int score)
    {
        // Mostrar el puntaje más alto en la interfaz de usuario
        textHighScore.text =  "High Score: " + score.ToString();
    }

    public void ShowStartMessage(int currentHighScore)
    {

            // Mostrar el mensaje
            messageText.gameObject.SetActive(true);
            messageText.text = "¡Record Superado!";

            // Desvanecer el mensaje después de unos segundos
            StartCoroutine(FadeOutMessage());

            hasShownStartMessage = true; // Actualizar la bandera para indicar que se mostró el mensaje
        
    }

    private IEnumerator FadeOutMessage()
    {
        yield return new WaitForSeconds(2f); // Esperar 2 segundos

        float fadeDuration = 0f; // Duración del desvanecimiento
        float elapsedTime = 0f; // Tiempo transcurrido desde el inicio del desvanecimiento

        while (elapsedTime < fadeDuration)
        {
            // Calcular el alpha (transparencia) del mensaje
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            Color textColor = messageText.color;
            textColor.a = alpha;
            messageText.color = textColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ocultar el mensaje al finalizar el desvanecimiento
        messageText.gameObject.SetActive(false);
    }
}
