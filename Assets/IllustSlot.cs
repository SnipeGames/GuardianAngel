using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustSlot : MonoBehaviour
{
    public string id;
    public Image illust;
    public Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Create(string key)
    {
        id = key;
        illust.sprite = Resources.Load<Sprite>(id);
        illust.SetNativeSize();
    }

    public void SetSpeaker()
    {
        illust.color = Color.white;
    }

    public void NotSpeaker()
    {
        illust.color = Color.gray;
    }
}
