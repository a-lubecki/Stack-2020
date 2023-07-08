using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class GameManager : MonoBehaviour {


    private static readonly int MIN_PERFECT_STACK_COUNT_TO_GROW = 5;


    [SerializeField] private bool isPlaying;
    [SerializeField] private bool isGameOver;
    [SerializeField] private TowerBehavior towerBehavior;
    [SerializeField] private MainCameraBehavior mainCameraBehavior;
    [SerializeField] private UIDisplayBehavior uiDisplayBehavior;
    [SerializeField] private AudioBehavior audioBehavior;
    [SerializeField] private CoinSystem coinSystem;
    [SerializeField] public Shop shop;
    private int perfectStackCount = 0;
    private int highScore = 0;
    private int coinCount = 0;
    private int skinN = 0;
    private bool soundAchievement = false;
    private int score = 0;
    private int matnum;

    void Awake()
    {
        Start();
    }
    void Start() {
        //PlayerPrefs.DeleteAll();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        coinCount = PlayerPrefs.GetInt("Coins", 0);
        skinN = PlayerPrefs.GetInt("Skin", 0);
        // Mostrar el puntaje más alto en la interfaz de usuario
        uiDisplayBehavior.DisplayCoinSystem(coinCount);
        uiDisplayBehavior.DisplayHighScore(highScore);
        
        
        uiDisplayBehavior.DisplayTitle();
    }

    void Update() {
       
        shop.PurchaseBase(skinN);
        if (Input.GetMouseButtonDown(0))
        {
            
            if (isPlaying)
            {
                TryStackCurrentBlock();
            }
            else if (isGameOver)
            {
                ResetTower();
            }
            else
            {
                // Obtener la posición del toque o clic
                Vector3 touchPosition = Input.mousePosition;

                // Verificar si la posición del toque está en la parte inferior de la pantalla
                if (touchPosition.y <= Screen.height * 0.35f)
                {
                    StartPlaying();
                }
            }
        }

    }

    private void StartPlaying() {

        isPlaying = true;
        perfectStackCount = 0;
        soundAchievement = false;
        uiDisplayBehavior.DisplayTitle();
        // uiDisplayBehavior.HideMessage();

        GenerateNextBlock();

        audioBehavior.PlaySoundStart();
    }

    private void StopPlaying() {

        isPlaying = false;
        isGameOver = true;


        uiDisplayBehavior.DisplayRetry();
        ScoreManager.SaveHighScore(towerBehavior.level);
        Handheld.Vibrate();
        score=0;

    }

    private void ResetTower() {

        isGameOver = false;

        towerBehavior.ResetTower();

        mainCameraBehavior.ResetPosition();

        uiDisplayBehavior.DisplayTitle();

        audioBehavior.PlaySoundRetry();
    }

    private void GenerateNextBlock() {

        towerBehavior.GenerateNextBlock();

        //make the camera follow the new block
        mainCameraBehavior.IncrementLevel(towerBehavior.level);
        score++;

        // Verifica si el puntaje es un múltiplo de 20
        if (score %1 == 0)
        {
            // Llama a la función CollectCoin del CoinSystem para recolectar una moneda
            coinSystem.CollectCoin(towerBehavior.level);
            audioBehavior.PlayCoinAdd();
        }
        uiDisplayBehavior.DisplayScore(towerBehavior.level);
        if (towerBehavior.level > highScore)
        {
            // Actualizar el puntaje más alto
            highScore = towerBehavior.level;

            // Guardar el nuevo puntaje más alto en PlayerPrefs
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            
            if (!soundAchievement )
            {
                audioBehavior.PlaySoundHighScore();

                uiDisplayBehavior.ShowStartMessage(highScore);
                soundAchievement=true;
            }
            
            
        }
        uiDisplayBehavior.UpdateHighScore(highScore);
    }

    private void TryStackCurrentBlock() {

        var hasStacked = towerBehavior.StackCurrentBlock();

        if (!hasStacked) {
            StopPlaying();
            return;
        }

        if (towerBehavior.hasPerfectStackPosition) {

            perfectStackCount++;
            audioBehavior.PlaySoundPerfectStack(perfectStackCount);

            //grow the top block of the tower to reward the player if he stacked perfectly several blocks
            if (perfectStackCount > MIN_PERFECT_STACK_COUNT_TO_GROW) {

                bool hasGrown = towerBehavior.GrowTopBlock();
                if (hasGrown) {
                    audioBehavior.PlaySoundGrowBlock();

                }
            }



        } else {

            perfectStackCount = 0;
            audioBehavior.PlaySoundBadStack();
        }

        towerBehavior.IncrementLevel();
        GenerateNextBlock();
        ScoreManager.SaveHighScore(towerBehavior.level);
        
    }


}
