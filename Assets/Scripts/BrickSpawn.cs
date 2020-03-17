using System.Collections.Generic;
using UnityEngine;

public class BrickSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> brickShapes = new List<GameObject>();
    public GameObject textMesh;
    public Vector3 ScreenTopLeft;
    public int ManyBricks = 8;
    int InitHealth;
    private int[] AnglesInit = {0, 90, 180, 270};
    public float distanceEachBrick = 1.0f;
    private System.Random rnd = new System.Random();

    void Start()
    {
        SpawnBricks();
    }

    public void SpawnBricks()
    {
        int thisRound = GameObject.Find("GameManager").GetComponent<GameManager>().round + 1;
        transform.position += -Vector3.up;
        for (int brickIndex = 0; brickIndex < ManyBricks; brickIndex++)
        {
            int getSpawnIndex = rnd.Next(0, brickShapes.Count + 1) - 1;
            int rotarySpawnAngle = rnd.Next(0, AnglesInit.Length);
            rotarySpawnAngle = AnglesInit[rotarySpawnAngle];
            Color32 randomColorPicker = Random.ColorHSV();
            if (getSpawnIndex < 0) continue;
            else
            {
                GameObject newBrick = Instantiate(brickShapes[getSpawnIndex], ScreenTopLeft + new Vector3(distanceEachBrick * brickIndex, 0, 0), Quaternion.identity);
                newBrick.transform.SetParent(gameObject.transform);
                newBrick.GetComponent<SpriteRenderer>().color = randomColorPicker;
                if (newBrick.tag == "Ball")
                {
                    newBrick.SetActive(true);
                    newBrick.GetComponent<BallControl>().isBrick = true;
                    newBrick.layer = 0;
                }
                else
                {
                    GameObject newText = Instantiate(textMesh, newBrick.transform.position, Quaternion.identity);
                    newBrick.transform.Rotate(new Vector3(0, 0, rotarySpawnAngle));
                    newText.transform.SetParent(newBrick.transform);
                    InitHealth = rnd.Next(thisRound, thisRound * rnd.Next(1, newBrick.GetComponent<Brick>().RANGE_HEALTH));
                    newBrick.GetComponent<Brick>().health = InitHealth;
                    newBrick.GetComponent<Brick>().InitBrick();
                }
            }
        }
    }
}
