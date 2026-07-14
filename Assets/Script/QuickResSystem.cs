
// ============================= BACKUP ======================================

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;
// using TMPro;

// public class QuickResSystem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
// {
//     public static QuickResSystem instance;
//     public RectTransform rectTransform; 
//     [SerializeField] private Canvas canvas;
//     [SerializeField] private LineRenderer LR;
//     public CanvasGroup canvasGroup;
//     public GameObject dollObj;
//     public GameObject dashTarget;
//     GameObject player;
//     public float respondTime;
//     public bool canRes, isStayInPoint;
//     public GameObject pointObj;
//     public TextMeshProUGUI timerText;
//     public Slider timeSlider;
//     public Slider pointSlider;
//     public dashTarget scriptPoint;
//     public GameObject quickResObj, gameOverScene, guardPointer;
//     public guardDetector detector;  
//     public float distPlayerGuard;
//     public Transform handPos;

//     [Header("AvoidSystem")]
//     // public int changeLife = 2;
//     public GameObject[] dodgeColl;
//     public bool avoiding;


//     private void Awake()
//     {
//         instance = this;
//         player = GameObject.FindGameObjectWithTag("Player");
//         LR.positionCount = 2;
//         LR.SetPosition(1, Vector2.zero);
//         LR.enabled = false;

//         rectTransform = GetComponent<RectTransform>();
//         canvasGroup = GetComponent<CanvasGroup>();
//     }

//     private void OnEnable()
//     {
//         Time.timeScale = .1f; //0.25, 0.3 0.5   
//         timeSlider.gameObject.SetActive(false);
//         pointSlider.gameObject.SetActive(false);
//         inGameSceneManager.instance.isStarting = true;
//         StartCoroutine(StartRes());
//         respondTime -= .05f;
//     }
    
//     private void Update() {
//         Debug.LogWarning(Time.timeScale);
//         guardPointer = detector.Target;
//         if (guardPointer != null)
//         {
//             GameObject handGuardDetector = FindChild(guardPointer);
//             if (handGuardDetector != null)
//             {
//                 handPos = handGuardDetector.transform;
//                 distPlayerGuard = Mathf.Abs(Vector2.Distance(handPos.position, player.transform.position));
//             }
//         }

//         if(!avoiding)
//         {
//             foreach(GameObject dodge in dodgeColl)
//             {
//                 dodge.SetActive(false);
//             }
//         }

        
//         isStayInPoint = scriptPoint._isStay;
//     }

//     GameObject FindChild(GameObject parent)
//     {
//         foreach (Transform child in parent.transform)
//         {
//             if (child.GetComponent<RightHandScript>() != null)
//             {
//                 return child.gameObject;
//             }

//             GameObject result = FindChild(child.gameObject);
//             if (result != null)
//             {
//                 return result;
//             }
//         }

//         return null; 
//     }

//     public void OnPointerDown(PointerEventData evenData)
//     {
//         Debug.Log("OnPointerDown");
//     }

//     public void OnBeginDrag(PointerEventData evenData)
//     {
//         canvasGroup.blocksRaycasts  = false;
//         canvasGroup.alpha = .6f;
//         LR.enabled = true;
//         // inGameSceneManager.instance.isStarting = true;
       
//     }

//     public void OnDrag(PointerEventData evenData)
//     {

//         LR.SetPosition(0, player.transform.position);

//         rectTransform.anchoredPosition += evenData.delta / canvas.scaleFactor;

//         Vector3 worldPosition;
//         RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, evenData.position, canvas.worldCamera, out worldPosition);

//         // Set the second position of the LineRenderer to the touch position
//         LR.SetPosition(1, worldPosition);

//         Vector3 direction = worldPosition - player.transform.position;
//         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;

//         dashTarget.transform.position = worldPosition;

//         dollObj.transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);


//     }

//     public void OnEndDrag(PointerEventData evenData)
//     {
//         LR.SetPosition(0, new Vector2(1000, 1000));
//         LR.SetPosition(1, new Vector2(1000, 1000));

