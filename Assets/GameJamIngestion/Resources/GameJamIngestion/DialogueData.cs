using UnityEngine;

namespace GameJamIngestion.Resources.GameJamIngestion
{
    public struct DialogueData
    {
        public Vector3 position;
        public string dialogue;
        public float timeToDisplay;

        public static DialogueData CreateFromJson(string json)
        {
            return JsonUtility.FromJson<DialogueData>(json);
        }
    }
}
