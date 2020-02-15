﻿using System;
using UnityEngine;
using Lean.Pool;
using DG.Tweening;


public class TowerBehavior : MonoBehaviour {


    ///the threshold that determine when a block is well stacked over the previous even if it's not perfectly stacked
    public static readonly float THRESHOLD_EXACT_BLOCK_STACKING = 0.3f;


    [SerializeField] private int level;
    [SerializeField] private BlockBehavior baseBlockBehavior;
    [SerializeField] private Transform trBlocks;
    [SerializeField] private LeanGameObjectPool poolBlocks;

    private BlockBehavior previousBlockBehavior;
    private BlockBehavior topBlockBehavior;

    public int Level {
        get {
            return level;
        }
    }


    void Start() {

        ResetTower();
    }

    public bool HasTopBlock() {
        return topBlockBehavior != null && topBlockBehavior != baseBlockBehavior;
    }

    public void IncrementLevel() {
        level++;
    }

    public void ResetTower() {

        level = 0;

        //cast all generated blocks to pool
        poolBlocks.DespawnAll();

        previousBlockBehavior = null;
        topBlockBehavior = baseBlockBehavior;

        //make the camera follow the new block
        Camera.main.transform.DOLocalMoveY(0, 0.2f);
    }

    public void GenerateNextBlock() {

        //replace the previous block by the current one
        previousBlockBehavior = topBlockBehavior;

        //add a new block to the stack
        var goBlock = poolBlocks.Spawn(Vector3.zero, Quaternion.identity, trBlocks, false);
        topBlockBehavior = goBlock.GetComponent<BlockBehavior>();

        //swap the moving axis of the new block and move
        var trPreviousBlock = previousBlockBehavior.transform;
        var previousPos = new Vector2(trPreviousBlock.localPosition.x, trPreviousBlock.localPosition.z);
        var previousSize = new Vector2(trPreviousBlock.localScale.x, trPreviousBlock.localScale.z);

        topBlockBehavior.Init(level, !previousBlockBehavior.MustMoveOnXAxis, previousPos, previousSize);
        topBlockBehavior.StartMoving();

        //make the camera follow the new block
        Camera.main.transform.DOLocalMoveY(level, 0.5f);
    }

    public bool StackCurrentBlock() {

        if (!HasTopBlock()) {
            throw new InvalidOperationException("Can't stack unexisting block");
        }

        topBlockBehavior.StopMoving();

        if (topBlockBehavior.IsStackedOutsidePreviousBlock(previousBlockBehavior)) {
            //not on the tower
            topBlockBehavior.SetAsKinematic(false);
            return false;
        }

        if (topBlockBehavior.HasExactStackPosition(previousBlockBehavior, THRESHOLD_EXACT_BLOCK_STACKING)) {

            //perfect or almost perfect
            topBlockBehavior.MoveOverOtherBlock(previousBlockBehavior);

        } else {

            //generate the rest of the cut block before resizing the current block to keep the same position and scale
            var goCutBlock = poolBlocks.Spawn(Vector3.zero, Quaternion.identity, trBlocks, false);
            //set scale before position to make the spawn work correctly
            goCutBlock.transform.localScale = topBlockBehavior.transform.localScale;
            goCutBlock.transform.localPosition = topBlockBehavior.transform.localPosition;

            //on the tower but not fitting perfectly, cut then let the new cut block falling down
            topBlockBehavior.SplitWithOtherBlock(previousBlockBehavior, goCutBlock);

            goCutBlock.GetComponent<BlockBehavior>().SetAsKinematic(false);
        }

        return true;
    }

}