//         DashPointManager dashPointManager = FindObjectOfType<DashPointManager>();
//         dashPointManager.SetPosDashTarget();   

//         inGameSceneManager.instance.isStarting = false;

//         canvasGroup.blocksRaycasts = true;
//         canvasGroup.alpha = 0f;
        
//         if(canRes)
//         {
//             StopCoroutine(StartRes());
//             StartCoroutine(DoDashRes(evenData));
//         }
//         // if(isStayInPoint && canRes)
//         // {
//         //     StopCoroutine(StartRes());
//         //     StartCoroutine(DoDashRes(evenData));
//         // }
//         // else  Fail();
//         Fail();
//     }

//     IEnumerator StartRes()
//     {
//         timeSlider.gameObject.SetActive(true);
//         timeSlider.maxValue = respondTime;
//         pointSlider.gameObject.SetActive(true);
//         pointSlider.maxValue = respondTime;

//         canRes = true;
//         float timer = 0;
//         while(timer < respondTime)
//         {
//             // timeSlider.value = timer;
//             timeSlider.value = respondTime - timer;
//             pointSlider.value = respondTime - timer;
//             timerText.text = (respondTime - timer).ToString("F2");
//             timer += Time.deltaTime;
//             yield return new WaitForSeconds(Time.deltaTime);
//         }
//         if(!avoiding)
//             Fail(); 
//     }

//     void Fail()
//     {
//         if(inGameSceneManager.instance.changeLife > 1)
//         {
//             avoiding = true;
//             foreach(GameObject dodge in dodgeColl)
//             {
//                 dodge.SetActive(true);
//             }
//             StartCoroutine(AvoidingProcess());
//             return;
//         } else {
//             inGameSceneManager.instance.changeLife--;
//         }
//         Time.timeScale = 1f;
//         StopCoroutine(StartRes());

//         timeSlider.gameObject.SetActive(false);
        
//         pointSlider.gameObject.SetActive(false);

//         skillPlayerUI.instance.isDashing = false;
        
//         rectTransform.anchoredPosition = Vector2.zero;
//         LR.enabled = false;
//         dashTarget.transform.position = new Vector2(0, 2000f);
//         timeSlider.gameObject.SetActive(false);
//         pointSlider.gameObject.SetActive(false);

//         inGameSceneManager.instance.isStarting = false;

//         StartCoroutine(GameOver());
//         quickResObj.SetActive(false);
        
//         canRes = false;

//     }
//     IEnumerator AvoidingProcess()
//     {
//         Time.timeScale = 1f;
//         yield return new WaitForSeconds(.1f);
//         foreach(GameObject dodge in dodgeColl)
//         {
//             dodge.SetActive(false);
//         }
//         inGameSceneManager.instance.changeLife--;

//         StopCoroutine(StartRes());

//         timeSlider.gameObject.SetActive(false);
        
//         pointSlider.gameObject.SetActive(false);

//         skillPlayerUI.instance.isDashing = false;
        
//         rectTransform.anchoredPosition = Vector2.zero;
//         LR.enabled = false;
//         dashTarget.transform.position = new Vector2(0, 2000f);
//         timeSlider.gameObject.SetActive(false);
//         pointSlider.gameObject.SetActive(false);

//         inGameSceneManager.instance.isStarting = false;

//         // StartCoroutine(GameOver());
//         yield return new WaitForSeconds(.1f);
//         quickResObj.SetActive(false);
//         avoiding = false;
        
//         canRes = false;
//     }

//     IEnumerator GameOver()
//     {
//         yield return new WaitForSeconds(1f);
//         // Debug.LogWarning("Faillll");
//         gameOverScene.SetActive(true);
        
//         quickResObj.SetActive(false);
//     }

//     IEnumerator DoDashRes(PointerEventData evenData)
//     {
//         Time.timeScale = 1f;
//         Vector3 worldPosition;
//         RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, evenData.position, canvas.worldCamera, out worldPosition);

//         Vector3 direction = worldPosition - player.transform.position;
//         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
//         player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

//         skillPlayerUI.instance.isDashing = true;

