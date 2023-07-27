using UnityEngine;
using System.Collections;

public class Move
{
    private float Speed;
    private float Duration;

    public Move()
    {

    }

    // Coroutine to be called from a MonoBehaviour
    public IEnumerator Charge(Transform unitTransform)
    {
        float startTime = Time.time;

        while (Time.time < startTime + Duration)
        {
            unitTransform.position += unitTransform.forward * Speed * Time.deltaTime;
            yield return null;
        }
    }

    
}