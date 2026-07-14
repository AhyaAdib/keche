using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageDownloader : MonoBehaviour
{
    public string imageUrl;
    public Image imageUI;

    IEnumerator Start()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        yield return new WaitForSeconds(.4f);
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            
            
            float desiredWidth = imageUI.rectTransform.sizeDelta.x;
            float aspectRatio = (float)texture.width / texture.height;

            // Set the width of the image component and adjust height to maintain aspect ratio
            imageUI.rectTransform.sizeDelta = new Vector2(desiredWidth, desiredWidth / aspectRatio);

            // Assign the sprite to the image component
            imageUI.sprite = sprite;
        }
        else
        {
            Debug.Log("Image download failed: " + request.error);
        }
    }
}
