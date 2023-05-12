using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxEffect;
    [SerializeField] private float parallaxVerticalEffect;
    [SerializeField] private bool useYEffect = false;
    private Vector3 _startPosition;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        var xDistance = _cam.transform.position.x * parallaxEffect;
        var yDistance = _cam.transform.position.y * parallaxEffect;

        if (!useYEffect)
        {
            transform.position = new Vector3(_startPosition.x + xDistance, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(_startPosition.x + xDistance, _startPosition.y + yDistance, _startPosition.z + _cam.transform.position.z);
        }
    }
}
