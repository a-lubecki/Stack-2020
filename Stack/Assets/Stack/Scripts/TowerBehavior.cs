using System;
using UnityEngine;
using Lean.Pool;


public class TowerBehavior : MonoBehaviour {


    [SerializeField] private int level;
    [SerializeField] private BlockBehavior baseBlockBehavior;
    [SerializeField] private Transform trBlocks;
    [SerializeField] private GameObject prefabBlock;
    [SerializeField] private BlockBehavior topBlockBehavior;


    public int Level {
        get {
            return level;
        }
    }

    public bool HasTopBlock() {
        return topBlockBehavior != null;
    }

    public void IncrementLevel() {
        level++;
    }

    public void ResetTower() {

        level = 0;

        //add all generted blocks to pool
        foreach (Transform tr in trBlocks) {
            LeanPool.Despawn(tr, 0);
        }
    }

    public void GenerateNextBlock() {

        //add a new block to the stack
        var goBlock = LeanPool.Spawn(prefabBlock, trBlocks);
        topBlockBehavior = goBlock.GetComponent<BlockBehavior>();

        var previousBlockBehavior = FindPreviousBlock();

        //swap the moving axis
        var mustMoveOnXAxis = true;
        if (previousBlockBehavior != null) {
            mustMoveOnXAxis = !previousBlockBehavior.MustMoveOnXAxis;
        }

        //init and move
        topBlockBehavior.Init(level, mustMoveOnXAxis);
        topBlockBehavior.StartMoving();
    }

    public bool StackCurrentBlock() {

        if (!HasTopBlock()) {
            throw new InvalidOperationException("Can't stack unexisting block");
        }

        topBlockBehavior.StopMoving();

        if (IsStackedOutsidePreviousBlock()) {
            //not on the tower
            topBlockBehavior.Fall();
            return false;
        }

        if (HasExactStackPosition()) {
            //perfect
            return true;
        }

        //on the tower
        var blockBehavior = GenerateCutBlock();
        blockBehavior.Fall();

        return true;///TODO TEST
    }

    private bool IsStackedOutsidePreviousBlock() {

        ///TODO
        return false;
    }

    private bool HasExactStackPosition() {

        ///TODO
        return true;
    }

    private BlockBehavior GenerateCutBlock() {

        ///TODO
        throw new NotSupportedException("TODO");
    }

    private BlockBehavior FindPreviousBlock() {

        if (trBlocks.childCount <= 1) {
            //tower must have at least 2 blocks to have a previous block
            return baseBlockBehavior;
        }

        return trBlocks.GetChild(topBlockBehavior.transform.GetSiblingIndex() - 1).GetComponent<BlockBehavior>();
    }

}

/*
public interface ITowerBehaviorListener {

    void OnTowerReset();

    void OnStackGeneration();

    void OnStackCurrent();

}
*/