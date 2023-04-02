using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    //we only use squares so this is fine
    public float objectLength = 3.0f;
    //public int objectWidth;
    public GameObject obstaclePrefab;

    private static int length = 15;
    private static int width = 15;


    private GameObject phantomObject;
    private float halfObjLen;
    private bool isPhantomShowing;
    private const bool TAKEN = false;
    private const bool FREE = true;
    //spot will be true if it is free, and false if it is taken
    private bool[,] availableGridSpots = new bool[2*length, 2*width];
    private GameObject[,] SolidObstacles = new GameObject[2*length, 2*width];

    private PlayerStatus player;

    private bool shouldPlaceTurret = false;
    private ObstacleStatus turretObstacle;

    //how many chassis an obstacle takes to build
    public const int ObstacleCost = 5;
    public const int TurretCost = 1;

    // Start is called before the first frame update
    void Start()
    {
        phantomObject = GameObject.Find("Phantom_Obstacle");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        halfObjLen = objectLength / 2.0f;
        setPhantomActive(false);
        initGridSpots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initGridSpots()
    {
        for(int i = 0; i < length * 2; i++)
        {
            for(int j = 0; j < width * 2; j++)
            {
                availableGridSpots[i, j] = FREE;
            }
        }
    }

    private int convertToArrayIndex(float toConvert, int shift)
    {
        return (int)(Mathf.Floor(toConvert / objectLength) + shift); //need to shift because we are not centered at (0,0)
    }

    private bool isSpotFree(Vector3 position)
    {
        int x = convertToArrayIndex(position.x, length);
        int z = convertToArrayIndex(position.z, width);
        if(x > (length * 2) || z > (width * 2) || x < 0 || z < 0)
        {
            return TAKEN;
        }

        return availableGridSpots[x, z];
    }

    private void setSpot(Vector3 position, bool val)
    {
        int x = convertToArrayIndex(position.x, length);
        int z = convertToArrayIndex(position.z, width);

        availableGridSpots[x, z] = val;
    }

    private void storeObstacle(GameObject toStore)
    {
        int x = convertToArrayIndex(toStore.transform.position.x, length);
        int z = convertToArrayIndex(toStore.transform.position.z, width);

        SolidObstacles[x, z] = toStore;

        setSpot(toStore.transform.position, TAKEN);
    }

    public void removeObstacle(Vector3 pos)
    {
        int x = convertToArrayIndex(pos.x, length);
        int z = convertToArrayIndex(pos.z, width);

        SolidObstacles[x, z] = null;

        setSpot(pos, FREE);
    }

    public void setPhantomActive(bool val)
    {
        isPhantomShowing = val;
        phantomObject.SetActive(val);
    }

    public void showTurretPhantom(ObstacleStatus obs)
    {
        shouldPlaceTurret = true;
        turretObstacle = obs;
    }

    public void hideTurretPhantom()
    {
        shouldPlaceTurret = false;
        turretObstacle = null;
    }

    public void showPhantom()
    {
        setPhantomActive(true);
    }

    public void hidePhantom()
    {
        setPhantomActive(false);
    }

    public void placeObstacle()
    {
        if (shouldPlaceTurret && turretObstacle != null)
        {
            if (player.spendResources(PlayerStatus.rTypes.Jets, PlayerStatus.rTypes.Optics, TurretCost) == true)
            {
                turretObstacle.ShowTurret();

            }
        } else if (isPhantomShowing)
        {
            placeObstacleAtPosition(phantomObject.transform.position);
        }
    }


    private void placeObstacleAtPosition(Vector3 position)
    {
        //check if the spot is free
        if (isSpotFree(position))
        {
            //need to check if player has enough chassis, and then subtract it
            if (player.spendResource(PlayerStatus.rTypes.Chassis, ObstacleCost) == true)
            {
                storeObstacle(Instantiate(obstaclePrefab, position, Quaternion.identity));
            }
        } else
        {
            //if spot is not free, not sure what happens here
        }
    }

    //show the grid containing the given coordinates
    public void setPhantomPosition(Vector3 position)
    {
        if (phantomObject != null)
        {
            Vector3 newPos = getGridCoord(position);
            if (isSpotFree(newPos) == FREE)
            {
                showPhantom();
                phantomObject.transform.position = newPos;
            } else
            {
                hidePhantom();
            }
        }
    }

    //returns the position vector for where the cube should be placed
    private Vector3 getGridCoord(Vector3 position)
    {
        return new Vector3(calculateGridCoord(position.x), halfObjLen, calculateGridCoord(position.z));
    }

    private float calculateGridCoord(float toTransform)
    {
        if (toTransform > 0)
        {
            return ((int)(toTransform / objectLength)) * objectLength + halfObjLen;
        } else
        {
            return ((int)(toTransform / objectLength)) * objectLength - halfObjLen;
        }
    }
}
