using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;
    public LineRenderer lr;

    [Header("Customizable Values")]
    public float timeForNextRay = 0.05f;
    public float moveSpeed = 5f;  // Speed of the player movement
    public float waypointThreshold = 0.1f;  // Distance to consider the waypoint reached

    //Not customizable:
    private float timer = 0;
    private int currentWayPoint = 0;
    private int wayIndex;
    private bool move;
    private bool touchStartedOnPlayer;
    public List<GameObject> wayPoints;

    [Header("Prefabs")]
    public GameObject wayPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr.enabled = false;
        wayIndex = 1;
        move = false;
        touchStartedOnPlayer = false;
    }

    public void OnMouseDown()
    {
        lr.enabled = true;
        touchStartedOnPlayer = true;
        lr.positionCount = 1;
        lr.SetPosition(0, transform.position);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && timer > timeForNextRay && touchStartedOnPlayer)
        {
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
            Vector3 direction = worldMousePos - Camera.main.transform.position;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
            {
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.green, 1f);
                GameObject newWaypoint = Instantiate(wayPoint, new Vector3(hit.point.x, transform.position.y, hit.point.z), Quaternion.identity);
                wayPoints.Add(newWaypoint);
                lr.positionCount = wayIndex + 1;
                lr.SetPosition(wayIndex, newWaypoint.transform.position);
                timer = 0;
                wayIndex++;
            }
        }

        timer += Time.deltaTime;

        if (Input.GetMouseButtonUp(0))
        {
            touchStartedOnPlayer = false;
            move = true;
        }
    }

    void FixedUpdate()
    {
        if (move)
        {
            // Move towards the next waypoint
            if (wayPoints.Count > 0)
            {
                Vector3 targetPosition = wayPoints[currentWayPoint].transform.position;
                Vector3 moveDirection = (targetPosition - rb.position).normalized;
                Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

                rb.MovePosition(newPosition);

                // Rotate the player to face the waypoint
                transform.LookAt(targetPosition);

                // Check if the player has reached the waypoint
                if (Vector3.Distance(rb.position, targetPosition) < waypointThreshold)
                {
                    currentWayPoint++;

                    // Check if all waypoints are visited
                    if (currentWayPoint == wayPoints.Count)
                    {
                        move = false;

                        // Destroy all waypoints and clear the list
                        foreach (var item in wayPoints) Destroy(item);
                        wayPoints.Clear();
                        wayIndex = 1;
                        currentWayPoint = 0;
                    }
                }
            }
        }
    }
}
