using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public GameObject Chassis;
    public GameObject Optics;
    public GameObject Jets;

    public int ChassisWeight = 1;
    public int ChassisDropMin = 1;
    public int ChassisDropMax = 10;

    public int OpticsWeight = 1;
    public int OpticsDropMin = 1;
    public int OpticsDropMax = 10;

    public int JetsWeight = 1;
    public int JetsDropMin = 1;
    public int JetsDropMax = 10;

    private int totalWeight;
    private float dropYPos = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        calcTotalWeight();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void calcTotalWeight()
    {
        totalWeight = ChassisWeight + OpticsWeight + JetsWeight;
    }

    public void CreateLoot(Vector3 position)
    {
        float drop = Random.Range(0, totalWeight);

        if (drop - (float)ChassisWeight < 0)
        {
            //Chassis drop
            createLoot(position, Chassis, ChassisDropMin, ChassisDropMax);
        } else if (drop - (float)(ChassisWeight + OpticsWeight) < 0)
        {
            //Optics drop
            createLoot(position, Optics, OpticsDropMin, OpticsDropMax);
        } else
        {
            //Jets drop
            createLoot(position, Jets, JetsDropMin, JetsDropMax);
        }
    }

    private void createLoot(Vector3 position, GameObject obj, int min, int max)
    {
        int amount = (int)Mathf.Floor(Random.Range(min, max + 1));
        position.y = dropYPos;
        LootController con = Instantiate(obj, position, Quaternion.identity).GetComponent<LootController>();
        con.setResourceAmount(amount);
    }
}
