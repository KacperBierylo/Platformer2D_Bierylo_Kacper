using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if(!LevelGenerator.instance.shouldFinish)
                LevelGenerator.instance.AddPiece();
            LevelGenerator.instance.RemoveOldestPiece();
        }
    }
}
