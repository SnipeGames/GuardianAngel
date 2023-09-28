using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyIncomeSign : MonoBehaviour
{
    public List<Transform> destinations = new List<Transform>();
    Transform currentDestination;
    int desNum;
    protected readonly WaitForSeconds waitForPlayerSearchCooldown = new WaitForSeconds(0.1f);

    void OnSpawned()
    {
        desNum = 0;
        StartCoroutine(AIStart());
    }

    protected virtual IEnumerator AIStart()
    {
        yield return new WaitForFixedUpdate();
        SetCurruntDestination();
        while (desNum <= destinations.Count)
        {

            if (Vector3.Distance(transform.position, currentDestination.position) <= 1f)
            {
                SetCurruntDestination();
            }

            yield return waitForPlayerSearchCooldown;
        }
        //PoolBoss.Despawn(gameObject.transform);
    }

    public void SetCurruntDestination()
    {
        currentDestination = destinations[Mathf.Min(desNum, destinations.Count - 1)];
        desNum++;
    }
}
