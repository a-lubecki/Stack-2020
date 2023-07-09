using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private BlockBehavior[] _platforms = null;

    public void BreakAllPlatforms()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
            //FindObjectOfType<BlockBehavior>().IncreaseScore();
        }

        foreach (BlockBehavior p in _platforms)
        {
            p.BreakingPlatforms();
        }

        StartCoroutine(RemoveParts());
    }

    IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}