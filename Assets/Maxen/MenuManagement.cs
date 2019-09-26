using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManagement
{
    private static int MenuScene = 0;
    private static int GameScene = 1;

    public static void GoToMainMenu()
    {
        SceneManager.LoadScene(MenuScene);
    }

    public static void GoToGame()
    {
        SceneManager.LoadScene(GameScene);
    }
}
