using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float lastXPos;
    private float textureUnitSizeX;

    public static CameraController instance;
    public Transform target;

    public Transform farBackground, middleBackground;

    public float parallaxMultiplier;

    public bool isFollowingTarget = true;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        lastXPos = transform.position.x;
    }

    void Update()
    {
        if (isFollowingTarget) {
            FollowTarget();
            MoveBackground();
        }
    }

    void FollowTarget() {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    
    }

    void MoveBackground() {

        float amountToMoveX = transform.position.x - lastXPos;

        farBackground.position += new Vector3(amountToMoveX, 0f, 0f);

        if (transform.position.x - middleBackground.position.x > 50) {
            middleBackground.position = new Vector3(middleBackground.position.x + 40, middleBackground.position.y, middleBackground.position.z);
        } else {
            middleBackground.position += new Vector3(amountToMoveX * parallaxMultiplier, 0f, 0f);
        }

        lastXPos = transform.position.x;

    }

}
