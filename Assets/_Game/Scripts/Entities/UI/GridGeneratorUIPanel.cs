using TMPro;
using UnityEngine;
using Zenject;

public class GridGeneratorUIPanel : MonoBehaviour
{
    [Inject] IGridController _gridController;
    [SerializeField] private TMP_InputField xDimensionField;
    [SerializeField] private TMP_InputField yDimensionField;

    public void OnGenerateButtonClicked()
    {
        _gridController.GenerateGrid(int.Parse(xDimensionField.text), int.Parse(yDimensionField.text));
    }
}
