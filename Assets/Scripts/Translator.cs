using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Translator : MonoBehaviour
{
    private static int _languageId;
    private static List<TranslatableText> _listId = new List<TranslatableText>();

    private static string[,] _lineText =
    {
        #region English
        {
            "Play", //0
            "Info", //1
            "Back", //2
            "Camera sens", //3
            "Joystick", //4
            "Toucnpad", //5
            "Remove ads", //6
            "Capybars", //7
            "Landscape", //8
            "Buy 5000", //9
            "Menu", //10
            "Thanks a lot for playing the game, here you may see more capybar <3", //11
            "Restart", //12
            "Respawn ads", //13
            "capybucks", //14
            "Alive 3 min", //15
            "Easy maze", //16
            "Medium maze", //17
            "Impossible maze", //18
            "Respawn", //19
            "Buy", //20
            "Painted capybara", //21
            "Capybara Poko", //22
            "Capybara Bandit", //23 
            "Gold Capybara", //24
            "Default terrain", //25
            "Forest terrain", //26
            "Sand terrain", //27
            "Snow Terrain", //28
            "Select Game Mode", //29
            "need to pass alive 5 min", //30
            "need to pass easy maze", //31
            "need to pass medium maze" //32
        },
        #endregion

                #region Russian
        {
            "������", //0
            "����", //1
            "�����", //2
            "������. ������", //3
            "��������", //4
            "������", //5
            "������ �������", //6
            "��������", //7
            "���������", //8
            "Buy 5000", //9
            "����", //10
            "������� ������� �� ����������� ����, ����� �� ������ ������� ������ ������� <3", //11
            "�������", //12
            "������� ads", //13
            "����������", //14
            "���� 3 ���", //15
            "������ ��������", //16
            "������� ��������", //17
            "����������� ��������", //18
            "�������", //19
            "Buy", //20
            "������������ ��������", //21
            "�������� ����", //22
            "�������� ������", //23 
            "������� ��������", //24
            "������� ��������", //25
            "������ ��������", //26
            "�������� ��������", //27
            "������ ��������", //28
            "�������� ������� �����", //29
            "����� ������ ������ 5 ���", //30
            "����� ������ ������ ��������", //31
            "����� ������ ������� ��������" //32
        },
        #endregion
    };

    static public void SelectLanguage(int id)
    {
        if (_languageId != id)
        {
            _languageId = id;
            UpdateTexts();
        }
    }

    static public string GetText(int textKey)
    {
        return _lineText[_languageId, textKey];
    }

    static public void Add(TranslatableText idText)
    {
        _listId.Add(idText);
        idText.GetText.text = _lineText[_languageId, idText.GetId];

    }

    static public void Clear()
    {
        _listId.Clear();
    }

    static public void Delete(TranslatableText idText)
    {
        _listId.Remove(idText);
    }

    static public void UpdateTexts()
    {
        for(int i=0; i < _listId.Count; i++) 
        {
            _listId[i].GetText.text = _lineText[_languageId, _listId[i].GetId];
        }
    }
}
