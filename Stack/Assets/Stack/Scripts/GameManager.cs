using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {


    [SerializeField] private bool isPlaying;
    [SerializeField] private bool isGameOver;
    [SerializeField] private TowerBehavior towerBehavior;


    void Update() {

        if (Input.GetMouseButtonDown(0)) {

            if (isPlaying) {
                StackCurrentBlock();
            } else if (isGameOver) {
                ResetTower();
            } else {
                StartPlaying();
            }
        }
    }

    private void StartPlaying() {

        isPlaying = true;

        towerBehavior.GenerateNextBlock();
    }

    private void StackCurrentBlock() {

        var hasStacked = towerBehavior.StackCurrentBlock();

        if (hasStacked) {

            towerBehavior.IncrementLevel();
            towerBehavior.GenerateNextBlock();

            ///TODO cam

        } else {
            StopPlaying();
        }
    }

    private void StopPlaying() {

        isPlaying = false;
        isGameOver = true;

        ///TODO cam
    }

    private void ResetTower() {

        isGameOver = false;

        towerBehavior.ResetTower();

        ///TODO cam
    }

}
