using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Lean.Pool;
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
    [SerializeField] private BlockBehavior blockBehavior;
    private int perfectStackCount = 0;
    private int highScore = 0;
    private int coinCount = 0;
    private int skinN = 0;
    private bool soundAchievement = false;
    private int score = 0;
    private int matnum;
    public GameObject _enemy;
    private bool enemyAwake;
    private int aux=0;
    [SerializeField] private Transform enemyInit;

    void Awake()
    {
        Start();
    }
    void Start() {
        enemyAwake=false;
        //PlayerPrefs.DeleteAll();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        coinCount = PlayerPrefs.GetInt("Coins", 0);
        skinN = PlayerPrefs.GetInt("Skin", 0);
        enemyInit.position = new Vector3(0f, 0f, 0f);
        // Mostrar el puntaje más alto en la interfaz de usuario
        uiDisplayBehavior.DisplayCoinSystem(coinCount);
        uiDisplayBehavior.DisplayHighScore(highScore);
        
        
        uiDisplayBehavior.DisplayTitle();
    }

    void Update() {
       
        shop.PurchaseBase(skinN);
        if (Input.GetMouseButtonDown(0))
        {// Obtener la posición del toque o clic
            Vector3 touchPosition1 = Input.mousePosition;
            // Verificar si la posición del toque está en la parte inferior de la pantalla
            if (touchPosition1.y <= Screen.height * 0.35f)
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

    }

    private void StartPlaying() {
       // blockBehavior.BreakingPlatforms(true);
        isPlaying = true;
        perfectStackCount = 0;
        soundAchievement = false;
        uiDisplayBehavior.DisplayTitle();
        // uiDisplayBehavior.HideMessage();
        
        GenerateNextBlock();
       
        //enemy.GenerateNewFruit();
        audioBehavior.PlaySoundStart();
    }

    private void StopPlaying() {
       //towerBehavior.RotateTower(90f, 75f,0f);
        //blockBehavior.BreakingPlatforms();
        isPlaying = false;
        isGameOver = true;

        enemyInit.position = new Vector3(0f, 0f, 0f);
        uiDisplayBehavior.DisplayRetry();
        ScoreManager.SaveHighScore(towerBehavior.level);
        Handheld.Vibrate();
        score=0;
        
    }

    private void ResetTower() {

        // towerBehavior.resetRotateTower();
        //blockBehavior.BreakingPlatforms();
        isGameOver = false;

        towerBehavior.ResetTower();

        mainCameraBehavior.ResetPosition();

        uiDisplayBehavior.DisplayTitle();
        enemyInit.position = new Vector3(0f, 0f, 0f);
        audioBehavior.PlaySoundRetry();
        //enemy.ResetPosition();
        


    }

    private void GenerateNextBlock() {

        if (aux<9)
        {
            generateEnemy();
            aux++;
        }
        else
        {
            aux=0;
        }
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

    private void generateEnemy()
    {
        LeanPool.Spawn(_enemy, enemyInit.position, Quaternion.identity);
        enemyInit.position += new Vector3(0f, 15f, 0f);
        
        
    }
}
