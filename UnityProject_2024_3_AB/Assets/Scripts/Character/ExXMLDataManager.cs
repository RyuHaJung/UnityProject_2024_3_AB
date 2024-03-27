using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;                   //������ �а� ���� ���ؼ�
using System.Xml.Serialization;    // XML�� ����ϱ� ���ؼ�

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int playerLevel;
    public List<string> items = new List<string>();
}

public class ExXMLDataManager : MonoBehaviour
{
    string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/playerData.xml";
        Debug.Log(filePath);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            PlayerData playerData = new PlayerData();
            playerData.playerName = "�÷��̾� 1";
            playerData.playerLevel = 1;
            playerData.items.Add("��1");
            playerData.items.Add("����1");
            SaveData(playerData);

        }
        
        if(Input.GetKeyDown(KeyCode.L))
        {
            PlayerData playerData = new PlayerData();

            playerData = LoadData();

            Debug.Log(playerData.playerName);
            Debug.Log(playerData.playerLevel);
            for(int i = 0; i < playerData.items.Count; i++)
            {
                Debug.Log(playerData.items[i]);
            }
        }
    }

    void SaveData(PlayerData data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));    //XML ����ȭ
        FileStream stream = new FileStream(filePath, FileMode.Create);      //���� ���� ��� ����
        serializer.Serialize(stream, data);                                 //���Ͽ� �����͸� �����Ѵ�
        stream.Close();                                                  //�ݵ�� Close ����� �Ѵ�
    }
    PlayerData LoadData()
    {
        if(File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
            FileStream stream = new FileStream(filePath, FileMode.Open);
            PlayerData data = (PlayerData)serializer.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
