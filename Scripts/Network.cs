using UnityEngine;

public class Network : MonoBehaviour
{
    private float[,] weightInpHid = {
        { 0.25f, 0.75f, 0f },
        { 0, 0, 1.1f } };
    private float[] weightsHidOutput = { 1.5f, -0.5f };
    private float ActFunc(float number, int a)
    {
        if (a == 0)
        {
            return number / 12;
        }
        else if (a == 1)
        { return 1 - Mathf.Exp(number * -0.025f); }
        else
        {
            return (number + Mathf.Abs(number)) / 2;
        }
    }
    public float StartNetwork(float[] gh)
    {
        float[] hidden = new float[weightInpHid.GetLength(0)];
        for (int i = 0; i < weightInpHid.GetLength(0); i++)
        {
            float sum = 0; for (int j = 0; j < weightInpHid.GetLength(1); j++)
            {
                sum += weightInpHid[i, j] * gh[j];
            }
            hidden[i] = ActFunc(sum, i);
        }
        float output = 0;
        for (int i = 0; i < hidden.Length; i++)
        {
            output += hidden[i] * weightsHidOutput[i];
        }
        output = ActFunc(output, 2); 
        return output;
    }
}
