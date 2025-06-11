using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BottleDropZone : MonoBehaviour, IDropHandler
{
    public Image bottleImage;
    public Sprite[] bottleSprites;
    private int dropCount = 0;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        if (dropped != null && dropped.GetComponent<DraggableMilk>() != null)
        {
            dropCount++;

            Destroy(dropped); 
            
            if (dropCount <= bottleSprites.Length)
            {
                bottleImage.sprite = bottleSprites[dropCount - 1];
            }

            if (dropCount >= 5)
            {
                PlayerPrefs.SetInt("FaseAtual", 3);
                SceneManager.LoadScene("PrepareBottle");
            }
        }
    }
}

