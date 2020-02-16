using UnityEngine;


public class GameManager : MonoBehaviour {


    private static readonly int MIN_PERFECT_STACK_COUNT_TO_GROW = 7;


    [SerializeField] private bool isPlaying;
    [SerializeField] private bool isGameOver;
    [SerializeField] private TowerBehavior towerBehavior;
    [SerializeField] private MainCameraBehavior mainCameraBehavior;
    [SerializeField] private UIDisplayBehavior uiDisplayBehavior;
    [SerializeField] private AudioBehavior audioBehavior;

    private int perfectStackCount = 0;


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
        perfectStackCount = 0;

        GenerateNextBlock();

        audioBehavior.PlaySoundStart();
    }

    private void StopPlaying() {

        isPlaying = false;
        isGameOver = true;

        uiDisplayBehavior.DisplayRetry();
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

        uiDisplayBehavior.DisplayScore(towerBehavior.level);
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
            if (perfectStackCount >= MIN_PERFECT_STACK_COUNT_TO_GROW) {

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
    }

}
