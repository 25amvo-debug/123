using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseController : MonoBehaviour
{
    private MainController mainController;
    DatabaseReference db;
    private List<TestData> testsList = new List<TestData>();

    [System.Serializable]
    public class TestData
    {
        public string[] Answers;
        public string BaseName;
        public int dificalt;
        public int testType;
        public int trueAnswer;
    }

    private void Awake()
    {
        db = FirebaseDatabase.DefaultInstance.RootReference;
        mainController = GetComponent<MainController>();
        CheckDatabase();
    }

    public void CheckDatabase()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("/")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    int dbVersion = int.Parse(snapshot.Child("version").Value.ToString());
                    if (dbVersion != mainController.dataStorage.versionTests)
                    {
                        testsList.Clear();
                        DataSnapshot testsSnapshot = snapshot.Child("items");
                        foreach (var child in testsSnapshot.Children)
                        {
                            string json = child.GetRawJsonValue();
                            TestData test = JsonUtility.FromJson<TestData>(json);
                            testsList.Add(test);
                        }
                        mainController.Tests = new TestsSC[testsList.Count];
                        for (int i = 0; i < testsList.Count; i++)
                        {
                            TestsSC a = new TestsSC();
                            a.trueAnswer = testsList[i].trueAnswer;
                            a.Answers = testsList[i].Answers;
                            a.BaseName = testsList[i].BaseName;
                            a.dificalt = testsList[i].dificalt;
                            a.testType = (MainController.Types) testsList[i].testType;
                            mainController.Tests[i] = a;         
                        }
                        mainController.SetTestData(false);
                        mainController.dataStorage.versionTests = dbVersion;
                        mainController.dataStorage.testsSC = mainController.Tests;
                        mainController.dataStorage.SaveToJson();
                        //Debug.Log("Застаріло");
                    }
                    else
                    {
                        mainController.Tests = mainController.dataStorage.testsSC;
                        mainController.SetTestData(true);
                        //Debug.Log("Все впорядку");
                    }
                }
            });
    }
}