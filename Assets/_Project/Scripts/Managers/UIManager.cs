using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainScreen _mainScreen;
    [SerializeField] private GameplayScreen _gameplayScreen;

    public MainScreen MainScreen => _mainScreen;
    public GameplayScreen GameplayScreen => _gameplayScreen;
}
