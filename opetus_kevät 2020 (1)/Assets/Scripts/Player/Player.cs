using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    PlayerMovement movement;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetPlayerPosition()
    {
        return gameObject.transform.position;
    }

    public void AddForce(Vector2 force)
    {
        movement.AddForce(force);
    }
}
