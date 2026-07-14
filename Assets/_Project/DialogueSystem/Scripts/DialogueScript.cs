using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum LR { Left, Right }

[System.Serializable]
public class Option
{
    public GameObject answerObj;
    public GameObject answerBoxText;
}
public class DialogueScript : MonoBehaviour
{
    [Header("The Dialog")]
    public GameObject dialogTextArea;
    public GameObject textObj;
    [Header("The Question")]
    public GameObject dialogQuestArea;
    public GameObject questObj;
    public List<Option> options = new List<Option>();
    [Header("Other Stuff")]
    public GameObject nameObj;
    public GameObject dialogHolder;
    public GameObject dialogOpener;
    public GameObject leftAvatar;
    public GameObject rightAvatar;
    [Header("Conversation tree")]
    public ConversationSO convTree;

    private TextMeshProUGUI textText;
    private TextMeshProUGUI  questionText;
    private TextMeshProUGUI  answerText;
    private TextMeshProUGUI nameText;
    public QuestionSO currQuestion;
    private DialogSO currConversation;
    private int convStart;
    private int convAdvance;
    private int convMax;
    private int dialogAdvance;
    private int dialogMax;
    private int avatarInt;
    private RectTransform nameBoxRect;
    public Animator dialogAnim;
    public Image avatarImageLeft, avatarImageRight;
    

    void Awake()
    {
        avatarImageLeft = leftAvatar.GetComponent<Image>();
        avatarImageRight = rightAvatar.GetComponent<Image>();
        nameBoxRect = nameObj.GetComponent<RectTransform>();
    }
    public void ExitDialog()
    {
        dialogAnim.SetBool("Close", true);
        StartCoroutine(DelayExit());
    }

    IEnumerator DelayExit()
    {
        yield return new WaitForSeconds(.8f);
        dialogTextArea.SetActive(false);
        dialogQuestArea.SetActive(false);
        dialogHolder.SetActive(false);
    }

    public void openDialog(int convStartPoint)
    {
        dialogHolder.SetActive(true);
        dialogOpener.SetActive(true);
        conv(convStartPoint);
    }

    void removeNPC(bool state)
    {
        convTree.inScene = state;
    }

    void conv(int convStartPoint)
    {
        // convStart = convTree.convStartPoint;
        convStart = convStartPoint;
        textText = textObj.GetComponent<TextMeshProUGUI>();
        nameText = nameObj.GetComponentInChildren<TextMeshProUGUI>();
        if(convTree.isStartQuestion)
        {
            loadQuestion(convStart);
            QAvatar();
            askQuestion();
        }
        else {
            loadDialog(convStart);
            CAvatar();
            PrintChat();
        }
    }

    public void loadDialog(int nextConv)
    {
        dialogTextArea.SetActive(true);
        dialogQuestArea.SetActive(false);
        currConversation = convTree.conversations[nextConv];
        convAdvance = 0;
        dialogAdvance = 0;
        convMax = currConversation
                    .fullConveration.Count -1;
        dialogMax = currConversation
                        .fullConveration[convAdvance]
                        .chat.Count -1;
    }

    void CAvatar()
    {
        Vector2 newNameRect = nameBoxRect.anchoredPosition;
        newNameRect.x *= -1;
        nameBoxRect.anchoredPosition = newNameRect;

        avatarInt = currConversation
                    .fullConveration[convAdvance].personAvatarInt;
        nameText.text = currConversation
                    .conversationAvatar[avatarInt].username;
        if(currConversation.fullConveration[convAdvance].screenPosition == LR.Left)
        {
            Color colorL = avatarImageLeft.color;
            colorL.a = 1f;
            avatarImageLeft.color = colorL;

            Color colorR = avatarImageRight.color;
            colorR.a = .3f;
            avatarImageRight.color = colorR;

            // leftAvatar.SetActive(true);
            // rightAvatar.SetActive(false);
            Image AvatarImage = leftAvatar.GetComponent<Image>();
            AvatarImage.sprite = currConversation
                                    .conversationAvatar[avatarInt].userAvatar;
        } else {
            Color colorR = avatarImageRight.color;
            colorR.a = 1f;
            avatarImageRight.color = colorR;

            Color colorL = avatarImageLeft.color;
            colorL.a = .3f;
            avatarImageLeft.color = colorL;
            // rightAvatar.SetActive(true);
            // leftAvatar.SetActive(false);
            Image AvatarImage = rightAvatar.GetComponent<Image>();
            AvatarImage.sprite = currConversation
                                    .conversationAvatar[avatarInt].userAvatar;
        }
    }

