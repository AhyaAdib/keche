using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arterfakManager : MonoBehaviour
{
    public dataArterfak arterfak;
    public int level, obtainedArtifact1, obtainedArtifact2;
    public Sprite currentArtifactFragment1, currentArtifactFragment2;
    bool bisaMenjawab;
    
    public brushingQuestion checkAnswer;

    public SpriteRenderer[] artifactFragmentImgs1;
    public SpriteRenderer[] artifactFragmentImgs2;
    SpriteRenderer mySR;

    public pilihSkill playerSkill;
    public Sprite gemSprite;

    public int End;
    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("level", 1);
        obtainedArtifact1 = PlayerPrefs.GetInt("arterfakDidapat", 0);
        End = PlayerPrefs.GetInt("end", 0);
        if(obtainedArtifact1 == 5f)
        {
            obtainedArtifact1 = 0;
            level++;
            obtainedArtifact2 = obtainedArtifact1 + 1;
        } else if(obtainedArtifact1 == 4f)
        {
            // level++;
            obtainedArtifact2 = 0;
        }

        if(End == 0)
        {
            var arterfakUnit1 = arterfak.arterfak1[level - 1];
            currentArtifactFragment1 = arterfakUnit1.pecahanArterfak[obtainedArtifact1];
        }
        
        // mySR = gameObject.GetComponent<SpriteRenderer>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if(checkAnswer.canAnswerBrushQuiz)
            bisaMenjawab = true;

            
        if(playerSkill.SkillFragment )
        {
            bisaMenjawab = false;
            // Debug.LogWarning("Skiil 2 aktif");
            if(obtainedArtifact2 > 4)
            {
                    var arterfakUnit2 = arterfak.arterfak1[level -1];
                // obtainedArtifact2 = 0;
                arterfakUnit2 = arterfak.arterfak1[level];
                currentArtifactFragment2 = gemSprite;
            } else {
                // if(End == 0 && level == 4 && obtainedArtifact1 == 4)
                // {
                //     // arterfakUnit2 = arterfak.arterfak1[level];
                //     artifactFragmentImgs2[0].gameObject.SetActive(true);
                //     // currentArtifactFragment2 = mySR.sprite;
                // } else {}
                 if(End == 0)
                {
                    var arterfakUnit2 = arterfak.arterfak1[level -1];
                    if(obtainedArtifact1 == 4)
                    {

                        arterfakUnit2 = arterfak.arterfak1[level];
                         currentArtifactFragment2 = arterfakUnit2.pecahanArterfak[obtainedArtifact2];
                    } else
                    {

                        currentArtifactFragment2 = arterfakUnit2.pecahanArterfak[obtainedArtifact1 + 1];
                    }
                }
            }

            artifactFragmentImgs2[0].gameObject.SetActive(true);
            if(End == 1 || (End == 0 && level == 4 && obtainedArtifact1 == 4))
            {
                foreach(SpriteRenderer artifactFragmentImg in artifactFragmentImgs2)
                {
                    // artifactFragmentImg.gameObject.SetActive(true);
                    artifactFragmentImg.sprite = gemSprite;
                }
            } else {

                foreach(SpriteRenderer artifactFragmentImg in artifactFragmentImgs2)
                {
                        artifactFragmentImg.sprite = currentArtifactFragment2;
                }
            }
        } else {
            if(artifactFragmentImgs2 != null)
            {
                foreach(SpriteRenderer artifactFragmentImg in artifactFragmentImgs2)
                {
                    artifactFragmentImg.gameObject.SetActive(false);
                }
            }
        }

        if(End == 1)
        {
            foreach(SpriteRenderer artifactFragmentImg in artifactFragmentImgs1)
            {
                // artifactFragmentImg.gameObject.SetActive(true);
                artifactFragmentImg.sprite = gemSprite;
            }
        } else {
            foreach(SpriteRenderer artifactFragmentImg in artifactFragmentImgs1)
            {
                artifactFragmentImg.sprite = currentArtifactFragment1;
            }
        }
    }
}
