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
            "Играть", //0
            "Инфо", //1
            "Назад", //2
            "Чувств. камеры", //3
            "Джойстик", //4
            "Сенсор", //5
            "Убрать рекламу", //6
            "Капибары", //7
            "Окружение", //8
            "Buy 5000", //9
            "Меню", //10
            "Большое спасибо за прохождение игры, здесь ты можешь увидеть больше капибар <3", //11
            "Рестарт", //12
            "Респавн ads", //13
            "капибаксов", //14
            "Живи 3 мин", //15
            "легкий лабиринт", //16
            "средний лабиринт", //17
            "невозможный лабиринт", //18
            "Респавн", //19
            "Buy", //20
            "Нарисованная капибара", //21
            "Капибара Поко", //22
            "Капибара бандит", //23 
            "Золотая капибара", //24
            "Обычный ландшафт", //25
            "Лесной ландшафт", //26
            "Песочный ландшафт", //27
            "Зимний ландшафт", //28
            "Выберите Игровой Режим", //29
            "нужно пройти выживи 5 мин", //30
            "нужно пройти легкий лабиринт", //31
            "нужно пройти средний лабиринт" //32
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
