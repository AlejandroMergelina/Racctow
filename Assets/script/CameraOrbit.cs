using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{

    [SerializeField]
    private Transform main;

    [SerializeField]
    private Vector3 finalTarget;
    private Vector3 target;

    [SerializeField]
    private float radio, height;

    [SerializeField]
    private InputManager inputManager;
    private float angle;
    private bool canRotate = true;

    private void OnEnable()
    {
        inputManager.OnRotateCameraAction += ChageAngel;
    }

    void LateUpdate()
    {
        target = main.position;
        Orbit();
        LookAtTheTarget();

    }

    private void LookAtTheTarget()
    {

        Vector3 direction = (target - transform.position).normalized;

        CalculateAndSetAngleY(direction.x, direction.z);

        CalculateAndSetAngleX(Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2)), -direction.y);

        //float angely = Mathf.Asin(direction.x / (Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2))));

        //if (Mathf.Sign(direction.z) >= 0)
        //{

        //    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Mathf.Rad2Deg * angely, transform.rotation.eulerAngles.z);

        //}
        //else if(Mathf.Sign(direction.z) < 0 && Mathf.Sign(direction.x) >= 0)
        //{

        //    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180 - Mathf.Rad2Deg * angely, transform.rotation.eulerAngles.z);

        //}
        //else
        //{

        //    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -180 - Mathf.Rad2Deg * angely, transform.rotation.eulerAngles.z);

        //}

    }

    private void CalculateAndSetAngleY(float c1, float c2)
    {

        float angely = Mathf.Asin(c1 / (Mathf.Sqrt(Mathf.Pow(c1, 2) + Mathf.Pow(c2, 2))));

        if (Mathf.Sign(c2) >= 0)
        {

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Mathf.Rad2Deg * angely, transform.rotation.eulerAngles.z);

        }
        else if (Mathf.Sign(c2) < 0 && Mathf.Sign(c1) >= 0)
        {

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180 - Mathf.Rad2Deg * angely, transform.rotation.eulerAngles.z);

        }
        else
        {

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -180 - Mathf.Rad2Deg * angely, transform.rotation.eulerAngles.z);

        }

    }
    private void CalculateAndSetAngleX(float c1, float c2)
    {

        float angely = Mathf.Acos(c1 / (Mathf.Sqrt(Mathf.Pow(c1, 2) + Mathf.Pow(c2, 2))));

        print(Mathf.Rad2Deg * angely + "/" + c1 + "/" + c2);
        transform.rotation = Quaternion.Euler(Mathf.Rad2Deg * angely, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        //if (Mathf.Sign(c2) >= 0)
        //{

        //    transform.rotation = Quaternion.Euler(Mathf.Rad2Deg * angely, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        //}
        //else if (Mathf.Sign(c2) < 0 && Mathf.Sign(c1) >= 0)
        //{

        //    transform.rotation = Quaternion.Euler(180 - Mathf.Rad2Deg * angely, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        //}
        //else
        //{

        //    transform.rotation = Quaternion.Euler(-180 - Mathf.Rad2Deg * angely, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        //}

    }


    private void Orbit()
    {

        Vector3 currentLocalPosition = new Vector3();

        currentLocalPosition.x = radio * Mathf.Sin(angle * Mathf.Deg2Rad);
        currentLocalPosition.z = radio * Mathf.Cos(angle * Mathf.Deg2Rad);
        currentLocalPosition.y = height;

        transform.position = target + currentLocalPosition;

    }

    private void ChageAngel()
    {
        print("hola");
        if (canRotate)
        {
            if (angle >= 360 || angle <= -360)
            {

                angle = 0;

            }
            StartCoroutine(InterpolarRotacion());
        }

    }

    IEnumerator InterpolarRotacion()
    {
        
        canRotate = false;

        

        float rotacionInicial = angle;
        float rotacionFinal = angle + (45 * inputManager.GetCameraRotateValue());
        float timer = 0;
        float tiempo = 0.5f;
        while (timer < tiempo)
        {
            
            float rotacionActual = Mathf.Lerp(rotacionInicial, rotacionFinal, timer / tiempo);
            
            angle = rotacionActual;
            timer += Time.deltaTime;
            yield return null;
        }
        
        angle = rotacionFinal;
        canRotate = true;
    }

    private void MoveSpring()
    {

        

    }
    
}
