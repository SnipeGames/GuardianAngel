using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager instance;
    public List<Path> paths = new List<Path>();
    
    private void Awake()
    {
        instance = this;
    }
    
    public void AddPath(Path path)
    {
        paths.Add(path);
    }    
}
