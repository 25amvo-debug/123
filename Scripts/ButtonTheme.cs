using TMPro;
using UnityEngine;

public class ButtonTheme : MonoBehaviour
{
    public MainController.Types typeButtonTheme;
    public void Pr()
    {
        var child = transform.Find("TextText");
        child.GetComponent<textButtonTheme>().UpdateText();
        var child2 = transform.Find("TextTheme");
        string name = null;
        switch (typeButtonTheme)
        {
            case MainController.Types.MathIntroductionToAlgebra:
                name = "Вступ до алгебри";
                break;
            case MainController.Types.MathIdentity:
                name = "Задачі та Тотожність";
                break;
            case MainController.Types.MathPowerWithNaturalExponent:
                name = "Степені";
                break;
            case MainController.Types.MathMonomialAndPolynomial:
                name = "Одночлени та многочлени";
                break;
            case MainController.Types.MathAddingAndSubtractingAndMultiplePolynomials:
                name = "Дії з многочленами";
                break;
            case MainController.Types.MathMOAPBAPFOP:
                name = "Складніші дії з многочленами";
                break;
            case MainController.Types.MathGroupingMethodTwoExpressions:
                name = "Метод групування. Добуток різниці та суми двох виразів";
                break;
            case MainController.Types.MathSquareOfSumSquareOfDifference:
                name = "Формули скороченого множення";
                break;
            case MainController.Types.MathConvertingPolynomialFactorization:
                name = "Перетворення многочлена";
                break;
        }
        child2.GetComponent<TMP_Text>().text = name;
    }
    public void OnTrigered()
    {
        FindAnyObjectByType<BlockSwitchController>().InizializateBlock(2, typeButtonTheme);
    }
    private void Start()
    {
       Pr();
    }
}
