using UnityEngine;
using UnityEngine.UI;
 
[RequireComponent(typeof(Button))]
public class QuitGame : MonoBehaviour
{
    private Button button;
 
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }
 
    private void TaskOnClick()
    {
        Debug.Log("Quitter le jeu !");
        Application.Quit();
 
        // En mode Éditeur, Unity ne ferme pas l'éditeur.
        // Pour tester dans l’éditeur, tu peux ajouter :
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}