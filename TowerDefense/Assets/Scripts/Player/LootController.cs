using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public PlayerStatus.rTypes type;
    public int resourceAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setResourceAmount(int n)
    {
        resourceAmount = n;
    }

    void OnTriggerEnter(Collider col)
    {
        PlayerStatus player = col.gameObject.GetComponent<PlayerStatus>();

        if (player != null)
        {
            // add drop amount to the players resources
            player.pickupDrop(type, resourceAmount);
            Destroy(gameObject);
        }
    }
}
