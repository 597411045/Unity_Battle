using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class GameData
{
    public string CharacterName;
    public int FootDamageMultipler;
    public int HandDamageMultipler;
    public string WeaponType;
    public string EquipType;

    public GameData()
    {
        CharacterName="C1";
        FootDamageMultipler=2;
        HandDamageMultipler=1;
        WeaponType = "Sword";
        EquipType = "";
    }
}
