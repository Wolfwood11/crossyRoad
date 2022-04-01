using System.Collections.Generic;
using Base;
using UnityEditor;
using UnityEngine;
using World;

namespace Editor
{
    [CustomEditor(typeof(BaseWorldItem))]
    public class TerrainGeneratorEditor : UnityEditor.Editor
    {
        private int _lastSize = -1;
        public override void OnInspectorGUI()
        {
            var baseWorldItem = target as BaseWorldItem;
            
            if (baseWorldItem == null)
            {
                return;
            }
            
            var gameObject = baseWorldItem.gameObject;

            if (GUILayout.Button("UpdatePrefab") && _lastSize != baseWorldItem.Size)
            {
                _lastSize = baseWorldItem.Size;
                
                while (gameObject.transform.childCount > 0)
                {
                    DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
                }

                for(int i = 0; i < _lastSize; i++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    var meshRenderer = cube.GetComponent<MeshRenderer>();
                    
                    meshRenderer.material = baseWorldItem.Material;
                    
                    cube.transform.position = new Vector3(0, 0, i);
                    cube.transform.localScale = new Vector3(50, 1, 1);
                    cube.transform.parent = baseWorldItem.transform;

                    EditorUtility.SetDirty(cube);
                }
                EditorUtility.SetDirty(gameObject);
            }

            base.OnInspectorGUI();
        }
    }
}