using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class RoadGrid : MonoBehaviour
{
    public int gridLength = 9, gridWidth = 9;
    public int spacingLength = 2, spacingWidth = 2;
    public GameObject roads;
    int ranNum;

    //All GameObjects in the grid
    public GameObject RoadStraight1, RoadCorner1, Intersection1, Intersection1ThreeWay, Tower1, Tower2, Tower3, Tower4, Tower6, Grass;
    public List<GameObject> models;

    public Vector3 defaultPos = new Vector3(0f, 0f, 0f);
    public Quaternion defaultRot = new Quaternion(0, 0, 0, 0);

    public void Start()
    {
        GenerateGrid();
    }

    [ContextMenu("Generate Grid")]
    public void GenerateGrid()
    {

        GameObject CityGrid;
        CityGrid = new GameObject();
        CityGrid.name = "CityGrid";
        CityGrid.transform.position = defaultPos;
        CityGrid.transform.rotation = defaultRot;
        //Instantiate(CityGrid, defaultPos, Quaternion.identity);
        CityGrid.transform.parent = roads.transform;
        


        GameObject[,] worldGrid;
        worldGrid = new GameObject[gridLength, gridWidth]; //City Grid [x,z] [length,width]




        //Place corners
        placeTile(0, 0, new Vector3(0f, 0f, 0f), 0, RoadCorner1);
        placeTile(0, (gridWidth - 1), new Vector3(0f, 0f, (10f * (float)(gridWidth - 1f))), 90, RoadCorner1);
        placeTile((gridLength - 1), 0, new Vector3((10f * (float)(gridLength - 1f)), 0f, 0f), 270, RoadCorner1);
        placeTile((gridLength - 1), (gridWidth - 1), new Vector3((10f * (float)(gridLength - 1f)), 0f, (10f * (float)(gridWidth - 1f))), 180, RoadCorner1);

        void placeTile(int a, int b, Vector3 pos, int rot, GameObject tile)
        {
            worldGrid[a, b] = tile;
            //Instantiate(worldGrid[a, b], (defaultPos + pos), Quaternion.Euler(0, rot, 0));
            worldGrid[a, b] = Instantiate(worldGrid[a, b], (defaultPos + pos), Quaternion.Euler(0, rot, 0));
            worldGrid[a, b].transform.parent = CityGrid.transform;
        }
        for (int a = 0; a < gridWidth; a++) //Placing all tiles of city
        {
            for (int b = 0; b < gridLength; b++)
            {
                //Next 4 if statements create the edges of the grid

                if (a == 0)
                {
                    if (b % spacingLength == 0 && (!worldGrid[a, b]))
                    {
                        placeTile(a, b, new Vector3(((float)(10 * b)), 0f, 0f), 270, Intersection1ThreeWay);
                    }
                    else if (b % spacingLength != 0 && (!worldGrid[a, b]))
                    {
                        placeTile(a, b, new Vector3(((float)(10 * b)), 0f, 0f), 270, models[0]);
                    }
                }
                if (a == gridWidth - 1)
                {
                    if (b % spacingLength == 0 && (!worldGrid[a, b]))
                    {
                        placeTile(a, b, new Vector3(((float)(10 * b)), 0f, ((float)(10 * a))), 90, Intersection1ThreeWay);
                    }
                    else if (b % spacingLength != 0 && (!worldGrid[a, b]))
                    {
                        placeTile(a, b, new Vector3(((float)(10 * b)), 0f, ((float)(10 * a))), 90, RoadStraight1);
                    }
                }
                if (b == 0)
                {
                    if (a % spacingWidth == 0 && (!worldGrid[a, b]))
                    {
                        placeTile(a, b, new Vector3(((float)(10 * b)), 0f, ((float)(10 * a))), 0, Intersection1ThreeWay);
                    }
                    else if (a % spacingWidth != 0 && (!worldGrid[a, b]))
                    {
                        placeTile(a, b, new Vector3(((float)(10 * b)), 0f, ((float)(10 * a))), 0, RoadStraight1);
                    }
                }
                if (b == gridLength - 1)
                {
                    if (a % spacingWidth == 0 && (!worldGrid[a, b]))
                    {
                        placeTile(a, b, new Vector3(((float)(10 * b)), 0f, ((float)(10 * a))), 180, Intersection1ThreeWay);
                    }
                    else if (a % spacingWidth != 0 && (!worldGrid[a, b]))
                    {
                        placeTile(a, b, new Vector3(((float)(10 * b)), 0f, ((float)(10 * a))), 180, RoadStraight1);
                    }
                } //Edges done
                if (((a % spacingWidth == 0) && (b % spacingLength == 0)) && (!worldGrid[a, b])) //Places all 4-way intersections
                {
                    placeTile(a, b, new Vector3(((float)(10 * b)), 0f, ((float)(10 * a))), 0, Intersection1);
                }
                if ((a % spacingWidth == 0) && (!worldGrid[a, b])) //places roads through city
                {
                    placeTile(a, b, new Vector3(((float)(10 * b)), 0f, ((float)(10 * a))), 90, RoadStraight1);
                }
                if ((b % spacingLength == 0) && (!worldGrid[a, b])) //roads through other axis
                {
                    placeTile(a, b, new Vector3(((float)(10 * b)), 0f, ((float)(10 * a))), 0, RoadStraight1);
                }
                if (!worldGrid[a, b]) //fills in any empty spaces with either grass or a building depending on RNG
                {
                    ranNum = Random.Range(0, 6);
                    switch (ranNum)
                    {
                        case (0):
                            worldGrid[a, b] = Tower1;
                            break;
                        case (1):
                            worldGrid[a, b] = Tower2;
                            break;
                        case (2):
                            worldGrid[a, b] = Tower3;
                            break;
                        case (3):
                            worldGrid[a, b] = Tower4;
                            break;
                        case (4):
                            worldGrid[a, b] = Tower6;
                            break;
                        default:
                            worldGrid[a, b] = Grass;
                            break;
                    }
                    ranNum = Random.Range(0, 4);
                    worldGrid[a, b] = Instantiate(worldGrid[a, b], defaultPos + new Vector3(((float)(10 * b)), 0f, ((float)(10 * a))), Quaternion.Euler(0, (90 * ranNum), 0));
                    worldGrid[a, b].transform.parent = CityGrid.transform;
                }

                //double for loop space

                worldGrid[a, b].AddComponent<MeshCollider>();
                
                //GameObject obj = Resources.Load<GameObject>("Assets/Resources/Models/Roads/RoadCorner1");
                //Mesh mesh = obj.GetComponent<Mesh>();
                //worldGrid[a, b].GetComponent<MeshCollider>().sharedMesh = obj.GetComponent<MeshCollider>().sharedMesh;




            }
        }



    }
}

