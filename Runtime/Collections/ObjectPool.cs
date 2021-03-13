using System.Collections.Generic;
using UnityEngine;

namespace Scream.UniMO.Collections
{
    /// <summary>
    /// ObjectPool is the class to handle object recycle and spawn
    /// </summary>
    [System.Serializable]
    public class ObjectPool
    {
        /// <summary>
        /// Object to spawn
        /// </summary>
        [Header("ObjectPool")]
        public GameObject PooledObject;


        /// <summary>
        /// Parent for all pool objects can be null
        /// </summary>
        public Transform PoolParent;

        /// <summary>
        /// How many item can pool hold
        /// </summary>
        public int PooledAmount;

        /// <summary>
        /// define if pool will spawn new object when pool is empty
        /// </summary>
        public bool IsGrow;

        /// <summary>
        /// Call this property to check pool holds anything now
        /// </summary>
        public bool IsAvailable => poolObjects.Count > 0;

        // To Store all item
        Queue<IObjectPoolAble> poolObjects;

        /// <summary>
        /// use this to set NECESSARY data via script
        /// <para>You can also set data through inspector but should call init by self</para>
        /// </summary>
        /// <param name="pooledObject">object to spawn</param>
        /// <param name="poolParent">parent for poolobjct</param>
        /// <param name="pooledAmount">how many object can pool hold</param>
        /// <param name="isGrow">can pool spawn new object when pool is empty</param>
        public ObjectPool(GameObject pooledObject, Transform poolParent, int pooledAmount = 1, bool isGrow = true)
        {
            PooledObject = pooledObject;
            PoolParent = poolParent;
            PooledAmount = pooledAmount;
            IsGrow = isGrow;
            Init();
        }

        /// <summary>
        /// Spawn all objects according to pooledAmount
        /// <para>MUST CALL this method if you set data with inspector not constructor</para>
        /// </summary>
        public void Init()
        {
            poolObjects = new Queue<IObjectPoolAble>();
            for (int i = 0; i < PooledAmount; i++)
                poolObjects.Enqueue(SpawnObject());
        }

        /// <summary>
        /// Return pool object from pool
        /// </summary>
        /// <param name="data">data for init pool object</param>
        /// <typeparam name="T">init data type</typeparam>
        /// <returns>the pool object it will be null when pool is empty</returns>
        public IObjectPoolAble GetPooledObject<T>(T data)
        {
            if (poolObjects.Count != 0)
            {
                IObjectPoolAble item = poolObjects.Dequeue();
                item.Init<T>(data);
                item.GameObject.SetActive(true);
                return item;
            }
            if (IsGrow)
            {
                IObjectPoolAble item = SpawnObject();
                item.Init<T>(data);
                item.GameObject.SetActive(true);
                return item;
            }
            return null;
        }

        /// <summary>
        /// Recycle Object to Pooling again
        /// </summary>
        /// <param name="item">which item will be Recycle to ObjectPooling</param>
        public void RecycleObject(IObjectPoolAble item)
        {
            poolObjects.Enqueue(item);
            item.GameObject.SetActive(false);
        }

        // Spawn Object and set its pool change its parent to poolParent then disable it
        IObjectPoolAble SpawnObject()
        {
            IObjectPoolAble item = GameObject.Instantiate(PooledObject, PoolParent).GetComponent<IObjectPoolAble>();
            item.Pool = this;
            item.GameObject.SetActive(false);
            return item;
        }
    }
}
