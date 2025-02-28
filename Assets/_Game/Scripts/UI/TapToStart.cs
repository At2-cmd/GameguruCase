using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TapToStart : MonoBehaviour, IInitializable
{
    [SerializeField] private Button taptoStartButton;
    public void Initialize()
    {
        Debug.Log("TapToStart initialized");
        taptoStartButton.onClick.AddListener(OnTapToStartButtonClicked);
    }

    private void OnTapToStartButtonClicked()
    {
        EventController.Instance.RaiseGameStarted();
        gameObject.SetActive(false);
    }
}
