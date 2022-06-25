using UnityEngine;

public class FloatingCrystalMovement : MonoBehaviour
{
    [SerializeField] Vector3 startPoint;
    [SerializeField] Vector3 endPoint;
    [SerializeField] float movementSpeed;
    private Vector3 nextPosition;


    void Start()
    {
        nextPosition = endPoint;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, movementSpeed * Time.deltaTime);
        
        if(Vector3.Distance(transform.position, nextPosition) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nextPosition = nextPosition != startPoint ? startPoint : endPoint;
    }
}
