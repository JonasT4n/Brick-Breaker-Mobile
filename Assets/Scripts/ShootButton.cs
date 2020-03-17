using UnityEngine;

public class ShootButton : MonoBehaviour
{
    public BallManager ballContainer;
    public BrickSpawn brickContainer;
    public LineHint lineHint;
    public bool allBallDestroyed = true;

    void Update()
    {
        if (ballContainer.CheckContainer <= 0)
        {
            allBallDestroyed = true;
        }
        if (ballContainer.isBrickSpawn && ballContainer.CheckContainer <= 0)
        {
            ballContainer.isBrickSpawn = false;
            brickContainer.SpawnBricks();
            GameObject.Find("GameManager").GetComponent<GameManager>().round++;
            GameObject.Find("GameManager").GetComponent<GameManager>().ballCount++;
        }
    }

    // Button Shoot Pressed
    public void OnClickShoot()
    {
        if (allBallDestroyed)
        {
            Vector3 mousePos = lineHint.GetMousePosition();
            ballContainer.ReleaseBalls(mousePos);
            allBallDestroyed = false;
        }
    }
}
