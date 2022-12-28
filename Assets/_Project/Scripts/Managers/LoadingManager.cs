using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : Singleton<LoadingManager>
{
    private void Start()
    {
        StartCoroutine(LoadingCoroutine());
    }

    private IEnumerator LoadingCoroutine()
    {
        yield return StartCoroutine(DataManager.Instance.Load());

        yield return StartCoroutine(Wallet.Instance.Load());

        SceneManager.LoadScene(1);
    }
}
