using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _tapForce;

    private Rigidbody2D _rigidbody;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector2.zero;
    }

    public void Move()
    {
        _rigidbody.velocity = new Vector2(_speed, _tapForce);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
}