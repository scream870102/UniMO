using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Scream.UniMO.Editor
{
    public class GetInstanceID : MonoBehaviour
    {
        [SerializeField] Object _object;
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
