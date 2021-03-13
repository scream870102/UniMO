using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scream.UniMO.Collections
{   
    /// <summary>
    /// Use this scriptableobject to store all object you need.
    /// <para>Inherit this class to fit your own object</para>
    /// </summary>
    /// <typeparam name="T">type of key</typeparam>
    /// <typeparam name="U">type of target object</typeparam>
    public class Container<T, U> : ScriptableObject
    where T : IConvertible
    {
        /// <summary>
        /// Store your objects and key here
        /// </summary>
        public ObjectContainer<T, U>[] Objects = null;

        /// <summary>
        /// Convert to list and ignore key
        /// </summary>
        /// <returns>convert the array of Objects to list</returns>
        public virtual List<U> ToList()
        {
            var result = new List<U>();
            foreach (var o in Objects)
                result.Add(o.Object);
            return result;
        }

        /// <summary>
        /// Convert to dictionary
        /// </summary>
        /// <returns>Convert Objects to dictionary</returns>
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

