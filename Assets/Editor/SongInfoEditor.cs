using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SongInfoEditor : MonoBehaviour
{
    [MenuItem("GGJ/Create song info")]
    public static void CreateSongInfo()
    {
        SongInfo asset = new SongInfo();  //scriptable object
        asset.bpm = 120f;
        asset.songStartTime = 0f;
        AssetDatabase.CreateAsset(asset, "Assets/Songs/info_SONGNAME.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