    void PrintChat()
    {
        textText.text = currConversation
                            .fullConveration[convAdvance]
                            .chat[dialogAdvance];

    }

    public void nextBtn()
    {
        if(convAdvance == convMax &&
        dialogAdvance == dialogMax)
        {
            if(currConversation.AlterStart)
            {
                convTree.convStartPoint = currConversation.StartInt;
                convTree.isStartQuestion = currConversation.isStartQuestion;
            }
            if(currConversation.askQuestion)
            {
                loadQuestion(currConversation.Question);
                QAvatar();
                askQuestion();
                return;
            } else {
                removeNPC(currConversation.removeNPC);
                ExitDialog();
                return;
            }
        }

        #region Advance Conversation
        if(dialogAdvance == dialogMax)
        {
            dialogAdvance =  0;
            convAdvance++;
            dialogMax = currConversation
                            .fullConveration[convAdvance]
                            .chat.Count -1;
            CAvatar();
            PrintChat();
        } else {
            dialogAdvance += 1;
            PrintChat();
        }
        #endregion
    }

    public void loadQuestion(int questionInt)
    {
        dialogTextArea.SetActive(false);
        dialogQuestArea.SetActive(true);
        currQuestion = convTree.questions[questionInt];
    }

    void QAvatar()
    {
        nameText.text = currQuestion.questioner;
        if(currQuestion.QuestSide == LR.Left)
        {
            Color colorL = avatarImageLeft.color;
            colorL.a = 1f;
            avatarImageLeft.color = colorL;

            Color colorR = avatarImageRight.color;
            colorR.a = .3f;
            avatarImageRight.color = colorR;

            // leftAvatar.SetActive(true); // hapus aja
            // rightAvatar.SetActive(false);
            Image AvatarImage = leftAvatar.GetComponent<Image>();
            AvatarImage.sprite = currQuestion.QuestImage;
        } else {
            Color colorR = avatarImageRight.color;
            colorR.a = 1f;
            avatarImageRight.color = colorR;

            Color colorL = avatarImageLeft.color;
            colorL.a = .3f;
            avatarImageLeft.color = colorL;

            // rightAvatar.SetActive(true);
            // leftAvatar.SetActive(false);
            Image AvatarImage = rightAvatar.GetComponent<Image>();
            AvatarImage.sprite = currQuestion.QuestImage;
        }
    }

    void askQuestion()
    {
        dialogQuestArea.SetActive(true);
        dialogTextArea.SetActive(false);
        questionText = questObj.GetComponent<TextMeshProUGUI>();
        questionText.text = currQuestion.questions;
        for(int i = 0; i < currQuestion.options.Count; i++)
        {
            if(currQuestion.options[i].optionText != null)
            {
                options[i].answerObj.SetActive(true);
                answerText = options[i].answerBoxText.GetComponent<TextMeshProUGUI>();
                answerText.text = currQuestion.options[i].optionText;
            }        
        }
    }

    public void questionClick(int questionInt)
    {
        options[0].answerBoxText.SetActive(false);
        options[1].answerBoxText.SetActive(false);
        options[2].answerBoxText.SetActive(false);
        options[3].answerBoxText.SetActive(false);
        if(currQuestion.options[questionInt].AlterStart)
        {
            convTree.convStartPoint = currQuestion.options[questionInt].startInt;
            convTree.isStartQuestion = currQuestion.options[questionInt].isStartQuestion;
        } 
        if(currQuestion.options[questionInt].leadSomewhere)
        {
            int goingWhere = currQuestion.options[questionInt].leadToTreeInt;
            if(currQuestion.options[questionInt].isLeadQuestion)
            {
                loadQuestion(goingWhere);
                QAvatar();
                askQuestion();
            } else {
                loadDialog(goingWhere);
                CAvatar();
                PrintChat();

            }
        } else {
            removeNPC(!currQuestion.options[questionInt].removeNPC);
            ExitDialog();
        }
    }
}
