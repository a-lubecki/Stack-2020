using System;
using UnityEngine;
using Lean.Pool;


public class BlockBehavior : MonoBehaviour {


    ///the max position of the moving animation
    public static readonly float MAX_AMPLITUDE = 6;

    public static readonly float MIN_SPEED = 11;
    public static readonly float MAX_SPEED = 20;

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

    public void SetAsKinematic(bool isKinematic) {

        GetComponent<Rigidbody>().isKinematic = isKinematic;
    }

    public void Init(int level, bool mustMoveOnXAxis, Vector2 initialHorizontalPos, Vector2 initialHorizontalSize) {

        SetAsKinematic(true);

        this.mustMoveOnXAxis = mustMoveOnXAxis;
        mustMoveOnPositiveDirection = false;

        //calculate new speed based on level
        speed = CalculateNewSpeed(level);

        //update pos, preparing for moving
        transform.localPosition = new Vector3(initialHorizontalPos.x, level, initialHorizontalPos.y);
        UpdateMovingPosition(MAX_AMPLITUDE);

        transform.localScale = new Vector3(initialHorizontalSize.x, 1, initialHorizontalSize.y);
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

        UpdateMovingPosition(axisPos + advance);

        //when the block has reached the max amplitude, inverse the direction
        axisPos = GetAxisPosition();
        if (axisPos <= -MAX_AMPLITUDE || axisPos >= MAX_AMPLITUDE) {
            mustMoveOnPositiveDirection = !mustMoveOnPositiveDirection;
        }
    }

    public void UpdateMovingPosition(float axisPos) {

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

    private float GetAxisPosition() {

        if (mustMoveOnXAxis) {
            return transform.localPosition.x;
        }

        return transform.localPosition.z;
    }

    private float GetAxisSize() {

        if (mustMoveOnXAxis) {
            return transform.localScale.x;
        }

        return transform.localScale.z;
    }

    private float GetShiftFromOtherBlock(BlockBehavior otherBlock) {

        if (mustMoveOnXAxis) {
            return otherBlock.transform.localPosition.x - transform.localPosition.x;
        }

        return otherBlock.transform.localPosition.z - transform.localPosition.z;
    }

    private float GetDistanceFromOtherBlock(BlockBehavior otherBlock) {

        return Mathf.Abs(GetShiftFromOtherBlock(otherBlock));
    }

    public bool IsStackedOutsidePreviousBlock(BlockBehavior otherBlock) {

        if (otherBlock == null) {
            throw new ArgumentException();
        }

        //distance betwen the 2 blocks must be greater than the size of the block
        return GetDistanceFromOtherBlock(otherBlock) > GetAxisSize();
    }

    public bool HasExactStackPosition(BlockBehavior otherBlock, float threshold) {

        if (otherBlock == null) {
            throw new ArgumentException();
        }

        //distance betwen the 2 blocks must be 0 or less than the threshold
        return GetDistanceFromOtherBlock(otherBlock) <= threshold;
    }

    ///update position to be exactly on the other block
    public void MoveOverOtherBlock(BlockBehavior otherBlock) {

        var otherPos = otherBlock.transform.localPosition;
        var newPos = transform.localPosition;

        if (mustMoveOnXAxis) {
            newPos.x = otherPos.x;
        } else {
            newPos.z = otherPos.z;
        }

        transform.localPosition = newPos;
    }

    ///resize the current block then generate a new additional block for the cut part
    public void SplitWithOtherBlock(BlockBehavior otherBlock, GameObject goCutPart) {

        if (otherBlock == null) {
            throw new ArgumentException();
        }

        var otherPos = otherBlock.transform.localPosition;
        var otherSize = otherBlock.transform.localScale;

        //get the shift between this block and the other, the shift is the non-absolute value of the distance
        var shift = GetShiftFromOtherBlock(otherBlock);

        //resize the current block
        var newPos = transform.localPosition;
        var newSize = transform.localScale;

        if (mustMoveOnXAxis) {
            newPos.x = otherPos.x - 0.5f * shift;
            newSize.x -= Math.Abs(shift);
        } else {
            newPos.z = otherPos.z - 0.5f * shift;
            newSize.z -= Math.Abs(shift);
        }

        transform.localPosition = newPos;
        transform.localScale = newSize;

        //resize and move the cut part
        var newCutPos = goCutPart.transform.localPosition;
        var newCutSize = goCutPart.transform.localScale;
        var multiplier = shift > 0 ? 1 : -1;

        if (mustMoveOnXAxis) {
            newCutSize.x = otherSize.x - newSize.x;
            newCutPos.x = otherPos.x - shift - multiplier * 0.5f * newSize.x;
        } else {
            newCutSize.z = otherSize.z - newSize.z;
            newCutPos.z = otherPos.z - shift - multiplier * 0.5f * newSize.z;
        }

        goCutPart.transform.localPosition = newCutPos;
        goCutPart.transform.localScale = newCutSize;
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
