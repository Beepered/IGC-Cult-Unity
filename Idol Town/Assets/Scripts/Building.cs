using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Building Preset", menuName = "New Building Preset")]
public class Building : ScriptableObject
{
    public GameObject prefab;
    public string name;
    public int cost;
    public int insanity, suspicion, money;
    public float people;
    public int suspicion_destruction;
}
