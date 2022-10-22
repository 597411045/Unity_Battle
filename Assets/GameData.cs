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
    public int ATK_J;
    public int ATK_H;
    public string WeaponType;
    public string EquipType;

}

[Serializable]
public class UserInfo
{
    public string name;
    public int coin;
    public int vipLevel;
    public string lastLoginTime;

    public UserInfo(string name, int coin, int vipLevel, string lastLoginTime)
    {
        this.name = name;
        this.coin = coin;
        this.vipLevel = vipLevel;
        this.lastLoginTime = lastLoginTime;
    }
}
