using System;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "New MapSetUp", menuName = "Map Set Up", order = 2)]
public class MapSetUp : ScriptableObject {
    [SerializeField] public List<Node> nodes = new();
    [SerializeField] public List<Path> paths = new();

    [SerializeField] public int entryNodeIdx;
}
