using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : BaseManager<ButtonManager>
{
    public Dictionary<string, string> statusToKey = new Dictionary<string, string>();
    public Dictionary<string, KeyCode> keyToInput = new Dictionary<string, KeyCode>();
    public Dictionary<KeyCode, string> keyToString = new Dictionary<KeyCode, string>();
    public override void OnInit()
    {
        AddMapping();
        base.OnInit();
    }
    private void AddMapping()
    {
        #region
        statusToKey.Add("leftmove", GameFacade.Instance.playerManager.playerData.LeftMove);
        statusToKey.Add("rightmove", GameFacade.Instance.playerManager.playerData.RightMove);
        statusToKey.Add("changestate", GameFacade.Instance.playerManager.playerData.ChangeState);
        statusToKey.Add("jump", GameFacade.Instance.playerManager.playerData.Jump);
        statusToKey.Add("interact", GameFacade.Instance.playerManager.playerData.Interact);
        #endregion
        //添加字符和keycode的映射关系
        #region
        keyToInput.Add("A", KeyCode.A);
        keyToInput.Add("B", KeyCode.B);
        keyToInput.Add("C", KeyCode.C);
        keyToInput.Add("D", KeyCode.D);
        keyToInput.Add("E", KeyCode.E);
        keyToInput.Add("F", KeyCode.F);
        keyToInput.Add("G", KeyCode.G);
        keyToInput.Add("H", KeyCode.H);
        keyToInput.Add("I", KeyCode.I);
        keyToInput.Add("J", KeyCode.J);
        keyToInput.Add("K", KeyCode.K);
        keyToInput.Add("L", KeyCode.L);
        keyToInput.Add("M", KeyCode.M);
        keyToInput.Add("N", KeyCode.N);
        keyToInput.Add("O", KeyCode.O);
        keyToInput.Add("P", KeyCode.P);
        keyToInput.Add("Q", KeyCode.Q);
        keyToInput.Add("R", KeyCode.R);
        keyToInput.Add("S", KeyCode.S);
        keyToInput.Add("T", KeyCode.T);
        keyToInput.Add("U", KeyCode.U);
        keyToInput.Add("V", KeyCode.V);
        keyToInput.Add("W", KeyCode.W);
        keyToInput.Add("X", KeyCode.X);
        keyToInput.Add("Y", KeyCode.Y);
        keyToInput.Add("Z", KeyCode.Z);
        keyToInput.Add("Space", KeyCode.Space);

        keyToString.Add(KeyCode.A, "A");
        keyToString.Add(KeyCode.B, "B");
        keyToString.Add(KeyCode.C, "C");            
        keyToString.Add(KeyCode.D, "D");
        keyToString.Add(KeyCode.E, "E");
        keyToString.Add(KeyCode.F, "F");
        keyToString.Add(KeyCode.G, "G");
        keyToString.Add(KeyCode.H, "H");
        keyToString.Add(KeyCode.I, "I");
        keyToString.Add(KeyCode.J, "J");
        keyToString.Add(KeyCode.K, "K");
        keyToString.Add(KeyCode.L, "L");
        keyToString.Add(KeyCode.M, "M");
        keyToString.Add(KeyCode.N, "N");
        keyToString.Add(KeyCode.O, "O");
        keyToString.Add(KeyCode.P, "P");
        keyToString.Add(KeyCode.Q, "Q");
        keyToString.Add(KeyCode.R, "R");
        keyToString.Add(KeyCode.S, "S");
        keyToString.Add(KeyCode.T, "T");
        keyToString.Add(KeyCode.U, "U");
        keyToString.Add(KeyCode.V, "V");
        keyToString.Add(KeyCode.W, "W");
        keyToString.Add(KeyCode.X, "X");
        keyToString.Add(KeyCode.Y, "Y");
        keyToString.Add(KeyCode.Z, "Z");
        keyToString.Add(KeyCode.Space, "Space");
        #endregion
    }
    /// <summary>
    /// 根据命令返回keycode值
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public KeyCode GetKeyCode(string order)
    {
        return keyToInput[statusToKey[order]];
    }

    /// <summary>
    /// 改变playerdata里的按键信息
    /// </summary>
    public void ChangeMapping(string left, string right, string jump, string change, string intercat)
    {
        GameFacade.Instance.playerManager.playerData.LeftMove = left;//从输入框或复选框输入值
        GameFacade.Instance.playerManager.playerData.RightMove = right;
        GameFacade.Instance.playerManager.playerData.Jump=jump;
        GameFacade.Instance.playerManager.playerData.ChangeState = change;
        GameFacade.Instance.playerManager.playerData.Interact = intercat;
    }
    /// <summary>
    /// 将playerdata里的按键信息存入配置文件
    /// </summary>
    public void SaveMappingChange()
    {
        GameFacade.Instance.playerManager.UpdataData();
        GameFacade.Instance.playerManager.OnInit();
    }
}
