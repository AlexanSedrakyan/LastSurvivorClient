using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    public Transform playerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool lookAtPlayer = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetPlayerGameObject(GameObject _player)
    {
        player = _player;
        //_cameraOffset = transform.position - player.transform.position;
        _cameraOffset = transform.position - new Vector3(7f, 0f, -7f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPos = player.transform.position + _cameraOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

            if (lookAtPlayer)
            {
                transform.LookAt(player.transform);
            }
        }
    }
}
