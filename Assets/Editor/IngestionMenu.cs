using System.Collections.Generic;
using System.Linq;
using GameJamIngestion;
using NUnit.Framework;
using Platformer.Mechanics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Editor
{
    public class IngestionMenu : MonoBehaviour
    {
        [MenuItem("Ingestion/Sounds/Ingest All Sounds")]
        static void IngestSounds()
        {
            IngestMusicTrack();
        }

        [MenuItem("Ingestion/Sounds/Ingest Music")]
        static void IngestMusicTrack()
        {
            AudioClip musicLoaded = Resources.Load<AudioClip>("GameJamRaw/Sounds/music");
            if (musicLoaded != null)
            {
                GameObject.Find("GameController").GetComponent<AudioSource>().clip = musicLoaded;
            }
            else
            {
                logIngestionFailure("Music");
            }
        }
        
        [MenuItem("Ingestion/Gameplay/Ingest All Gameplay Data")]
        static void IngestGameplayData()
        {
            IngestPlayerData();
        }

        [MenuItem("Ingestion/Gameplay/Ingest Player Capabilities")]
        static void IngestPlayerData()
        {
            TextAsset textLoaded = Resources.Load<TextAsset>("GameJamRaw/Gameplay/playerstats");
            if (textLoaded != null)
            {
                ApplyPlayerCapabilities(PlayerCapabilities.CreateFromJson(textLoaded.text));
            }
            else
            {
                logIngestionFailure("Player capabilities");
            }
        }
        
        [MenuItem("Ingestion/Character Art/Ingest Player Move Art")]
        static void IngestPlayerMoveAnim()
        {
            List<Sprite> movementSpritesLoaded = Resources.LoadAll<Sprite>("GameJamRaw/Images/Player/Move").ToList();
            if (movementSpritesLoaded != null && movementSpritesLoaded.Count > 0)
            {
                OverridePlayerMovementAnimation(movementSpritesLoaded);
            }
            else
            {
                logIngestionFailure("Player movement animation");
            }
        }

        private static void OverridePlayerMovementAnimation(List<Sprite> movementSpritesLoaded)
        {
            Animator playerAnimator = GetPlayerAnimator();
            AnimatorOverrideController movementOverride =
                new AnimatorOverrideController(playerAnimator.runtimeAnimatorController);
            List<KeyValuePair<AnimationClip, AnimationClip>> overrides =
                new List<KeyValuePair<AnimationClip, AnimationClip>>();
            overrides.Add(playerAnimator.);
            movementOverride.ApplyOverrides(overrides);
            playerAnimator.runtimeAnimatorController = movementOverride;
        }

        private static Animator GetPlayerAnimator()
        {
            return GameObject.Find("Player").GetComponent<Animator>();
        }
        
        private static void ApplyPlayerCapabilities(PlayerCapabilities capabilities)
        {
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.maxSpeed = capabilities.movementSpeed;
            player.jumpTakeOffSpeed = capabilities.jumpHeight;
            player.gravityModifier = capabilities.gravityMultiplier;
            player.GetComponent<Health>().maxHP = capabilities.hitPoints;
        }

        [MenuItem("Ingestion/Environment Art/Ingest Far Background")]
        static void IngestFarBackgroundSprite()
        {
            Sprite imageLoaded = Resources.Load<Sprite>("GameJamRaw/Images/farbackground");
            if (imageLoaded != null)
            {
                Debug.Log("Warning: Far Background ingestion not yet implemented");
            }
            else
            {
                logIngestionFailure("Far Background");
            }
        }

        private static void logIngestionFailure(string assetDescription)
        {
            Debug.LogWarning($"Loaded null {assetDescription} image from jam assets; leaving default {assetDescription} enabled");
        }

        /*
    private static void ReverseClip()
    {
        string directoryPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(Selection.activeObject));
        string fileName = Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject));
        string fileExtension = Path.GetExtension(AssetDatabase.GetAssetPath(Selection.activeObject));
        fileName = fileName.Split('.')[0];
        string copiedFilePath = directoryPath + Path.DirectorySeparatorChar + fileName + "_Reversed" + fileExtension;
        var clip = GetSelectedClip();
 
        AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(Selection.activeObject), copiedFilePath);
 
        clip  = (AnimationClip)AssetDatabase.LoadAssetAtPath(copiedFilePath, typeof(AnimationClip));
 
        if (clip == null)
            return;
        float clipLength = clip.length;
        var curves = AnimationUtility.GetAllCurves(clip, true);
        clip.ClearCurves();
        foreach (AnimationClipCurveData curve in curves)
        {
            var keys = curve.curve.keys;
            int keyCount = keys.Length;
            var postWrapmode = curve.curve.postWrapMode;
            curve.curve.postWrapMode = curve.curve.preWrapMode;
            curve.curve.preWrapMode = postWrapmode;
            for (int i = 0; i < keyCount; i++)
            {
                Keyframe K = keys[i];
                K.time = clipLength - K.time;
                var tmp = -K.inTangent;
                K.inTangent = -K.outTangent;
                K.outTangent = tmp;
                keys[i] = K;
            }
            curve.curve.keys = keys;
            clip.SetCurve(curve.path, curve.type, curve.propertyName, curve.curve);
        }
        var events = AnimationUtility.GetAnimationEvents(clip);
        if (events.Length > 0)
        {
            for (int i = 0; i < events.Length; i++)
            {
                events[i].time = clipLength - events[i].time;
            }
            AnimationUtility.SetAnimationEvents(clip, events);
        }
    }
    
    static bool ReverseClipValidation()
    {
        return Selection.activeObject.GetType() == typeof(AnimationClip);
    }
 
    public static AnimationClip GetSelectedClip()
    {
        var clips = Selection.GetFiltered(typeof(AnimationClip), SelectionMode.Assets);
        if (clips.Length > 0)
        {
            return clips[0] as AnimationClip;
        }
        return null;
    }
    //for reference only
    */
    }
}
