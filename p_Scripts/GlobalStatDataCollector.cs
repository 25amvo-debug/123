using UnityEngine;
using System;

public class GlobalStatDataCollector : MonoBehaviour
{
    private void Awake()
    {
        dataStorage.LoadFromJson();
    }

    public DataStorage dataStorage;
    public void ReupdateData(MainController.Types[] types, float[] time, float mark)
    {
        for(int i = 0; i < time.Length; i++)
        {
            int typesNum = (int)types[i];
            if (dataStorage.avaregeTimeCR[typesNum] == 0) { dataStorage.avaregeTime[typesNum] = time[i]; }
            else { dataStorage.avaregeTime[typesNum] = (dataStorage.avaregeTime[typesNum] * dataStorage.avaregeTimeCR[typesNum] + time[i]) / (dataStorage.avaregeTimeCR[typesNum] + 1); }
            dataStorage.avaregeTimeCR[typesNum]++;
        }

        if(dataStorage.updateTime == 0) { dataStorage.avarageMark = mark; }
        else { dataStorage.avarageMark = (dataStorage.avarageMark * dataStorage.updateTime + mark) / (dataStorage.updateTime + 1)  ; }
        dataStorage.updateTime++;
    }
}
