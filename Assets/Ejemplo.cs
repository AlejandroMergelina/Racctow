using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Ejemplo : MonoBehaviour
{
    private IObjectPool<bOLINCHI> pool;

    [SerializeField] private bOLINCHI prefab;
    public int CountInactive => throw new System.NotImplementedException();

    public IObjectPool<bOLINCHI> Pool { get => pool;}

    // Start is called before the first frame update
    void Start()
    {
        pool = new ObjectPool<bOLINCHI>(NewInstance, Get, Release, Destroy);
        StartCoroutine(EjemploCo());
        
    }
    private IEnumerator EjemploCo()
    {
        while (true)
        {
            Debug.Log("fdsfds");
            pool.Get();
            yield return new WaitForSeconds(0.25f);
           

        }
    }



    bOLINCHI NewInstance()
    {
        return Instantiate(prefab, transform.position, Quaternion.identity);
    }
    void Get(bOLINCHI b)
    {
        b.transform.position = b.PosicionInicial;
        b.gameObject.SetActive(true);
    }
    void Release(bOLINCHI b)
    {
        b.gameObject.SetActive(false);
    }
    void Destroy(bOLINCHI b)
    {
        Destroy(b.gameObject);
    }


}
