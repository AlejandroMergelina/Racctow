using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraOrbit : MonoBehaviour
{

    [SerializeField]
    private Transform main;

    [SerializeField]
    private float finalTarget;
    private Vector3 target;
    private Vector3 lastDirection;
    private bool canTraslate;
    Coroutine call;

    [SerializeField]
    private float radio, height;

    [SerializeField]
    private InputManager inputManager;
    private float angle;
    private bool canRotate = true;

    private void OnEnable()
    {
        inputManager.OnRotateCameraAction += ChangeAngle;
        target = main.forward * finalTarget + main.position;
    }

    void LateUpdate()
    {
        target = main.forward * finalTarget + main.position;
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

        float angely = Mathf.Acos(c1 / (Mathf.Sqrt(Mathf.Pow(c1, 2) + Mathf.Pow(c2, 2))));

        print(Mathf.Rad2Deg * angely + "/" + c1 + "/" + c2);
        transform.rotation = Quaternion.Euler(Mathf.Rad2Deg * angely, transform.eulerAngles.y, transform.eulerAngles.z);

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

    private void ChangeAngle()
    {
        print("hola");
        if (canRotate)
        {
            //if (angle >= 360 || angle <= -360)
            //{

            //    angle = 0;

            //}
            angle %= 360;
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

        Vector3 direction = target - main.transform.position;
        float resultado = Vector3.Dot(direction, transform.forward);
        //Si resultado da positivo los dos vectores están apuntando a dir. similares.
        //1-: Ifualwa
        //-1: Opuestas
        //0: Perpendicular

        if ( call != null)
        {

            call = StartCoroutine(PositionInterpolate(direction));

        }
        else
        {
            StopCoroutine(call);
            call = StartCoroutine(PositionInterpolate(direction));

        }


        

    }

    IEnumerator PositionInterpolate(Vector3 finalDirection)
    {

        float timer = 0;
        float tiempo = 0.5f;
        while (timer < tiempo)
        {

            Vector3 curretPosition = Vector3.Lerp(lastDirection, finalDirection, timer / tiempo);

            target = curretPosition;
            timer += Time.deltaTime;
            yield return null;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(main.forward * finalTarget + main.position, 0.2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(target, 0.2f);
    }

    private void OnDisable()
    {
        inputManager.OnRotateCameraAction -= ChangeAngle;
    }

}
