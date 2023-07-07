using System;
using UnityEngine;
using Lean.Pool;


public class TowerBehavior2 : MonoBehaviour {


    ///the threshold that determine when a block is well stacked over the previous even if it's not perfectly stacked
    public static readonly float THRESHOLD_EXACT_BLOCK_STACKING = 0.6f;


    [SerializeField] private BlockBehavior2 baseBlockBehavior;
    [SerializeField] private Transform trBlocks;
    [SerializeField] private LeanGameObjectPool poolBlocks;

    private BlockBehavior previousBlockBehavior;
    private BlockBehavior topBlockBehavior;


    public int level { get; private set; }
    public bool hasPerfectStackPosition { get; private set; }


    private ColorIncrementManager colorIncrementManager = new ColorIncrementManager();


    void Start() {

        
    }
    void Update()
    {
        baseBlockBehavior.transform.Rotate(Vector3.up, 1f, Space.World);
    }

    public bool HasTopBlock() {
        return topBlockBehavior != null && topBlockBehavior != baseBlockBehavior;
    }

    public void IncrementLevel() {
        level++;
    }


    public void GenerateNextBlock() {

        hasPerfectStackPosition = false;

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
        topBlockBehavior.Color = colorIncrementManager.NewColorFromOther(previousBlockBehavior.Color, 0.03f);
        topBlockBehavior.StartMoving();
        baseBlockBehavior.transform.Rotate(Vector3.up, 10f, Space.World);

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

            hasPerfectStackPosition = true;

        } else {

            //generate the rest of the cut block before resizing the current block to keep the same position and scale
            var goCutBlock = poolBlocks.Spawn(Vector3.zero, Quaternion.identity, trBlocks, false);
            //set scale before position to make the spawn work correctly
            goCutBlock.transform.localScale = topBlockBehavior.transform.localScale;
            goCutBlock.transform.localPosition = topBlockBehavior.transform.localPosition;

            //on the tower but not fitting perfectly, cut then let the new cut block falling down
            topBlockBehavior.SplitWithOtherBlock(previousBlockBehavior, goCutBlock);

            var cutBlockBehavior = goCutBlock.GetComponent<BlockBehavior>();
            cutBlockBehavior.Color = topBlockBehavior.Color;
            cutBlockBehavior.SetAsKinematic(false);
        }

        return true;
    }

    public bool GrowTopBlock() {

        if (!HasTopBlock()) {
            throw new InvalidOperationException("Can't grow unexisting block");
        }

        var basePos = baseBlockBehavior.transform.localPosition;
        var baseSize = baseBlockBehavior.transform.localScale;

        return topBlockBehavior.Grow(
            UnityEngine.Random.Range(0.3f, 2),
            new Vector2(basePos.x, basePos.z),
            new Vector2(baseSize.x, baseSize.z)
        );
    }

}
