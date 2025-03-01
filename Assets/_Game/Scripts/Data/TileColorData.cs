using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileColorData
{
    [SerializeField] private List<Color> tileColorsForLevel;
    public List<Color> TileColorsForLevel => tileColorsForLevel;
}
