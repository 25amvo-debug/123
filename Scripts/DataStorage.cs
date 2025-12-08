using NaughtyAttributes;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "DataStorage", menuName = "Scriptable Objects/DataStorage")]
public class DataStorage : ScriptableObject
{
    public float avarageMark;
    public int updateTime = 0;
    public int LastMark = -1;
    public int versionTests;
    public float[] avaregeTime;
    public int[] avaregeTimeCR;
    public TestsSC[] testsSC;
    public int activeTheme;
    public List<MessagesData> messages = new List<MessagesData>();
    public enum Level {Low, Middle, High}
    public float[] levels;
    private static string FilePath =>
        Path.Combine(Application.persistentDataPath, "_data.json");

    public void LoadFromJson()
    {
        string json = File.ReadAllText(FilePath);
        JsonUtility.FromJsonOverwrite(json, this);
    }
    public void SaveToJson()
    {
        string json = JsonUtility.ToJson(this, prettyPrint: true);
        File.WriteAllText(FilePath, json);
    }
    [Button]
    public void Rest()
    {
        testsSC = new TestsSC[0];
        messages.Clear();
        avarageMark = 0;
        updateTime = 0;
        LastMark = -1;
        versionTests = 0;
        for (int i = 0; i < avaregeTime.Length; i++)
        {
            avaregeTime[i] = 0;
            avaregeTimeCR[i] = 0;
        }
        activeTheme = 1;
        levels = new float[9];
        for(int i = 0; i < levels.Length; i++)
        {
            levels[i] = -0.1f;
        }
        SaveToJson();
    }
}
