using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages game resources for processing gain (better garbage collection usage)
/// </summary>
public class ObjectPool : MonoBehaviour
{

    //public static void PreSpawnObjects<T>(T prefab, int count) where T : Component
    //{
    //    for (int i = 0; i < count; i++)
    //    {
    //        T pooledObject = SpawnPooledObject(prefab);
    //        ReturnToPool(pooledObject);
    //    }
    //}

    private Dictionary<GameObject, Pool> pools = new Dictionary<GameObject, Pool>();
    private  Dictionary<GameObject, Component> components = new Dictionary<GameObject, Component>();

    private Transform poolContainer;
    private Transform spawnContainer => ObjectPoolContainer.SpawnContainer;

    public const string NullError = "Danger! Null object sent to pool";

    private void Awake()
    {
        if (poolContainer != null)
            Destroy(gameObject);

        GameManager.Instance.objectPool = this;
        poolContainer = transform;
    }

    public  T SpawnPooledObject<T>(GameObject prefab, Transform parent) where T : Component
    {
        return SpawnPooledObject<T>(prefab, default, default, parent);
    }

    public  T SpawnPooledObject<T>(GameObject prefab, Vector2 position = default, Quaternion rotation = default, Transform parent = null) where T : Component
    {
        T component = prefab.GetComponent<T>(); //From pool
        return SpawnPooledObject(component, position, rotation, parent);
    }

    public T SpawnPooledObject<T>(T prefab, Transform parent) where T : Component
    {
        return SpawnPooledObject(prefab, default, default, parent);
    }

    public  T SpawnPooledObject<T>(T prefab, Vector2 position = default, Quaternion rotation = default, Transform parent = null) where T : Component
    {
        if (parent == null)
        {
            parent = spawnContainer;
        }

        if (!pools.ContainsKey(prefab.gameObject))
        {
            Pool createdPool = new Pool(prefab.name, poolContainer);
            T instance = Instantiate(prefab, position, rotation, parent);

            pools[prefab.gameObject] = createdPool;
            pools[instance.gameObject] = createdPool;
            createdPool.AddActiveInstance(instance);

            components[instance.gameObject] = instance;
            return instance;
        }

        Pool pool = pools[prefab.gameObject];

        if (pool.AvailableInstances > 0)
        {
            Component poolComponent = pool.GetFromPool();
            T component = poolComponent as T;

            if (component == null)
                component = poolComponent.GetComponent<T>();

            component.transform.SetPositionAndRotation(position, rotation);
            component.transform.SetParent(parent);
            component.gameObject.SetActive(true);

            return component;
        }
        else
        {
            T component = Instantiate(prefab, position, rotation, parent);
            GameObject componentGameObject = component.gameObject;
            components[componentGameObject] = component;
            pools[componentGameObject] = pool;
            pool.AddActiveInstance(component);
            return component;
        }
    }

    public  void ReturnToPool<T>(T instance) where T : Component
    {
        if (pools.ContainsKey(instance.gameObject))
        {
            pools[instance.gameObject].AddToPool(components[instance.gameObject]);
            return;
        }

        GameObject instanceGameObject = instance.gameObject;
        components[instanceGameObject] = instance;
        Pool pool = new Pool(instance.name, poolContainer);
        pools[instanceGameObject] = pool;
        pool.AddToPool(instance);
    }

    public void ReturnAllToPool()
    {
        foreach (Pool pool in pools.Values.ToList())
        {
            foreach (Component instance in pool.ActiveInstances.ToList())
            {
                if (instance == null)
                {
                    Debug.LogError(NullError);
                    pool.ActiveInstances.Remove(instance);
                    continue;
                }

                pool.AddToPool(instance);
            }
        }
    }

    private void OnDestroy()
    {
        //pools.Clear();
    }
}
