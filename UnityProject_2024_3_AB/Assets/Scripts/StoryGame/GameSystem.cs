using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

namespace STORYGAME
{


#if UNITY_EDITOR
    [CustomEditor(typeof(GameSystem))]
    public class GameSystemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameSystem gameSystem = (GameSystem)target;

            //Reset Story Models 버튼 생성
            if(GUILayout.Button("Reset Story Modes"))
            {
                gameSystem.ResetStroyModles();
            }
        }
    }

#endif
    public class GameSystem : MonoBehaviour
    {
        public static GameSystem instance;

        private void Awake()
        {
            instance = this;
        }

        public enum GAMESTATE
        {
            STORYSHOW,
            WAITSELECT,
            STORYEND,
            BATTLEMODE,
            BATTLEDONE,
            SHOPMODE,
            ENDMODE,
        }

        public GAMESTATE currentState;
        public StoryTableObject[] storyModels;
        public int currentStoryInex = 1;

        public StoryTableObject[] storyModles;
#if UNITY_EDITOR
        [ContextMenu("Reset Story Models")]
        public void ResetStroyModles()
        {
            storyModles = Resources.LoadAll<StoryTableObject>("");
        }
#endif
    }

}