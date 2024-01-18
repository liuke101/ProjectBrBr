using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using System.IO;
using UnityEditor;
using System.Text;

[Serializable]
public class JsonData
{
    public PlayerData playerData;
}
public class DataManager : BaseManager<DataManager>
{
    public Dictionary<string, string> talk = new Dictionary<string, string>();

    public override void OnInit()
    {
        initdic();
        base.OnInit();
    }
    public override void OnDestory()
    {
        base.OnDestory();
    }

    private void initdic()
    {
        string data = Application.dataPath + "/Resources/Datas/PlayerData/talk.txt";
        List<string> list_Get = Read(data);
        foreach (string s in list_Get)
        {
            if(s!=" ")
            {
                string[] arr = s.Split(':');
                try
                {
                    talk.Add(arr[0], arr[1]);
                }
                catch
                {
                    Debug.Log(s);
                }
            }
           
        }
    }

    public List<string> Read(string path)
    {
        StreamReader sr = new StreamReader(path, Encoding.UTF8);
        string line;
        List<string> list = new List<string>();
        while ((line = sr.ReadLine()) != null)
        {
            list.Add(line.ToString());
        }
        return list;
    }

    public JsonData LoadData(string path)
    {
        string pathLoadData = Application.dataPath + path;
        if (!File.Exists(pathLoadData))
        {
            Debug.Log("数据不存在");
            return null;
        }
        string jsonStr = File.ReadAllText(pathLoadData);
        JsonData tempdata = new JsonData();
        tempdata = JsonUtility.FromJson<JsonData>(jsonStr);
        return tempdata;
    }
    public JsonData LoadData()
    {
        JsonData jsondata = LoadData("/Resources/Datas/PlayerData/PlayerData.json");
        return jsondata;
    }

    /// <summary>
    /// 重载方法：SaveData
    /// </summary>
    /// <param name="jsondata">玩家数据</param>
    /// <param name="path">保存的路径（相对路径）</param>
    public void SavaData(string path, JsonData jsondata)
    {
        string pathSavaData = Application.dataPath + path;//获得绝对路径
        if (!File.Exists(pathSavaData))
        {
            Debug.Log("创建成功");
        }
        FileInfo fileInfo = new FileInfo(pathSavaData);
        StreamWriter sw = fileInfo.CreateText();
        string JSON = JsonUtility.ToJson(jsondata);
        sw.WriteLine(JSON);//写入json数据
        sw.Close();
        sw.Dispose();
        Debug.Log("写入成功");
    }
    public void SavaData(JsonData jsondata)
    {
        SavaData("/Resources/Datas/PlayerData/PlayerData.json", jsondata);
    }
}
