using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestionsOption
{
    public string optionText;
    public bool leadSomewhere;
    public int leadToTreeInt;
    public bool isLeadQuestion;
    [Header("Alter Tree Start")]
    public bool AlterStart;
    public int startInt;
    public  bool isStartQuestion;
    [Space(30)]
    public bool removeNPC;

}
[CreateAssetMenu(fileName = "New Question", menuName = "dialog/Question")]
public class QuestionSO : ScriptableObject
{
    public string questions;
    public string questioner;
    public Sprite QuestImage;
    public LR QuestSide;
    public List<QuestionsOption> options = new List<QuestionsOption>();
}
