using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharsData", menuName = "Character/CharSrciptableObject")]
public class CharSrciptableObject : ScriptableObject
{
    [SerializeField] public CharactersData CharactersData;
    [SerializeField] public PLayerData PlayerData;
    [SerializeField] public EnemiesData EnemiesData;
}
