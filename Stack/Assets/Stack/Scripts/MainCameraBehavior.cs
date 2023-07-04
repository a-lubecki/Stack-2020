using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;


public class MainCameraBehavior : MonoBehaviour {


    private List<string> images = new List<string>();
    private int imageIndex;
    
    Texture2D myTexture;
    public RawImage rawImage;
    public string imagePath;
    public CoinSystem coinSystem;
    [SerializeField] private RawImage imageBackground;
    [SerializeField] private RawImage imageBackground2;
    [SerializeField] private RawImage imageBackground3;
    [SerializeField] private RawImage imageBackground4;
    [SerializeField] private RawImage imageBackground5;
    [SerializeField] private RawImage imageBackground6;
    [SerializeField] private RawImage imageBackground7;
    [SerializeField] private RawImage imageBackground8;
    [SerializeField] private RawImage imageBackground9;
    [SerializeField] private RawImage imageBackground10;
    private ColorIncrementManager colorIncrementManager = new ColorIncrementManager();


    public void ResetPosition() {

        transform.DOLocalMoveY(0, 0.2f);

        UpdateBackgroundColor(UnityEngine.Random.Range(0.2f, 0.5f));
        ChangeRandomImagePath();
    }

    public void IncrementLevel(int level) {

        transform.DOLocalMoveY(-level, 0.1f);
        
        UpdateBackgroundColor(0.01f);
    }

    private void UpdateBackgroundColor(float changePercentage) {

        //animat color change based on the current color
        imageBackground.DOColor(colorIncrementManager.NewColorFromOther(imageBackground.color, changePercentage), 0.1f);
      
        imageBackground2.DOColor(colorIncrementManager.NewColorFromOther(imageBackground.color, changePercentage), 0.1f);
        imageBackground3.DOColor(colorIncrementManager.NewColorFromOther(imageBackground.color, changePercentage), 0.1f);
        imageBackground4.DOColor(colorIncrementManager.NewColorFromOther(imageBackground.color, changePercentage), 0.1f);
        imageBackground5.DOColor(colorIncrementManager.NewColorFromOther(imageBackground.color, changePercentage), 0.1f);
        imageBackground6.DOColor(colorIncrementManager.NewColorFromOther(imageBackground.color, changePercentage), 0.1f);
    }
    private void ChangeRandomImagePath()
    {
        int condicion = Random.Range(1, 6);

        switch (condicion)
        {
            case 1:
                imageBackground.gameObject.SetActive(true);
                imageBackground2.gameObject.SetActive(false);
                imageBackground3.gameObject.SetActive(false);
                imageBackground4.gameObject.SetActive(false);
                imageBackground5.gameObject.SetActive(false);
                imageBackground6.gameObject.SetActive(false);
                break;
            case 2:
                imageBackground.gameObject.SetActive(false);
                imageBackground2.gameObject.SetActive(true);
                imageBackground3.gameObject.SetActive(false);
                imageBackground4.gameObject.SetActive(false);
                imageBackground5.gameObject.SetActive(false);
                imageBackground6.gameObject.SetActive(false);
                break;
            case 3:
                imageBackground.gameObject.SetActive(false);
                imageBackground2.gameObject.SetActive(false);
                imageBackground3.gameObject.SetActive(true);
                imageBackground4.gameObject.SetActive(false);
                imageBackground5.gameObject.SetActive(false);
                imageBackground6.gameObject.SetActive(false);
                break;
            case 4:
                imageBackground.gameObject.SetActive(false);
                imageBackground2.gameObject.SetActive(false);
                imageBackground3.gameObject.SetActive(false);
                imageBackground4.gameObject.SetActive(true);
                imageBackground5.gameObject.SetActive(false);
                imageBackground6.gameObject.SetActive(false);
                break;
            case 5:
                imageBackground.gameObject.SetActive(false);
                imageBackground2.gameObject.SetActive(false);
                imageBackground3.gameObject.SetActive(false);
                imageBackground4.gameObject.SetActive(false);
                imageBackground5.gameObject.SetActive(true);
                imageBackground6.gameObject.SetActive(false);
                break;
            case 6:
                imageBackground.gameObject.SetActive(false);
                imageBackground2.gameObject.SetActive(false);
                imageBackground3.gameObject.SetActive(false);
                imageBackground4.gameObject.SetActive(false);
                imageBackground5.gameObject.SetActive(false);
                imageBackground6.gameObject.SetActive(true);
                break;
            default:
                imageBackground.gameObject.SetActive(false);
               
                imageBackground2.gameObject.SetActive(false);
                imageBackground3.gameObject.SetActive(false);
                imageBackground4.gameObject.SetActive(false);
                imageBackground5.gameObject.SetActive(false);
                imageBackground6.gameObject.SetActive(false);
                break;
        }
    }

    private Texture2D LoadTextureFromFile(string filePath)
    {
        // Carga la imagen desde el archivo
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        return texture;
    }
    // Ejemplo de uso
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeRandomImagePath();
        }
    }
    private Bounds CalculateTowerBounds()
    {
        // Encuentra todos los bloques en la torre
        BlockBehavior[] blocks = FindObjectsOfType<BlockBehavior>();

        // Si no hay bloques, retorna un Bounds vacío
        if (blocks.Length == 0)
        {
            return new Bounds();
        }

        // Inicializa los límites utilizando el primer bloque como referencia
        Bounds bounds = new Bounds(blocks[0].transform.position, Vector3.zero);

        // Encuentra los límites que encapsulan todos los bloques de la torre
        foreach (BlockBehavior block in blocks)
        {
            bounds.Encapsulate(block.transform.position);
        }

        return bounds;
    }

    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    public void RotateCameraX(float angle)
    {
        // Rotar la cámara en el eje X
        LeanTween.rotateX(gameObject, angle, 0.5f).setOnComplete(ReturnToOriginalRotation);
    }

    private void ReturnToOriginalRotation()
    {
        // Regresar la cámara a la posición original
        LeanTween.rotate(gameObject, originalRotation.eulerAngles, 0.5f);
    }
}
