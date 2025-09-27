using System.Collections;
using UnityEngine;

public abstract class MovableController : MonoBehaviour, IMoveable
{
    public virtual void Move(Vector3 destinationPosition, float duration)
    {
        StartCoroutine(MoveTowardsDestination(destinationPosition, duration));
    }
    private IEnumerator MoveTowardsDestination(Vector3 destination, float duration)
    {
        float distance = Vector3.Distance(transform.position, destination);
        float speed = distance / duration;

        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination;
    }
}
