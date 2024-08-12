using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float moveSpeed = 5f;
    public float pointDistanceThreshold = 0.1f;
    public float groundLevelY = 0f;  // Adjust this to match your ground level

    private List<Vector3> points = new List<Vector3>();
    private int currentPointIndex = 0;
    private bool isDrawing = false;
    private bool isMoving = false;

    public Animator anim;

    void Start()
    {
        anim.SetBool("isRunning",true);
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        anim.SetBool("isRunning", true);
        Vector3 position = transform.position;
        position.y = 0.02f;
        transform.position = position;

        HandleDrawing();

        if (isMoving)
        {
            MoveAlongPath();
            anim.SetBool("isRunning", true);  // Trigger running animation
            Debug.Log("Running animation triggered");
        }
        else
        {
            anim.SetBool("isRunning", false); // Trigger idle animation
        }
    }

    void HandleDrawing()
    {   
        if (Input.GetMouseButtonDown(0)) // Start drawing on left click
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            if (Vector3.Distance(mousePosition, transform.position) < 0.5f) // Ensure the drawing starts near the character
            {
                isDrawing = true;
                points.Clear();
                lineRenderer.positionCount = 0;
                AddPoint(ClampToGroundLevel(mousePosition));
            }
        }

        if (isDrawing)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            if (Vector3.Distance(mousePosition, points[points.Count - 1]) > pointDistanceThreshold)
            {
                AddPoint(ClampToGroundLevel(mousePosition));
            }

            if (Input.GetMouseButtonUp(0)) // Stop drawing on mouse release
            {
                isDrawing = false;
                isMoving = true;
                currentPointIndex = 0;
            }
        }
    }

    void AddPoint(Vector3 point)
    {
        points.Add(point);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);
    }

    void MoveAlongPath()
    {
        if (currentPointIndex >= points.Count)
        {
            isMoving = false;
            return;
        }

        Vector3 targetPosition = points[currentPointIndex];
        Vector3 direction = targetPosition - transform.position;
        
        // Rotate the player to face the direction of the movement, but only on the Y axis
        if (direction != Vector3.zero) 
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < pointDistanceThreshold)
        {
            // Move to the next point
            currentPointIndex++;
            
            // Remove the point behind the player (the previous point)
            if (currentPointIndex > 1)
            {
                points.RemoveAt(0); // Remove the first point in the list
                lineRenderer.positionCount--;
                for (int i = 0; i < lineRenderer.positionCount; i++)
                {
                    lineRenderer.SetPosition(i, points[i]);
                }
            }
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // Set z to the character's z position
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    Vector3 ClampToGroundLevel(Vector3 position)
    {
        position.y = Mathf.Max(position.y, groundLevelY);
        return position;
    }

    void LateUpdate()
    {
        Camera.main.transform.LookAt(transform.position);
    }
}
