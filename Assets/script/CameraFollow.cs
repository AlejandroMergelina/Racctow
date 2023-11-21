using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private CharacterController target;
    [SerializeField]
    private Vector3 focusAreaSize;

    [SerializeField]
    private float verticalOffset;

    private FocusArea focusArea;

    private void Start()
    {

        focusArea = new FocusArea(target.bounds, focusAreaSize);


    }

    private void LateUpdate()
    {

        focusArea.Update(target.bounds);
        Vector3 focusPosition = focusArea.Center + Vector3.forward * verticalOffset;

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
        public Vector3 Velicity { get => velocity; set => velocity = value; }

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
