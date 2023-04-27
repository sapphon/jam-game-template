using UnityEngine;

namespace GameJamIngestion
{
    public struct PlayerCapabilities
    {
        public float movementSpeed;
        public float gravityMultiplier;
        public float jumpHeight;
        public int hitPoints;

        public static PlayerCapabilities CreateFromJson(string json)
        {
            return JsonUtility.FromJson<PlayerCapabilities>(json);
        }
    }
}