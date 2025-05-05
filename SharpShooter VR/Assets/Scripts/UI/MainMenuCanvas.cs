using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class MainMenuCanvas : XRButtonInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("OnselectEntered");
        base.OnSelectEntered(args);
        
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("VR TESTING");
    }
}
