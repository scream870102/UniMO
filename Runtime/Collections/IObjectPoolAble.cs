using UnityEngine;

namespace Scream.UniMO.Collections
{
    /// <summary>
    /// Inherit this interface make objects can be managed by class ObjectPool
    /// </summary>
    public interface IObjectPoolAble
    {
        /// <summary>
        /// which pool is object belongs to 
        /// </summary>
        /// <value></value>
        ObjectPool Pool { get; set; }

        /// <summary>
        /// Call this method to recycle object back to pool
        /// </summary>
        void Recycle();

        /// <summary>
        /// Call this method to init data of object
        /// </summary>
        /// <param name="data">data</param>
        /// <typeparam name="T">type of data</typeparam>
        void Init<T>(T data);

        /// <summary>
        /// The gameobject of pool object
        /// </summary>
        public GameObject GameObject { get; set; }
    }
}
