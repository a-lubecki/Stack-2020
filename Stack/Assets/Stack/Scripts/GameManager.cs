﻿using UnityEngine;
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


    private int perfectStackCount = 0;
    private int highScore = 0;
    private bool soundAchievement = false;

    void Start() {

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Mostrar el puntaje más alto en la interfaz de usuario
        uiDisplayBehavior.DisplayHighScore(highScore);

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
