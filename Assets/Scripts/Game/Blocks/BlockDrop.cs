using UnityEngine;
using System.Collections;

/*
 * BlockDrop Class - Handler/Mediator for Block that will be dropped
 *
 */

public class BlockDrop : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other) //Hero jumps on enemy
    {
        if(other.gameObject.CompareTag("Hero"))
        {
            StartCoroutine(DropTheBlock());
        }
    }

    IEnumerator DropTheBlock()
    {
        yield return null;
        yield return null; 
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
