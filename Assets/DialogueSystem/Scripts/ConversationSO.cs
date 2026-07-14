using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "dialog/Conversation")]
public class ConversationSO : ScriptableObject
{
    public bool inScene = true;
    // public Vector2 SceneLocation;
    public int convStartPoint;
    public bool isStartQuestion;
    [Space(30)]
    public List<DialogSO> conversations = new List<DialogSO>();
    public List<QuestionSO> questions = new List<QuestionSO>();

}
