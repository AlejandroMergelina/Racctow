using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prueva
{
    public class CameraFollow2 : MonoBehaviour
    {
        //camera orbit
        private Vector3 target;

        [SerializeField]
        private float radio, height;

        [SerializeField]
        private InputManager inputManager;
        [SerializeField]
        private float angle;
        private bool canRotate = true;

        //camera follow

        [SerializeField]
        private CharacterController controller;
        [SerializeField]
        private Transform mainTransform;
        [SerializeField]
        private float verticalOffset;
        [SerializeField]
        private float lookAheadDstX;
        [SerializeField]
        private float lookAheadDstZ;
        [SerializeField]
        private float loockSmoothTimeX;
        [SerializeField]
        private float loockSmoothTimeZ;
        [SerializeField]
        private Vector3 focusAreaSize;
        [SerializeField]
        private Move main;

        private FocusArea focusArea;

        private float currentLookAheadX;
        private float targetLookAheadX;
        private float lookAheadDirX;
        private float smoothLoockVelocityX;

        private float currentLookAheadZ;
        private float targetLookAheadZ;
        private float lookAheadDirZ;
        private float smoothLoockVelocityZ;

        private bool lookAheadStoppedX;
        private bool lookAheadStoppedZ;

        private void OnEnable()
        {
            inputManager.OnRotateCameraAction += ChangeAngle;
        }

        private void Start()
        {

            focusArea = new FocusArea(mainTransform, focusAreaSize);


        }

        private void LateUpdate()
        {
            focusArea.Update(mainTransform, angle);
            Vector3 focusPosition = focusArea.Center + Vector3.forward * verticalOffset;
            //print("input: " + focusArea.Velocity + " / " + main.transform.forward);
            Vector3 currentInputDirection = main.transform.rotation * main.MovementDirection;
            if (focusArea.Velocity.x != 0)
            {

                lookAheadDirX = Mathf.Sign(focusArea.Velocity.x);

                if (Mathf.Sign(main.transform.forward.x) == Mathf.Sign(focusArea.Velocity.x) && currentInputDirection.x != 0)
                {
                    lookAheadStoppedX = false;
                    targetLookAheadX = lookAheadDirX * lookAheadDstX;

                }

            }
            else if (!lookAheadStoppedX)
            {


                lookAheadStoppedX = true;
                targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;




            }

            if (focusArea.Velocity.z != 0)
            {

                lookAheadDirZ = Mathf.Sign(focusArea.Velocity.z);

                if (Mathf.Sign(main.transform.forward.z) == Mathf.Sign(focusArea.Velocity.z) && currentInputDirection.z != 0)
                {
                    lookAheadStoppedZ = false;
                    targetLookAheadZ = lookAheadDirZ * lookAheadDstZ;

                }

            }
            else if (!lookAheadStoppedZ)
            {


                lookAheadStoppedZ = true;
                targetLookAheadZ = currentLookAheadZ + (lookAheadDirZ * lookAheadDstZ - currentLookAheadZ) / 4f;




            }

            currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLoockVelocityX, loockSmoothTimeX);
            currentLookAheadZ = Mathf.SmoothDamp(currentLookAheadZ, targetLookAheadZ, ref smoothLoockVelocityZ, loockSmoothTimeZ);

            focusPosition += Vector3.forward * currentLookAheadZ;
            focusPosition += Vector3.right * currentLookAheadX;
            target = focusPosition;
            transform.position = Orbit();
            LookAtTheTarget();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, .5f);
            Gizmos.DrawLine(focusArea.Corners[0], focusArea.Corners[1]);
            Gizmos.DrawLine(focusArea.Corners[1], focusArea.Corners[2]);
            Gizmos.DrawLine(focusArea.Corners[2], focusArea.Corners[3]);
            Gizmos.DrawLine(focusArea.Corners[3], focusArea.Corners[0]);
            //Gizmos.DrawCube(focusArea.Center, focusAreaSize);
            //Gizmos.color = Color.blue;
            //Vector3 shift = Vector3.zero;
            //Vector3 q;
            //focusArea.Distacias(focusArea.Corners[0], focusArea.Corners[1], mainTransform.position, out shift, out q);
            //print(mainTransform.position + " / " + q);
            //Gizmos.DrawLine(q, mainTransform.position);
            //focusArea.Distacias(focusArea.Corners[1], focusArea.Corners[2], mainTransform.position, out shift, out q);
            //print(mainTransform.position + " / " + q);
            //Gizmos.DrawLine(q, mainTransform.position);
            //focusArea.Distacias(focusArea.Corners[2], focusArea.Corners[3], mainTransform.position, out shift, out q);
            //print(mainTransform.position + " / " + q);
            //Gizmos.DrawLine(q, mainTransform.position);
            //focusArea.Distacias(focusArea.Corners[3], focusArea.Corners[0], mainTransform.position, out shift, out q);
            //print(mainTransform.position + " / " + q);
            //Gizmos.DrawLine(q, mainTransform.position);
            Gizmos.DrawSphere(target, 0.2f);

        }

        struct FocusArea
        {
            [SerializeField]
            private Vector3 center;
            public Vector3 Center { get => center;}

            private Vector3[] corners;
            public Vector3[] Corners { get => corners;}

            private float[] cornersAngle;

            [SerializeField]
            private Vector3 velocity;
            public Vector3 Velocity { get => velocity;}

            private float left, right;
            private float top, bottom;

            private Vector3 size;

            public FocusArea(Transform target, Vector3 size)
            {
                this.size = size;
                left = target.position.x - size.x / 2;
                right = target.position.x + size.x / 2;
                bottom = target.position.z - size.z / 2;
                top = target.position.z + size.z / 2;
                //print(top + "/" + left + "/" + right + "/" + bottom);
                corners = new Vector3[4];

                velocity = Vector3.zero;
                center = new Vector3((left + right) / 2, 0, (top + bottom) / 2);

                corners[0] = new Vector3(right, 0, top);
                corners[1] = new Vector3(right, 0, bottom);
                corners[2] = new Vector3(left, 0, bottom);
                corners[3] = new Vector3(left, 0, top);

                //corners[0] = new Vector3(Mathf.Sqrt(2), 0, 0) + target.position;
                //corners[1] = new Vector3(0, 0, -Mathf.Sqrt(2)) + target.position;
                //corners[2] = new Vector3(-Mathf.Sqrt(2), 0, 0) + target.position;
                //corners[3] = new Vector3(0, 0, Mathf.Sqrt(2)) + target.position;

                cornersAngle = new float[4];

                

            }

            private float calculateAngle()
            {

                return 0;

            }

            public void Update(Transform target, float angle)
            {

                

                float shiftX = 0;
                float shiftZ = 0;

                float HorizontalDistanceLeft = Distacias(corners[1], corners[0], target.position);
                float HorizontalDistanceRight = Distacias(corners[2], corners[3], target.position);
                
                float HorizontalDistances = HorizontalDistanceLeft + HorizontalDistanceRight;

                if(size.x < HorizontalDistances)
                {

                    if(HorizontalDistanceRight < HorizontalDistanceLeft)
                    {

                        Vector3 directorVector = (corners[0] - corners[3]).normalized * HorizontalDistanceRight;

                        //shiftX = directorVector.x;
                        //shiftZ = directorVector.z;

                    }
                    else if (HorizontalDistanceRight > HorizontalDistanceLeft)
                    {

                        Vector3 directorVector = (corners[3] - corners[0]).normalized * HorizontalDistanceLeft;
                        //shiftX = directorVector.x;
                        //shiftZ = directorVector.z;

                    }


                }

                float VerticalDistancesTop = Distacias(corners[0], corners[3], target.position);
                float VerticalDistancesBottom = Distacias(corners[1], corners[2], target.position);

                float VerticalDistances = VerticalDistancesTop + VerticalDistancesBottom;

                if (size.z < VerticalDistances)
                {

                    if (VerticalDistancesTop < VerticalDistancesBottom)
                    {

                        Vector3 directorVector = (corners[2] - corners[3]).normalized * VerticalDistancesTop;

                        //shiftX = directorVector.x;
                        //shiftZ = directorVector.z;

                    }
                    else if (VerticalDistancesTop > VerticalDistancesBottom)
                    {

                        Vector3 directorVector = (corners[3] - corners[2]).normalized * VerticalDistancesBottom;
                        //shiftX = directorVector.x;
                        //shiftZ = directorVector.z;

                    }


                }

                print("distancias = " + HorizontalDistanceLeft + " / " + HorizontalDistanceRight + " / " + VerticalDistancesTop + " / " + VerticalDistancesBottom);

                //if (targetBounds.center.x < left)
                //{

                //    shiftX = targetBounds.min.x - left;

                //}
                //else if (targetBounds.max.x > right)
                //{

                //    shiftX = targetBounds.max.x - right;

                //}
                //left += shiftX;
                //right += shiftX;

                //if (targetBounds.min.z < bottom)
                //{

                //    shiftZ = targetBounds.min.z - bottom;

                //}
                //else if (targetBounds.max.z > top)
                //{

                //    shiftZ = targetBounds.max.z - top;

                //}
                //top += shiftZ;
                //bottom += shiftZ;
                //center = new Vector3((left + right) / 2, 0, (top + bottom) / 2);

                corners[0] += new Vector3(shiftX, 0, shiftZ);
                corners[1] += new Vector3(shiftX, 0, shiftZ);
                corners[2] += new Vector3(shiftX, 0, shiftZ);
                corners[3] += new Vector3(shiftX, 0, shiftZ);
                center = (corners[0] - corners[2]) / 2 + corners[2];

                velocity = new Vector3(shiftX, 0, shiftZ);

            }
            
            public float Distacias(Vector3 corner1, Vector3 corner2, Vector3 main/*, out Vector3 shift,out Vector3 q*/)
            {
                //print("esquina : " + corner1 + " / " + corner2);
                //float a = (corner2.z - corner1.z)/ (corner2.x - corner1.x);
                //float b = -1;
                //float c = (corner1.z - a * corner1.x )* (corner2.x - corner1.x);
                ///*Vector3*/ q = Vector3.zero;
                //q.x = main.x - (a * (a * main.x + b * main.z + c)) / (a * a + b * b);
                //q.z = main.z -(b * (a * main.x + b * main.z + c)) / (a * a + b * b);
                //print(q.x);
                ////Vector3 Uc = Vector3.zero;
                ////Vector3 U = new Vector3(main.x - (-c), 0, main.x);
                ////Vector3 V = new Vector3(a, 0, b);

                ////Uc = U - ((U.x * V.x - U.z * V.z) / V.magnitude) * V;

                ////q = main + Uc;

                ////shift = main - q;

                //shift = main - q;
                print(corner1 + " / " + corner2);

                float a = 0;
                float b = 0;
                float c = 0;

                if ((corner2.x - corner1.x) == 0)
                {
                    a = -1;
                    b = (corner2.x - corner1.x) / (corner2.z - corner1.z);
                    c = (((corner2.x - corner1.x)  / (corner2.z - corner1.z)) * (-corner1.z)) + corner1.x;
                }
                else
                {

                    a = (corner2.z - corner1.z) / (corner2.x - corner1.x);
                    b = -1;
                    c = (((corner2.z - corner1.z) / (corner2.x - corner1.x)) * (-corner1.x))+ corner1.z;

                }

                print(a + " / " + b + " / " + c);

                return Mathf.Abs(a * main.x + b * main.z + c) / Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));

            }

           
        }

        // camera orbit

        private void LookAtTheTarget()
        {

            Vector3 direction = (target - transform.position).normalized;

            CalculateAndSetAngleY(direction.x, direction.z);

            CalculateAndSetAngleX(Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2)), -direction.y);

        }

        private void CalculateAndSetAngleY(float c1, float c2)
        {

            float angely = Mathf.Asin(c1 / Mathf.Sqrt(Mathf.Pow(c1, 2) + Mathf.Pow(c2, 2)));

            if (Mathf.Sign(c2) >= 0)
            {

                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Mathf.Rad2Deg * angely, transform.eulerAngles.z);

            }
            else if (Mathf.Sign(c2) < 0 && Mathf.Sign(c1) >= 0)
            {

                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180 - Mathf.Rad2Deg * angely, transform.eulerAngles.z);

            }
            else
            {

                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, -180 - Mathf.Rad2Deg * angely, transform.eulerAngles.z);

            }

        }
        private void CalculateAndSetAngleX(float c1, float c2)
        {

            float angely = Mathf.Acos(c1 / Mathf.Sqrt(Mathf.Pow(c1, 2) + Mathf.Pow(c2, 2)));

            transform.rotation = Quaternion.Euler(Mathf.Rad2Deg * angely, transform.eulerAngles.y, transform.eulerAngles.z);

        }


        private Vector3 Orbit()
        {

            Vector3 currentLocalPosition = new Vector3();

            currentLocalPosition.x = radio * Mathf.Sin(angle * Mathf.Deg2Rad);
            currentLocalPosition.z = radio * Mathf.Cos(angle * Mathf.Deg2Rad);
            currentLocalPosition.y = height;

            return  target + currentLocalPosition;

        }

        private void ChangeAngle()
        {
            print("hola");
            if (canRotate)
            {
                angle %= 360;
                StartCoroutine(InterpolarRotacion());
            }

        }

        IEnumerator InterpolarRotacion()
        {

            canRotate = false;



            float rotacionInicial = angle;
            float rotacionFinal = angle + 45 * inputManager.GetCameraRotateValue();
            float timer = 0;
            float tiempo = 0.5f;
            while (timer < tiempo)
            {

                float rotacionActual = Mathf.Lerp(rotacionInicial, rotacionFinal, timer / tiempo);

                angle = rotacionActual;
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            angle = rotacionFinal;
            canRotate = true;
        }
    }
}