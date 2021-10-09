using UnityEngine;

namespace Scream.UniMO.Utils
{
    /// <summary>
    /// derived this class then you will got a monobehavior implement singleton
    /// </summary>
    /// <typeparam name="T">Target type to convert to singleton</typeparam>
    public class TSingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T instance = null;

        /// <summary>
        /// instance for this type
        /// </summary>
        /// <value>object of this class</value>
        public static T Instance
        {
            get { return instance ??= (FindObjectOfType(typeof(T)) as T); }
            set { instance = value; }
        }

        protected virtual void Awake()
        {
            if (instance == null) instance = this as T;
            if (instance == this) DontDestroyOnLoad(this);
            else DestroyImmediate(this);
        }

        protected virtual void OnDestroy() => instance = null;
    }

    /// <summary>
    /// derived this class then you will got a monobehavior implement singleton
    /// <para>however you can reassign instance value and it will destory old one if it exists</para>
    /// </summary>
    /// <typeparam name="T">Target type to convert to singleton</typeparam>
    public class TSingletonMonoBehaviorDestroy<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T instance = null;

        /// <summary>
        /// instance for this type
        /// </summary>
        /// <value>object of this class</value>
        public static T Instance
        {
            get { return instance ??= (FindObjectOfType(typeof(T)) as T); }
            set { instance = value; }
        }

        protected virtual void Awake()
        {
            if (instance == null) instance = this as T;
            if (instance != this)
            {
                DestroyImmediate(instance);
                instance = this as T;
            }
        }

        protected virtual void OnDestroy() => instance = null;
    }
}