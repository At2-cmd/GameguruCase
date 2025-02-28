using _Game.Scripts.Entities.UI;
using UnityEngine;
using Zenject;

public abstract class PopupBase : MonoBehaviour
{
    [Inject] protected BlackScreen BlackScreen;
    [SerializeField] private FinishPopupBoard board;
    public virtual void Initialize()
    {
        board.PlayOpeningAnimation(null);
    }

    public void SetPopupActiveness(bool value)
    {
        gameObject.SetActive(value);
    }
}
