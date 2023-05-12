using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxEffect;
    private float _startPosition;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Start()
    {
        _startPosition = transform.position.x;
    }

    private void Update()
    {
        var xDistance = _cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(_startPosition + xDistance, transform.position.y, transform.position.z);
    }
}
