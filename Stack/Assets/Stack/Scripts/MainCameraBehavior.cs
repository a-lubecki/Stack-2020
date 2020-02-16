using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class MainCameraBehavior : MonoBehaviour {



    [SerializeField] private RawImage imageBackground;


    public void ResetPosition() {

        transform.DOLocalMoveY(0, 0.2f);

        UpdateBackgroundColor(0.2f);
    }

    public void IncrementLevel(int level) {

        transform.DOLocalMoveY(level, 0.5f);

        UpdateBackgroundColor(0.03f);
    }

    private void UpdateBackgroundColor(float changePercentage) {

        float h, s, v;
        Color.RGBToHSV(imageBackground.color, out h, out s, out v);

        var newColor = Random.ColorHSV(
            h - changePercentage,
            h + changePercentage,
            s - changePercentage,
            s + changePercentage,
            v - changePercentage,
            v + changePercentage
        );

        imageBackground.DOColor(newColor, 0.5f);
    }

}
