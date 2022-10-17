using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class GameData : ScriptableObject
{
    public string CharacterName;
    public int FootDamageMultipler;
    public int HandDamageMultipler;
}
