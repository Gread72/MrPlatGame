using UnityEngine;
using System.Collections;

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
