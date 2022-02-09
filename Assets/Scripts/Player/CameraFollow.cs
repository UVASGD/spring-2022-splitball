using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // object to follow
    public float smoothingSpeed;    // "lag" between player moving and camera catching up

    public Vector3 offset;          // offset vector from player

    public BoxCollider2D boundBox; // the bounds of where the camera can go
    private Vector3 minbounds;  //min x,y that the camera can go
    private Vector3 maxbounds; //max x,y that camera can go

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;


    private void Start() {
        // initialize camera position and rotation
      
        //commented out in order to properly follow midpoint of two balls
        //target = GameObject.Find("Player").GetComponent<Transform>();
        //transform.position = target.transform.position + offset;
        
        minbounds = boundBox.bounds.min;
        maxbounds = boundBox.bounds.max;

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight*Screen.width/Screen.height;
    }

    private void Update() {
        // move camera with player, interpolating based on amount of smoothing
        Vector3 newPosition;
        if (target){
            newPosition = target.position + offset;
        } else {
            newPosition = transform.position;
        }
        
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothingSpeed);

        float clampedX = Mathf.Clamp(transform.position.x, minbounds.x+halfWidth, maxbounds.x - halfWidth);
        float clampedy = Mathf.Clamp(transform.position.y, minbounds.y+halfHeight, maxbounds.y - halfHeight);
        transform.position = new Vector3(clampedX, clampedy, transform.position.z);
    }

    public void SetBounds(BoxCollider2D newBounds){
        boundBox = newBounds;
        minbounds = boundBox.bounds.min;
        maxbounds = boundBox.bounds.max;
    }
}
