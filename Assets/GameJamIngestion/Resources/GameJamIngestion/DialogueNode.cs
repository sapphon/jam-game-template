using System;
using TMPro;
using UnityEngine;

namespace GameJamIngestion.Resources.GameJamIngestion
{
    public class DialogueNode : MonoBehaviour
    {
        public float timeToDisplay;
        public float timeTriggered;
        public TextMeshPro textComponent;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && other.gameObject.name == "Player")
            {
                this.timeTriggered = Time.time;
                
            }
        }


        void Update()
        {
            if (Time.time < this.timeTriggered + this.timeToDisplay)
            {
                float currentOpacityTarget =  1 - ((Time.time - this.timeTriggered) / this.timeToDisplay);
                textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b,
                    currentOpacityTarget);
            }
        }
    }
}
