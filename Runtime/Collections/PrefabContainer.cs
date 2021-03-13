using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scream.UniMO.Collections
{
    /// <summary>
    /// Use this scriptableobject to store all prefab you need.
    /// </summary>
    [CreateAssetMenu(fileName = "PrefabContainer", menuName = "ScriptableObjects/PrefabContainer")]
    public class PrefabContainer : Container<string, Object>
    {

        /// <summary>
        /// Convert object to other type
        /// </summary>
        /// <typeparam name="T">target type of object</typeparam>
        /// <returns>Objects list</returns>
        public List<T> ToList<T>()
        where T : Object
        {
            var result = new List<T>();
            foreach (var o in Objects)
                result.Add((T)o.Object);
            return result;
        }


        /// <summary>
        /// Convert key from string to basic type
        /// </summary>
        /// <typeparam name="T">Target key type</typeparam>
        /// <typeparam name="U">Object type</typeparam>
        /// <returns>Convert key to specif type and return as dictionary</returns>
        public Dictionary<T, U> ToDictionary<T, U>()
        where T : IConvertible
        where U : Object
        {
            var result = new Dictionary<T, U>();
            foreach (var o in Objects)
            {
                T key;
                if (typeof(T).IsEnum)
                    key = (T)Enum.Parse(typeof(T), o.Key);
                else
                    key = (T)Convert.ChangeType(o.Key, typeof(T));
                if (result.ContainsKey(key))
                {
                    Debug.LogError($"{o.Key} already exists");
                    continue;
                }
                result.Add(key, (U)o.Object);
            }
            return result;
        }
    }
}

