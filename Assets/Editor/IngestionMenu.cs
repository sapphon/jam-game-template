using UnityEditor;
using UnityEngine;

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
        [MenuItem("Ingestion/Environment Art/Ingest Far Background")]
        static void IngestFarBackgroundSprite()
        {
            Sprite imageLoaded = Resources.Load<Sprite>("GameJamRaw/Images/farbackground");
            if (imageLoaded != null)
            {
            
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
