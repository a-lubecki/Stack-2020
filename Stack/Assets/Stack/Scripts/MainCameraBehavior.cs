using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class MainCameraBehavior : MonoBehaviour {



    [SerializeField] private RawImage imageBackground;

    private ColorIncrementManager colorIncrementManager = new ColorIncrementManager();


    public void ResetPosition() {

        transform.DOLocalMoveY(0, 0.2f);

        UpdateBackgroundColor(UnityEngine.Random.Range(0.2f, 0.5f));
    }

    public void IncrementLevel(int level) {

        transform.DOLocalMoveY(-level, 0.1f);

        UpdateBackgroundColor(0.01f);
    }

    private void UpdateBackgroundColor(float changePercentage) {

        //animat color change based on the current color

        imageBackground.DOColor(colorIncrementManager.NewColorFromOther(imageBackground.color, changePercentage), 0.1f);
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

}
