using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
[RequireComponent(typeof(Button))]
public class loadGame : MonoBehaviour
{
 private Button button;
 
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        Debug.Log("lancer le jeu !");
       SceneManager.LoadScene (sceneBuildIndex:1);
    }
}
