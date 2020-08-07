using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneSwitcher : MonoBehaviour
{
    public string sceneName;
    public void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { proceed(); });
    }

    public void proceed()
    {
        SceneManager.LoadScene(sceneName,LoadSceneMode.Additive);
    }



}
