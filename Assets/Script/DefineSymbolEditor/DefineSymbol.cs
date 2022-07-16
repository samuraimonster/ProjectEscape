using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Define�̃L�[�Ə�Ԃ�ێ�����N���X
/// </summary>
public class DefineSymbol
{
    private string key;

    public string Key { get { return key; } }

    private bool isEnable;

    public bool IsEnable { get { return isEnable; } set { isEnable = value; } }

	public DefineSymbol(string key, bool isEnable)
	{
		this.key = key;
		this.isEnable = isEnable;
	}
}
