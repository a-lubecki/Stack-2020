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

        transform.DOLocalMoveY(level, 0.5f);

        UpdateBackgroundColor(0.01f);
    }

    private void UpdateBackgroundColor(float changePercentage) {

        //animat color change based on the current color
        imageBackground.DOColor(colorIncrementManager.NewColorFromOther(imageBackground.color, changePercentage), 0.5f);
    }

}
