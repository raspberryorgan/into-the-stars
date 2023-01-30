using UnityEngine;

public static class ObjectPoolExtensions
{
    public static T SpawnPooledObject<T>(this T pooledObject, Vector2 position = default, Quaternion rotation = default, Transform parent = null) where T : Component
    {
        return GameManager.Instance.objectPool.SpawnPooledObject(pooledObject, position, rotation, parent);
    }

    public static T SpawnPooledObject<T>(this T pooledObject, Transform parent = null) where T : Component
    {
        return GameManager.Instance.objectPool.SpawnPooledObject(pooledObject, parent);
    }

    public static void ReturnToPool<T>(this T pooledObject) where T : Component
    {
        GameManager.Instance.objectPool.ReturnToPool(pooledObject);
    }
}

