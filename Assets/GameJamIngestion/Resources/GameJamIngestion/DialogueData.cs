using UnityEngine;

namespace GameJamIngestion.Resources.GameJamIngestion
{
    public struct DialogueData
    {
        public Vector3 position;
        public string dialogue;
        public float timeToDisplay;

        public static void printExample()
        {
            DialogueData example = new DialogueData();
            example.position = new Vector3(1, 2, 3);
            example.dialogue = "Dios Mio";
            example.timeToDisplay = 5.5f;
            Debug.Log(JsonUtility.ToJson(example));
        }

        public static DialogueData CreateFromJson(string json)
        {
            return JsonUtility.FromJson<DialogueData>(json);
        }
    }
}
