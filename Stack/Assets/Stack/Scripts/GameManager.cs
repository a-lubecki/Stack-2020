using UnityEngine;
using DG.Tweening;


public class GameManager : MonoBehaviour {


    [SerializeField] private bool isPlaying;
    [SerializeField] private bool isGameOver;
    [SerializeField] private TowerBehavior towerBehavior;
    [SerializeField] private UIDisplayBehavior uiDisplayBehavior;


    void Start() {

        uiDisplayBehavior.DisplayTitle();
    }

    void Update() {

        //handle click or touch depending of the game status
        if (Input.GetMouseButtonDown(0)) {

            if (isPlaying) {
                TryStackCurrentBlock();
            } else if (isGameOver) {
                ResetTower();
            } else {
                StartPlaying();
            }
        }
    }

    private void StartPlaying() {

        isPlaying = true;

        GenerateNextBlock();
    }

    private void StopPlaying() {

        isPlaying = false;
        isGameOver = true;

        uiDisplayBehavior.DisplayRetry();
    }

    private void ResetTower() {

        isGameOver = false;

        towerBehavior.ResetTower();

        //make the camera follow the new block
        Camera.main.transform.DOLocalMoveY(0, 0.2f);

        uiDisplayBehavior.DisplayTitle();
    }

    private void GenerateNextBlock() {

        towerBehavior.GenerateNextBlock();

        //make the camera follow the new block
        Camera.main.transform.DOLocalMoveY(towerBehavior.Level, 0.5f);

        uiDisplayBehavior.DisplayScore(towerBehavior.Level);
    }

    private void TryStackCurrentBlock() {

        var hasStacked = towerBehavior.StackCurrentBlock();

        if (hasStacked) {

            towerBehavior.IncrementLevel();
            GenerateNextBlock();

        } else {
            StopPlaying();
        }
    }

}
