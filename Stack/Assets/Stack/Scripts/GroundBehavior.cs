using System;
using UnityEngine;
using Lean.Pool;


public class GroundBehavior : MonoBehaviour {


    void OnTriggerEnter(Collider other) {

        if (other.GetComponent<BlockBehavior>() == null) {
            //not a block
            return;
        }

        //cast fallen block to pool
        LeanPool.Despawn(other);
    }

}
