using System;
using System.Collections.Generic;

namespace Scream.UniMO.Collections
{
    /// <summary>
    /// Use this scriptableobject to store all object you need.
    /// <para>Inherit this class to fit your own object</para>
    /// </summary>
    /// <typeparam name="T">type of key</typeparam>
    /// <typeparam name="U">type of target object</typeparam>
    [Serializable]
    public class Container<T, U>
    where T : IConvertible
    {
        public T Key;
        public U Value;

        /// <summary>
        /// Convert to dictionary
        /// </summary>
        /// <param name="collection">which collection to be found</param>
        /// <returns>Convert Objects to dictionary</returns>
        public static Dictionary<T, U> ToDictionary(IEnumerable<Container<T, U>> collection)
        {
            var result = new Dictionary<T, U>();
            foreach (var o in collection)
            {
                if (result.ContainsKey(o.Key))
                {
                    continue;
                }
                result.Add(o.Key, o.Value);
            }
            return result;
        }

        /// <summary>
        /// Convert to list and ignore key
        /// </summary>
        /// <param name="collection">which collection to be found</param>
        /// <returns>convert the array of Objects to list</returns>
        public static List<U> ToList(IEnumerable<Container<T, U>> collection)
        {
            var result = new List<U>();
            foreach (var o in collection)
                result.Add(o.Value);
            return result;
        }

        /// <summary>
        /// Check if this collection contains specific value
        /// </summary>
        /// <param name="value">value to find</param>
        /// <param name="collection">which collection to be found</param>
        /// <returns>return the result</returns>
        public static bool ContainsValue(U value, IEnumerable<Container<T, U>> collection)
        {
            foreach (var pair in collection)
            {
                if (pair.Value.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check if this collection contains specific key 
        /// </summary>
        /// <param name="key">key to find</param>
        /// <param name="collection">which collection to be found</param>
        /// <returns>return the result</returns>
        public static bool ContainsKey(T key, IEnumerable<Container<T, U>> collection)
        {
            foreach (var pair in collection)
            {
                if (pair.Key.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get all the keys from a collection
        /// </summary>
        /// <param name="collection">resource collection</param>
        /// <returns>all the keys as array</returns>
        public static T[] Keys(IEnumerable<Container<T, U>> collection)
        {
            var result = new List<T>();
            foreach (var pair in collection)
            {
                result.Add(pair.Key);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Get all the values from a collection
        /// </summary>
        /// <param name="collection">resource collection</param>
        /// <returns>all the values as array</returns>
        public static U[] Values(IEnumerable<Container<T, U>> collection)
        {
            var result = new List<U>();
            foreach (var pair in collection)
            {
                result.Add(pair.Value);
            }
            return result.ToArray();
        }

    }

    /// <summary>
    /// ContainerExtension contains extension method for container class
    /// </summary>
    public static class ContainerExtension
    {
        /// <summary>
        /// Convert to list and ignore key
        /// </summary>
        /// <param name="collection">resource collection</param>
        /// <typeparam name="T">type of key</typeparam>
        /// <typeparam name="U">type of value</typeparam>
        /// <returns>all object to list</returns>
        public static List<U> ToList<T, U>(this IEnumerable<Container<T, U>> collection)
        where T : IConvertible
        {
            return Container<T, U>.ToList(collection);
        }

        /// <summary>
        /// Convert to dictionary ,ignore value key pair if key already exists
        /// </summary>
        /// <param name="collection">resource collection</param>
        /// <typeparam name="T">type of key</typeparam>
        /// <typeparam name="U">type of value</typeparam>
        /// <returns>all object to dictionary</returns>
        public static Dictionary<T, U> ToDictionary<T, U>(this IEnumerable<Container<T, U>> collection)
        where T : IConvertible
        {
            return Container<T, U>.ToDictionary(collection);
        }

        /// <summary>
        /// Check if this collection contains specific value
        /// </summary>
        /// <param name="collection">resource collection</param>
        /// <param name="value">value to find</param>
        /// <typeparam name="T">type of key</typeparam>
        /// <typeparam name="U">type of value</typeparam>
        /// <returns>result</returns>
        public static bool ContainsValue<T, U>(this IEnumerable<Container<T, U>> collection, U value)
        where T : IConvertible
        {
            return Container<T, U>.ContainsValue(value, collection);
        }

        /// <summary>
        /// Check if this collection contains specific key 
        /// </summary>
        /// <param name="collection">resource collection</param>
        /// <param name="key">key to find</param>
        /// <typeparam name="T">type of key</typeparam>
        /// <typeparam name="U">type of value</typeparam>
        /// <returns>result</returns>
        public static bool ContainsKey<T, U>(this IEnumerable<Container<T, U>> collection, T key)
        where T : IConvertible
        {
            return Container<T, U>.ContainsKey(key, collection);
        }

        /// <summary>
        /// Get all the keys from a collection
        /// </summary>
        /// <param name="collection">resource collection</param>
        /// <typeparam name="T">type of key</typeparam>
        /// <typeparam name="U">type of value</typeparam>
        /// <returns>all the keys as array</returns>
        public static T[] Keys<T, U>(this IEnumerable<Container<T, U>> collection)
        where T : IConvertible
        {
            return Container<T, U>.Keys(collection);
        }

        /// <summary>
        /// Get all the values from a collection
        /// </summary>
        /// <param name="collection">resource collection</param>
        /// <typeparam name="T">type of key</typeparam>
        /// <typeparam name="U">type of value</typeparam>
        /// <returns>all the values as array</returns>
        public static U[] Values<T, U>(this IEnumerable<Container<T, U>> collection)
        where T : IConvertible
        {
            return Container<T, U>.Values(collection);
        }

    }
}
