using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockBehavior : MonoBehaviour {


    ///the max position of the moving animation
    public static readonly float MAX_AMPLITUDE = 7;
    public static readonly float MIN_SPEED = 10;
    public static readonly float MAX_SPEED = 17;
    ///the number of levels before incrementing the speed
    public static readonly int LAST_LEVEL_FOR_MIN_SPEED = 10;
    ///the number of levels when the speed must stop incrementing
    public static readonly int FIRST_LEVEL_FOR_MAX_SPEED = 50;


    [SerializeField] private bool mustMoveOnXAxis;
    [SerializeField] private bool mustMoveOnPositiveDirection;
    [SerializeField] private bool isMoving;
    [SerializeField] private float speed;


    public bool MustMoveOnXAxis {
        get {
            return mustMoveOnXAxis;
        }
    }

    void Update() {

        Move();
    }

    public void Init(int level, bool mustMoveOnXAxis) {

        this.mustMoveOnXAxis = mustMoveOnXAxis;
        mustMoveOnPositiveDirection = false;

        //calculate new speed based on level
        speed = CalculateNewSpeed(level);

        //update pos, preparing for moving
        transform.localPosition = new Vector3(0, level, 0);
        UpdatePosition(MAX_AMPLITUDE);
    }

    public void StartMoving() {

        isMoving = true;
    }

    public void StopMoving() {

        isMoving = false;
    }

    private void Move() {

        if (!isMoving) {
            return;
        }

        var axisPos = GetAxisPosition();
        var advance = Time.deltaTime * speed;

        //inverse advance if negative
        if (!mustMoveOnPositiveDirection) {
            advance = -advance;
        }

        UpdatePosition(axisPos + advance);

        //when the block has reached the max amplitude, inverse the direction
        axisPos = GetAxisPosition();
        if (axisPos <= -MAX_AMPLITUDE || axisPos >= MAX_AMPLITUDE) {
            mustMoveOnPositiveDirection = !mustMoveOnPositiveDirection;
        }
    }

    private float GetAxisPosition() {

        if (mustMoveOnXAxis) {
            return transform.localPosition.x;
        }

        return transform.localPosition.z;
    }

    private void UpdatePosition(float axisPos) {

        //capping of the pos
        if (axisPos < -MAX_AMPLITUDE) {
            axisPos = -MAX_AMPLITUDE;
        } else if (axisPos > MAX_AMPLITUDE) {
            axisPos = MAX_AMPLITUDE;
        }

        //update pos base on axis
        var pos = transform.localPosition;

        if (mustMoveOnXAxis) {
            pos.x = axisPos;
        } else {
            pos.z = axisPos;
        }

        transform.localPosition = pos;
    }

    public void Fall() {

        ///TODO
    }

    private static float CalculateNewSpeed(int level) {

        if (level <= LAST_LEVEL_FOR_MIN_SPEED) {
            return MIN_SPEED;
        }

        if (level >= FIRST_LEVEL_FOR_MAX_SPEED) {
            return MAX_SPEED;
        }

        //get the percentage between min an max levels
        var levelPercentage = (level - LAST_LEVEL_FOR_MIN_SPEED) / (float)(FIRST_LEVEL_FOR_MAX_SPEED - LAST_LEVEL_FOR_MIN_SPEED);
        //return the speed with the percentage (linear calculation)
        return MIN_SPEED + levelPercentage * (MAX_SPEED - MIN_SPEED);
    }

}
