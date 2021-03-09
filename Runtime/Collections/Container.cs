using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scream.UniMO.Collections
{
    /// <summary>
    /// Use this scriptableobject to store all prefab you need.
    /// </summary>
    /// <typeparam name="T">type of key</typeparam>
    /// <typeparam name="U">type of target object</typeparam>
    public class Container<T, U> : ScriptableObject
    where T : IConvertible
    {
        public ObjectContainer<T, U>[] Objects = null;

        /// <summary>
        /// Convert to list and ignore key
        /// </summary>
        public virtual List<U> ToList()
        {
            var result = new List<U>();
            foreach (var o in Objects)
                result.Add(o.Object);
            return result;
        }

        /// <summary>
        /// Convert to Dictionary
        /// </summary>
        public virtual Dictionary<T, U> ToDictionary()
        {
            var result = new Dictionary<T, U>();
            foreach (var o in Objects)
            {
                if (result.ContainsKey(o.Key))
                {
                    Debug.LogError($"{o.Key} already exists");
                    continue;
                }
                result.Add(o.Key, o.Object);
            }
            return result;
        }

    }

    /// <summary>
    /// Container Type
    /// </summary>
    /// <typeparam name="T">type of key</typeparam>
    /// <typeparam name="U">type of object</typeparam>
    [Serializable]
    public class ObjectContainer<T, U>
    where T : IConvertible
    {
        public T Key;
        public U Object;
    }
}

