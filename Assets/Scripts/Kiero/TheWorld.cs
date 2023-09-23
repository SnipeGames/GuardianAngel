using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour
{
    public GameObject spritePrefab;
    private List<GameObject> instantiatedSprites = new List<GameObject>();

    public static List<Vector3> clickPositions = new List<Vector3>();

    private void OnEnable()
    {
        Commander.Click += OnMouseClicked;
        Commander.Confirm += Execute;
        clickPositions.Clear();
    }

    private void OnDisable()
    {
        Commander.Click -= OnMouseClicked;
    }


    public void Execute()
    {
        RemoveAllSprites();
    }

    private void OnMouseClicked(Vector3 pos)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out hit))
        {
            clickPositions.Add(hit.point);
            InstantiateSpriteAtPosition(pos);
        }
    }



    private void InstantiateSpriteAtPosition(Vector3 position)
    {
        GameObject spriteInstance = Instantiate(spritePrefab, position, Quaternion.identity);
        instantiatedSprites.Add(spriteInstance);
    }

    public void RemoveAllSprites()
    {
        foreach (var sprite in instantiatedSprites)
        {
            Destroy(sprite);
        }
        instantiatedSprites.Clear();
    }
}
