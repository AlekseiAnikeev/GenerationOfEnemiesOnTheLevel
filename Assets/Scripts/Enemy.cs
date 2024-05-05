using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [FormerlySerializedAs("speed")] [SerializeField, Min(1f)] private float _speed = 1;

    private Vector3 _targetPosition;

    public void SetTargetPosition(Vector3 targetPosition) => _targetPosition = targetPosition;

    private void Update()
    {
        MoveTowards(_targetPosition);
    }

    private void MoveTowards(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);
    }
}
