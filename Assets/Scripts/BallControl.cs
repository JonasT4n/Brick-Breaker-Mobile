using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField] private BallManager ballManager;
    public float ballSpeed = 5.0f;
    public Vector2 constantVel = new Vector2();
    private Rigidbody2D ballRigid = new Rigidbody2D();
    private float ExtraForceOnYAxis = 5.0f;
    public bool isBrick = false;
    private int wallHit = 0;

    public float Angle { set; get; }

    void Update()
    {
        if (wallHit >= 3)
        {
            wallHit = 0;
            if (Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) <= 1 && !isBrick)
            {
                Vector2 newVel = new Vector2(0, -ExtraForceOnYAxis);
                gameObject.GetComponent<Rigidbody2D>().AddForce(newVel, ForceMode2D.Impulse);
            }
        }
        if (Phythagoras(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y) < ballSpeed - 1 && !isBrick)
        {
            Angle = Mathf.Atan(ballRigid.velocity.y / ballRigid.velocity.x);
            constantVel = VectorFromAngle(Angle) * ballSpeed;
            ballRigid.velocity = constantVel;
        }
        if (Phythagoras(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y) > ballSpeed + 1 && !isBrick)
        {
            Angle = Mathf.Atan(ballRigid.velocity.y / ballRigid.velocity.x);
            constantVel = VectorFromAngle(Angle) * ballSpeed;
            ballRigid.velocity = constantVel;
        }
    }

    public void PushBall()
    {
        ballRigid = gameObject.GetComponent<Rigidbody2D>();
        constantVel = VectorFromAngle(Angle) * ballSpeed;
        ballRigid.AddForce(constantVel, ForceMode2D.Impulse);
    }

    private Vector2 VectorFromAngle(float theta)
    {
        float xForce = Mathf.Cos(theta);
        float yForce = Mathf.Abs(Mathf.Sin(theta));
        if (theta < 0)
        {
            return new Vector2(-xForce, yForce);
        }
        else
        {
            return new Vector2(xForce, yForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.name == "Void")
        {
            if (ballManager.CheckContainer <= 3)
            {
                ballManager.isBrickSpawn = true;
            }
            GameObject.Find("GameManager").GetComponent<GameManager>().ballCount++;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball" && isBrick)
        {
            isBrick = false;
            gameObject.layer = 8;
            transform.SetParent(GameObject.Find("BallContainer").transform);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            this.constantVel = collision.gameObject.GetComponent<BallControl>().constantVel;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = -constantVel;
            PushBall();
        }
        wallHit++;
    }

    private float Phythagoras(float x, float y)
    {
        float hypotenuse = Mathf.Sqrt((x * x) + (y * y));
        return hypotenuse;
    }
}
