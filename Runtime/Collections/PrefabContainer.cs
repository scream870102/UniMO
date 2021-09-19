using UnityEngine;
using Object = UnityEngine.Object;

namespace Scream.UniMO.Collections
{
    /// <summary>
    /// Use this scriptableobject to store all prefab you need.
    /// </summary>
    [CreateAssetMenu(fileName = "PrefabContainer", menuName = "ScriptableObjects/PrefabContainer")]
    public class PrefabContainer : ScriptableObject
    {
        public Container<string, Object>[] PrefabDictionary;
    }
}

