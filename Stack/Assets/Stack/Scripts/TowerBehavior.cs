using System;
using UnityEngine;
using Lean.Pool;


public class TowerBehavior : MonoBehaviour {


    ///the threshold that determine when a block is well stacked over the previous even if it's not perfectly stacked
    public static readonly float THRESHOLD_EXACT_BLOCK_STACKING = 0.3f;


    [SerializeField] private int level;
    [SerializeField] private BlockBehavior baseBlockBehavior;
    [SerializeField] private Transform trBlocks;
    [SerializeField] private GameObject prefabBlock;

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
        while (trBlocks.childCount > 0) {
            LeanPool.Despawn(trBlocks.GetChild(0));
        }

        previousBlockBehavior = null;
        topBlockBehavior = baseBlockBehavior;
    }

    public void GenerateNextBlock() {

        //replace the previous block by the current one
        previousBlockBehavior = topBlockBehavior;

        //add a new block to the stack
        var goBlock = LeanPool.Spawn(prefabBlock, trBlocks);
        topBlockBehavior = goBlock.GetComponent<BlockBehavior>();

        //swap the moving axis of the new block and move
        var trPreviousBlock = previousBlockBehavior.transform;
        var previousPos = new Vector2(trPreviousBlock.localPosition.x, trPreviousBlock.localPosition.z);
        var previousSize = new Vector2(trPreviousBlock.localScale.x, trPreviousBlock.localScale.z);

        topBlockBehavior.Init(level, !previousBlockBehavior.MustMoveOnXAxis, previousPos, previousSize);
        topBlockBehavior.StartMoving();
    }

    public bool StackCurrentBlock() {

        if (!HasTopBlock()) {
            throw new InvalidOperationException("Can't stack unexisting block");
        }

        topBlockBehavior.StopMoving();

        if (topBlockBehavior.IsStackedOutsidePreviousBlock(previousBlockBehavior)) {
            //not on the tower
            topBlockBehavior.Fall();
            return false;
        }

        if (topBlockBehavior.HasExactStackPosition(previousBlockBehavior, THRESHOLD_EXACT_BLOCK_STACKING)) {

            //perfect or almost perfect
            topBlockBehavior.UpdateMovingPosition(0);

        } else {

            //on the tower but not fitting perfectly, ut then let the new cut block falling down
            var cutBlock = topBlockBehavior.SplitWithOtherBlock(previousBlockBehavior);
            ///cutBlock.Fall();
        }

        return true;
    }

}

/*
public interface ITowerBehaviorListener {

    void OnTowerReset();

    void OnStackGeneration();

    void OnStackCurrent();

}
*/