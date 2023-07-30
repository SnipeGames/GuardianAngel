using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

[System.Serializable]
public class DialogueEntry
{
    public string name;
    public string content;
    public string expression;
    public string animation;
    public List<string> translations = new List<string>();
}

public class DialogueManager : MonoBehaviour
{
    public TextAsset tsvFile;
    public TextMeshProUGUI dialogueText; // ��ȭ�� ����� Text ������Ʈ
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI playerText;
    public GameObject PlayerTextPannel;
    public Transform IllustPannel;

    public IllustSlot model;
    Animator animator;
    
    float typeSpeed = 0.03f; // Ÿ���� �ӵ�
    public List<DialogueEntry> dialogues = new List<DialogueEntry>();

    public List<IllustSlot> illusts = new List<IllustSlot>();

    private int currentDialogueIndex = 0;
    private bool isDialoguePlaying = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        ParseTSV();
        DisplayDialogue(currentDialogueIndex);
    }


    void ParseTSV()
    {
        string[] records = tsvFile.text.Split('\n');
        for (int i = 1; i < records.Length; i++)
        {
            if (records[i].Trim() == "") continue;
            string[] fields = records[i].Split('\t'); // �� ���ڷ� ����

            if (fields.Length < 4)
            {
                Debug.LogError("Invalid data format in line " + i);
                continue;
            }

            DialogueEntry entry = new DialogueEntry
            {
                name = fields[0],
                expression = fields[1],
                animation = fields[2],
                content = fields[3]
            };

            for (int j = 4; j < fields.Length; j++)
            {
                entry.translations.Add(fields[j]);
            }

            dialogues.Add(entry);
        }
    }

    public void DisplayDialogue(int index)
    {
        if (index < 0 || index >= dialogues.Count)
        {
            Debug.LogError("Invalid dialogue index!");
            return;
        }

        //���� �� ����
        if (dialogues[index].animation == "CREATE")
        {
            var ill = Instantiate(model, IllustPannel);
            ill.Create(dialogues[index].name);
            illusts.Add(ill);
        }

        if (dialogues[index].animation == "EXIT")
        {
            //id�� ã�Ƽ� �����Ѵ�
        }

        //ȭ�ڸ� ã�Ƽ� Ȱ��ȭ�ϱ�
        for (int i = 0; i < illusts.Count; i++)
        {
            if(dialogues[index].name == illusts[i].id)
            {
                illusts[i].SetSpeaker();
                illusts[i].animator.Play(dialogues[index].animation);
            }
            else
                illusts[i].NotSpeaker();
        }


        if (!isDialoguePlaying)
        {
            StartCoroutine(TypeDialogue());
        }
        else
        {
            // ��ȭ�� ��� ���� �� ��ư�� ������ ��ȭ ����� ��� �Ϸ�
            StopAllCoroutines();
            dialogueText.text = dialogues[index].content;
            isDialoguePlaying = false;
        }
    }
    
    IEnumerator TypeDialogue()
    {
        isDialoguePlaying = true;
        DialogueEntry dialogue = dialogues[currentDialogueIndex];

        if (dialogue.name == "P")
        {
            PlayerTextPannel.SetActive(true);
            PlayerTextPannel.transform.localScale = Vector3.zero;
            PlayerTextPannel.transform.DOScale(1, 0.3f);
            playerText.text = dialogue.content;
        }
        else
        {
            nameText.text = Localize.GetLocalizedString("Character", dialogue.name) ;
            dialogueText.text = "";
            for (int i = 0; i < dialogue.content.Length; i++)
            {
                char letter = dialogue.content[i];
                dialogueText.text += letter;
                yield return new WaitForSeconds(typeSpeed);
            }
        }

        isDialoguePlaying = false;
    }

    public void PlayerDialogPressed()
    {
        PlayerTextPannel.transform.localScale = Vector3.one;
        PlayerTextPannel.transform.DOScale(1.1f, 0.1f).onComplete += () =>
        {
            PlayerTextPannel.transform.DOScale(0, 0.2f).onComplete += () =>
            {
                PlayerTextPannel.SetActive(false);
                NextDialogue();
            };
        };
    }



    public void NextDialogue()
    {
        if (!PlayerTextPannel.activeInHierarchy)
        {
            if (isDialoguePlaying)
            {
                DisplayDialogue(currentDialogueIndex);
            }
            else if (currentDialogueIndex < dialogues.Count - 1)
            {
                currentDialogueIndex++;
                DisplayDialogue(currentDialogueIndex);
            }
        }
    }
}