using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TapToStart : MonoBehaviour
{
    [Inject] ITouchInputController _touchInputController;
    [SerializeField] private Button taptoStartButton;
    public void Initialize()
    {
        taptoStartButton.onClick.AddListener(OnTapToStartButtonClicked);
    }

    private void OnTapToStartButtonClicked()
    {
        EventController.Instance.RaiseGameStarted();
        _touchInputController.CanUserGiveInput = true;
        gameObject.SetActive(false);
    }
}
