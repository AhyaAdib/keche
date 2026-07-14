using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class userInfo{
    public string username;
    public Sprite userAvatar;
}

[System.Serializable]
public class conversation{
    public string ConvDes;
    public int personAvatarInt;
    public LR screenPosition;
    [TextArea]
    public List<string> chat = new List<string>();
}

[CreateAssetMenu(fileName = "New Dialog", menuName = "dialog/Dialog")]
public class DialogSO : ScriptableObject
{
    public List<userInfo> conversationAvatar = new List<userInfo>();
    [Header("_______________________________________")]
    public List<conversation> fullConveration = new List<conversation>();
    [Header("After conversation")]
    public bool askQuestion;
    public int Question;
    [Header("Alter tree start")]
    public bool AlterStart;
    public int StartInt;
    public bool isStartQuestion;
    [Space(30)]
    public bool removeNPC;



}
