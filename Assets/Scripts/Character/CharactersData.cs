using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharactersData
{
    [field: SerializeField]public float charactersBaseSpeed { get; private set; } = 1.5f;
}
