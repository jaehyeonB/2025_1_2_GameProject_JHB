using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : InteractableObject
{
    [Header("µ¿Àü ¼³Á¤")]
    public int coinValue = 10;
    public string questTag = "Coin";

    protected override void Start()
    {
        base.Start();
        objectName = "µ¿Àü";
        interactionText = "[E] µ¿Àü È¹µæ";
        interactionType = InteractionType.Item;
    }

    protected override void CollectionItem()
    {
        if(QuestManager.Instance != null)
        {
            QuestManager.Instance.AddCollectProgress(questTag);
        }

        AchievementManager.instance?.UpdateProgress(AchievementType.CollectCoins, coinValue);

        transform.Rotate(Vector3.up * 360f);
        Destroy(gameObject, 0.5f);
    }
}
