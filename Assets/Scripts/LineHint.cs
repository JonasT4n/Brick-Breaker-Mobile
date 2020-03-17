using UnityEngine;

public class LineHint : MonoBehaviour
{
    [SerializeField] private LineRenderer lineTrajectory;
    [SerializeField] private Transform ballShootPoint;
    private CircleCollider2D ballPointCollider;
    private Vector3 mousePos = new Vector3();
    private Vector2 OffsetPoint = new Vector2();
    public Vector3 GetMousePosition() {return mousePos;}
    
    // Start is called before the first frame update
    void Start()
    {
        lineTrajectory.gameObject.SetActive(true);
        Ray mPosFix = Camera.main.ScreenPointToRay(Input.mousePosition); // From Screen to World Position ray
        mousePos = Vector3.up; // Set Offset to get more accurate
        ballPointCollider = ballShootPoint.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray mPosFix = Camera.main.ScreenPointToRay(Input.mousePosition); // From Screen to World Position ray
            float xMouse = mPosFix.origin.x - transform.position.x;
            float yMouse = mPosFix.origin.y - transform.position.y;
            if (yMouse < 3)
            {
                yMouse = 3;
            }
            mousePos = new Vector3(xMouse, yMouse, 0); // Set Offset to get more accurate
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Ray mPosFix = Camera.main.ScreenPointToRay(touch.position); // From Screen to World Position ray
                float xMouse = mPosFix.origin.x - transform.position.x;
                float yMouse = mPosFix.origin.y - transform.position.y;
                if (yMouse < 3)
                {
                    yMouse = 3;
                }
                mousePos = new Vector3(xMouse, yMouse, 0); // Set Offset to get more accurate
            }
        }

        RaycastHit2D[] RayHit = Physics2D.CircleCastAll(ballShootPoint.position, ballPointCollider.radius, mousePos);

        foreach(RaycastHit2D ray in RayHit)
        {
            Vector2 hitPoint = ray.point;
            Vector2 hitNormal = ray.normal;
            OffsetPoint = hitPoint + hitNormal * ballPointCollider.radius;
            if (ray.collider != null && ballShootPoint.GetComponent<BallControl>() == null)
            {
                lineTrajectory.SetPosition(0, ballShootPoint.position);
                lineTrajectory.SetPosition(1, ray.point);
            }
            else
            {
                lineTrajectory.SetPosition(0, ballShootPoint.position);
                lineTrajectory.SetPosition(1, ballShootPoint.position * 10);
            }
        }
    }
}
