using UnityEngine;
using UnityEngine.UIElements;

public class TestUxml : MonoBehaviour {
    
     public UIDocument pauseMenuUI;
     private Button exitButton;

     private void OnEnable() {
          exitButton = pauseMenuUI.rootVisualElement.Q("Exit") as Button;
          exitButton.RegisterCallback<ClickEvent>(Exit);
     }
     
     private void Exit(ClickEvent evt) {
          Debug.Log("Exit!");
     }

}
