using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;

/// <summary>
/// 使用对象池的时候，新创建一个脚本，继承这个类，就可以了
/// </summary>
public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;
    public int currentPoolCount;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab,transform);
            obj.SetActive(false);
            pool.Enqueue(obj);//进池子
        }
    }

    private void Update() {
        currentPoolCount = pool.Count;
    }

    public GameObject GetObjectFromPool(Vector3 position, Quaternion rotation)//从池子中拿
    {
        if (pool.Count == 0)
        {
            GameObject obj = Instantiate(prefab,transform);
            pool.Enqueue(obj);
        }

        GameObject pooledObject = pool.Dequeue();
        pooledObject.transform.position = position;
        pooledObject.transform.rotation = rotation;
        pooledObject.SetActive(true);
        return pooledObject;
    }

    public void ReturnObjectToPool(GameObject obj)//返回池子
    {
        obj.SetActive(false);
        // if (pool.Count == 10)
        // {
        //     pool.Enqueue(obj);
        // }
        pool.Enqueue(obj);
    }

    public void ReturnObjectToPool(GameObject obj,float time)//几秒后返回池子
    {
        StartCoroutine(Delay(time,obj));
    }

    IEnumerator Delay(float time,GameObject obj)
    {
        yield return new WaitForSeconds(time);
        if (obj.activeSelf)
            ReturnObjectToPool(obj);
    }
}
