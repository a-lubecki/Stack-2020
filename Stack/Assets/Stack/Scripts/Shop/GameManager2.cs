using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class GameManager2 : MonoBehaviour {




    [SerializeField] private bool isPlaying;
    [SerializeField] private bool isGameOver;
    [SerializeField] private TowerBehavior2 towerBehavior;
    [SerializeField] private MainCameraBehavior mainCameraBehavior;
    [SerializeField] private UIDisplayBehavior uiDisplayBehavior;
    [SerializeField] private AudioBehavior audioBehavior;
    [SerializeField] private CoinSystem coinSystem;
    [SerializeField] private Shop shop;

    private int perfectStackCount = 0;
    private int highScore = 0;
    private int coinCount;
    private bool soundAchievement = false;
    private int score = 0;
    public int matnum;
    public int priceSkin;
    void Start() {

        coinCount=PlayerPrefs.GetInt("Coins", 0) ;
        // Mostrar el puntaje más alto en la interfaz de usuario
        uiDisplayBehavior.DisplayCoinSystem(coinCount);
        
        uiDisplayBehavior.DisplayTitle();
    }

    void Update() {

        
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 touchPosition = Input.mousePosition;
            
            // Verificar si la posición del toque está en la parte inferior de la pantalla
            if (touchPosition.y <= Screen.height * 0.35f)
            {

                priceSkin=matnum*1000;
                uiDisplayBehavior.DisplayPriceSkin(priceSkin);
                matnum++;

                // Reiniciar el contador si llega a 15
                if (matnum > 15)
                {
                    //print(matnum);
                    matnum -=15;
                   
                }
                // matnum = UnityEngine.Random.Range(0, 10);
                //print("runtime" + matnum);
                shop.PurchaseBase(matnum);
            }
            uiDisplayBehavior.DisplayCoinSystem(coinCount);
        //    if (isPlaying)
        //    {
        //        TryStackCurrentBlock();
        //    }
        //    else if (isGameOver)
        //    {
        //        ResetTower();
        //    }
        //    else
        //    {
        //        // Obtener la posición del toque o clic
        //        Vector3 touchPosition = Input.mousePosition;

            //        // Verificar si la posición del toque está en la parte inferior de la pantalla
            //        if (touchPosition.y <= Screen.height * 0.35f)
            //        {
            //            StartPlaying();
            //        }
            //    }
        }

    }



}
