using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private CharacterController target;
    [SerializeField]
    private float verticalOffset;
    [SerializeField]
    private float lookAheadDstX;
    [SerializeField]
    private float loockSmoothTimeX;
    [SerializeField]
    private float verticalSmoothTime;
    [SerializeField]
    private Vector3 focusAreaSize;
    [SerializeField]
    private GameObject player;

    private FocusArea focusArea;

    private float currentLookAheadX;
    private float targetLookAheadX;
    private float lookAheadDirX;
    private float smoothLoockVelocityX;
    private float smoothVelocityY;

    private bool lookAheadStopped;

    private void Start()
    {

        focusArea = new FocusArea(target.bounds, focusAreaSize);


    }

    private void LateUpdate()
    {

        focusArea.Update(target.bounds);
        Vector3 focusPosition = focusArea.Center + Vector3.forward * verticalOffset;

        if(focusArea.Velocity.x != 0)
        {

            lookAheadDirX = Mathf.Sign(focusArea.Velocity.x);
            if(Mathf.Sign(player.transform.forward.x) == Mathf.Sign(focusArea.Velocity.x) && player.transform.forward.x!= 0)
            {
                lookAheadStopped= false;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;

            }
            else
            {
                if(lookAheadStopped)
                {
                    lookAheadStopped = true;
                    targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;

                }
                

            }
        }

        
        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLoockVelocityX, loockSmoothTimeX);


        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        focusPosition += Vector3.right * currentLookAheadX;
        transform.position = focusPosition + Vector3.up * 10;

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.Center, focusAreaSize);

    }

    struct FocusArea
    {
        [SerializeField]
        private Vector3 center;
        public Vector3 Center { get => center; set => center = value; }
        [SerializeField]
        private Vector3 velocity;
        public Vector3 Velocity { get => velocity; set => velocity = value; }

        private float left, right;
        private float top, bottom;

        public FocusArea(Bounds targetBounds, Vector3 size)
        {

            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.z - size.z / 2;
            top = targetBounds.min.z + size.z / 2;

            velocity = Vector3.zero;
            center = new Vector3((left + right) / 2,0, (top + bottom) / 2);

        }

        public void Update(Bounds targetBounds)
        {

            float shiftX = 0;
            if (targetBounds.min.x < left)
            {

                shiftX = targetBounds.min.x - left;

            }
            else if (targetBounds.max.x > right)
            {

                shiftX = targetBounds.max.x - right;

            }
            left += shiftX;
            right += shiftX;

            float shiftZ = 0;
            if (targetBounds.min.z < bottom)
            {

                shiftZ = targetBounds.min.z - bottom;

            }
            else if (targetBounds.max.z > top)
            {

                shiftZ = targetBounds.max.z - top;

            }
            top += shiftZ;
            bottom += shiftZ;
            center = new Vector3((left + right) / 2, 0, (top + bottom) / 2);
            velocity = new Vector3(shiftX, 0, shiftZ);

        }

    }

}
