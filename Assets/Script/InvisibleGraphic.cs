using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// �����Ȃ�Graphic(uGUI�Ń{�^���̓����蔻���傫������p)
/// </summary>
public class InvisibleGraphic : Graphic
{
    //���_�𐶐�����K�v������Ƃ��̃R�[���o�b�N�֐�
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);
        vh.Clear();//���_��S�ăN���A���A�����\������Ȃ��悤��
    }

    //�\������K�v���Ȃ��̂ŁA�C���X�y�N�^�[�ɉ����\�����Ȃ��悤��
#if UNITY_EDITOR

    [CustomEditor(typeof(InvisibleGraphic))]
    class GraphicCastEditor : Editor
    {
        public override void OnInspectorGUI()
        {
        }
    }

#endif

}