using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private void Start()
    {
        Commander.Confirm += StartSlash;
    }

    public void StartSlash()
    {

    }
}
