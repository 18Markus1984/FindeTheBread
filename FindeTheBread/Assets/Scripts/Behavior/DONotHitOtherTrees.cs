using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DONotHitOtherTrees : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)           //if a collision stay 
    {
        if (collision.collider.tag == "Tree" || collision.collider.tag == "TilemapBottom")  //if colider tag is "Tree" or "TilemapBottom"
        {
            Vector2Int position = FindObjectOfType<LevelCreater>().FindeNewPositionfortheTree();        //finde a new position without hitting a tree
            transform.position = new Vector3Int(position.x,position.y,0);                           //set the tree position to the new postion
        }
        else
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);          //ignore every other collision
        }
    }
}
