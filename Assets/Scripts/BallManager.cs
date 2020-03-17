using System.Collections;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject ballSample;
    private Vector3 BallDirection = new Vector3();
    public bool isBrickSpawn = false;

    public int CheckContainer { get { return transform.childCount; } }

    // Releasing all the Balls inside the Container
    public void ReleaseBalls(Vector3 mousePos)
    {
        BallDirection = mousePos;
        StartCoroutine(OneByOneShoot());
    }

    IEnumerator OneByOneShoot()
    {
        int HowMany = GameObject.Find("GameManager").GetComponent<GameManager>().ballCount;
        for (int eachBall = 0; eachBall < HowMany; eachBall++)
        {
            GameObject newBall = Instantiate(ballSample, ballSample.transform.position, Quaternion.identity);
            GameObject.Find("GameManager").GetComponent<GameManager>().ballCount--;
            newBall.SetActive(true);
            newBall.transform.parent = gameObject.transform;
            BallControl control = newBall.GetComponent<BallControl>();
            control.Angle = Mathf.Atan(BallDirection.y / BallDirection.x);
            control.PushBall();
            yield return new WaitForSeconds(0.1f); // Time Wait until Next Ball will be Shoot
        }
    }
}
