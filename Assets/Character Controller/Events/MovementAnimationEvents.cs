using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimationEvents:MonoBehaviour
{
    [SerializeField] GameObject slideDustPrefab;
    [SerializeField] Transform slideDustSpawn;

    public void SlideEffects()
    {
        Instantiate(slideDustPrefab, slideDustSpawn.position, slideDustSpawn.rotation);
    }
}
