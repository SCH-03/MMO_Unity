using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}


public class DataManager 
{
  
    public Dictionary<int,Data.Stat> StatDict { get; private set;} = new Dictionary<int,Data.Stat>();
    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
    }

    private Loader LoadJson<Loader, key, Value>(string path) where Loader : ILoader<key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
