#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Scream.UniMO.Editor
{
    /// <summary>
    /// Use this class to get object instance id 
    /// <para>You can also get id in editor mode</para>
    /// </summary>
    public class GetInstanceID : MonoBehaviour
    {
        [SerializeField] Object _object;
        /// <summary>
        /// Call this method to get instance id 
        /// </summary>
        public void GetID() => Debug.Log(_object.GetInstanceID());
    }

    [CustomEditor(typeof(GetInstanceID))]
    public class GetInStanceIDEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GetInstanceID myScript = (GetInstanceID)target;
            if (GUILayout.Button("GetID"))
                myScript.GetID();
        }
    }

}
#endif
