using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddCollectableOnList(gameObject);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Lul");
            GameManager.Instance.RemoveCollatableOnList(gameObject);
            Destroy(gameObject);
        }
    }
}
