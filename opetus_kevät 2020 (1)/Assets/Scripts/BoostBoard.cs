using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBoard : MonoBehaviour
{
    public Vector2 direction = Vector2.up;
    public float boostAmount = 400f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            Player.Instance.AddForce(direction * boostAmount);
        }
    }
}
