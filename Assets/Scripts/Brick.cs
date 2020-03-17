using UnityEngine;
using TMPro;

public class Brick : MonoBehaviour
{
    public int RANGE_HEALTH = 5;
    public int health;
    private System.Random rnd = new System.Random();
    TextMeshPro HealthCount;

    // Start is called before the first frame update
    public void InitBrick()
    {
        HealthCount = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        if (HealthCount == null)
        {
            Debug.Log("Missing Component.");
        }
        HealthCount.SetText(health.ToString());
    }

    // When a Brick being hit by Ball
    private void OnCollisionEnter2D(Collision2D collider)
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (collider.gameObject.tag == "Ball")
        {
            health--; manager.ScoreInt++;
            HealthCount.SetText(health.ToString());
            if (health <= 0) Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Lose Range")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOverScreen.SetActive(true);
        }
    }
}