//         // Debug.LogWarning("dgwgqjjgsjhxcvsghg");
//         Transform target = dashTarget.transform;
//         target.position = worldPosition;
//         if(target != null)
//         {
//             float speed = 100f; // Kecepatan per frame
//             while (Vector2.Distance(player.transform.position, target.position) > 0.1f)
//             {
//                 player.transform.position = Vector2.MoveTowards(player.transform.position, target.position, speed * Time.deltaTime);
//                 yield return null; // Tunggu frame berikutnya
//             }
//         }
//         skillPlayerUI.instance.isDashing = false;
        
//         rectTransform.anchoredPosition = Vector2.zero;
//         LR.enabled = false;
//         dashTarget.transform.position = new Vector2(0, 2000f);
//         timeSlider.gameObject.SetActive(false);
//         pointSlider.gameObject.SetActive(false);

//         inGameSceneManager.instance.isStarting = false;
//         StopCoroutine(StartRes());
//         quickResObj.SetActive(false);

//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class QuickResSystem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static QuickResSystem instance;

    [Header("UI & Canvas")]
    public RectTransform rectTransform; 
    [SerializeField] private Canvas canvas;
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI timerText;
    public Slider timeSlider;
    public Slider pointSlider;
    public GameObject quickResObj, gameOverScene;

    [Header("Dash System")]
    [SerializeField] private LineRenderer LR;  // 🔥 dipakai buat efek dash
    [SerializeField] private TrailRenderer TRPlayer;  // 🔥 dipakai buat efek dash
    private GameObject player;                 // 🔥 player akan dicari lewat tag
    public GameObject dollObj;
    public GameObject dashTarget;              // 🔥 target dash (di inspector)
    public dashTarget scriptPoint;
    public guardDetector detector;  
    public Transform handPos;

    [Header("Res System")]
    public float respondTime;
    public bool canRes, isStayInPoint;
    public float distPlayerGuard;
    public GameObject pointObj;
    public GameObject guardPointer;

    [Header("Avoid System")]
    public GameObject[] dodgeColl;
    public bool avoiding;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player"); // 🔥 pastikan Player punya tag "Player"
        LR.positionCount = 2;
        LR.SetPosition(1, Vector2.zero);
        LR.enabled = false;

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        Time.timeScale = .08f; // slow motion ketika masuk quick res mode
        timeSlider.gameObject.SetActive(false);
        pointSlider.gameObject.SetActive(false);
        inGameSceneManager.instance.isStarting = true;
        StartCoroutine(StartRes());
        respondTime -= .045f;
    }
    
    private void Update() {
        guardPointer = detector.Target;
        if (guardPointer != null)
        {
            GameObject handGuardDetector = FindChild(guardPointer);
            if (handGuardDetector != null)
            {
                handPos = handGuardDetector.transform;
                distPlayerGuard = Mathf.Abs(Vector2.Distance(handPos.position, player.transform.position));
            }
        }

        if(!avoiding)
        {
            foreach(GameObject dodge in dodgeColl)
            {
                dodge.SetActive(false);
            }
        }
        
        isStayInPoint = scriptPoint._isStay;
    }

    GameObject FindChild(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.GetComponent<RightHandScript>() != null)
            {
                return child.gameObject;
            }

            GameObject result = FindChild(child.gameObject);
            if (result != null)
            {
                return result;
            }
        }

        return null; 
    }

    public void OnPointerDown(PointerEventData evenData) { }

    public void OnBeginDrag(PointerEventData evenData)
    {
        canvasGroup.blocksRaycasts  = false;
        canvasGroup.alpha = .6f;
        LR.enabled = true;
    }

    public void OnDrag(PointerEventData evenData)
    {
        LR.SetPosition(0, player.transform.position);

        rectTransform.anchoredPosition += evenData.delta / canvas.scaleFactor;

        Vector3 worldPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, evenData.position, canvas.worldCamera, out worldPosition);

        LR.SetPosition(1, worldPosition);

        Vector3 direction = worldPosition - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;

        dashTarget.transform.position = worldPosition;
        dollObj.transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
    }

    public void OnEndDrag(PointerEventData evenData)
    {
        LR.SetPosition(0, new Vector2(1000, 1000));
        LR.SetPosition(1, new Vector2(1000, 1000));

        DashPointManager dashPointManager = FindObjectOfType<DashPointManager>();
        dashPointManager.SetPosDashTarget();   

        inGameSceneManager.instance.isStarting = false;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 0f;
        
        // if(canRes)
        if(isStayInPoint && canRes)
        {
            StopCoroutine(StartRes());
            StartCoroutine(DoDashRes(evenData));
        }
        else
        {
            Fail();
        }
    }

    IEnumerator StartRes()
    {
        timeSlider.gameObject.SetActive(true);
        timeSlider.maxValue = respondTime;
        pointSlider.gameObject.SetActive(true);
        pointSlider.maxValue = respondTime;

        canRes = true;
        float timer = 0;
        while(timer < respondTime)
        {
            timeSlider.value = respondTime - timer;
            pointSlider.value = respondTime - timer;
            timerText.text = (respondTime - timer).ToString("F2");
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if(!avoiding)
            Fail(); 
    }

    void Fail()
    {
        if(inGameSceneManager.instance.changeLife > 1)
        {
            avoiding = true;
            foreach(GameObject dodge in dodgeColl)
            {
                dodge.SetActive(true);
            }
            StartCoroutine(AvoidingProcess());
            return;
        } 
        else 
        {
            inGameSceneManager.instance.changeLife--;
        }

        Time.timeScale = 1f;
        StopCoroutine(StartRes());

        timeSlider.gameObject.SetActive(false);
        pointSlider.gameObject.SetActive(false);

        skillPlayerUI.instance.isDashing = false;
        rectTransform.anchoredPosition = Vector2.zero;
        LR.enabled = false;
        dashTarget.transform.position = new Vector2(0, 2000f);
        inGameSceneManager.instance.isStarting = false;

        StartCoroutine(GameOver());
        quickResObj.SetActive(false);
        canRes = false;
    }

    IEnumerator AvoidingProcess()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(.1f);
        foreach(GameObject dodge in dodgeColl)
        {
            dodge.SetActive(false);
        }
        inGameSceneManager.instance.changeLife--;

        StopCoroutine(StartRes());

        timeSlider.gameObject.SetActive(false);
        pointSlider.gameObject.SetActive(false);

        skillPlayerUI.instance.isDashing = false;
        rectTransform.anchoredPosition = Vector2.zero;
        LR.enabled = false;
        dashTarget.transform.position = new Vector2(0, 2000f);
        inGameSceneManager.instance.isStarting = false;

        yield return new WaitForSeconds(.1f);
        quickResObj.SetActive(false);
        avoiding = false;
        canRes = false;
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        gameOverScene.SetActive(true);
        quickResObj.SetActive(false);
    }

    IEnumerator DoDashRes(PointerEventData evenData)
    {
        Time.timeScale = 1f;

        // Ambil posisi world dari pointer
        Vector3 worldPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.transform as RectTransform,
            evenData.position,
            canvas.worldCamera,
            out worldPosition
        );

        // Set target
        dashTarget.transform.position = worldPosition;

        // Tampilkan efek garis dash
        // LR.SetPosition(0, player.transform.position);
        // LR.SetPosition(1, worldPosition);

        // Tunggu sebentar biar garis kelihatan
        yield return new WaitForSeconds(0.05f);

        // Teleport player langsung (hanya X & Y, Z tetap sama)
        player.transform.position = new Vector3(
            worldPosition.x,
            worldPosition.y,
            player.transform.position.z
        );

        playerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        pm.StartCoroutine(pm.ShowTrail());

        // Reset state
        skillPlayerUI.instance.isDashing = false;
        rectTransform.anchoredPosition = Vector2.zero;
        dashTarget.transform.position = new Vector2(0, 2000f);
        timeSlider.gameObject.SetActive(false);
        pointSlider.gameObject.SetActive(false);
        inGameSceneManager.instance.isStarting = false;
        StopCoroutine(StartRes());
        quickResObj.SetActive(false);
    }

}