using System;
using System.Linq;
using UnityEngine;

public class Network : MonoBehaviour
{
    private float[,] weightInpHid = {
  { -1.22954059f, -0.930465937f, 1.77526736f, 1.88378048f },
  { 0.489727646f, 1.3726325f, -0.455932766f, -0.8009566f },
  { -0.5914321f, -1.956491f, -0.07288879f, -0.485261232f },
  { 0.0711543262f, 0.298084766f, -0.3439504f, -0.956443667f },
  { 0.576780856f, 3.094362f, 1.58482909f, 0.6864667f },
  { -1.04475331f, 0.161576241f, -0.8006556f, 0.302395225f },
  { -0.38842386f, -0.8570758f, -0.0389397144f, 0.604436636f },
  { 1.89107108f, 1.34442425f, -0.2671836f, 1.54239559f } };
    private float[,] weightsHidOutput = {
  { 2.03363562f, -0.257909566f, 2.69370651f, -0.858668268f, -3.03815317f, 0.7080179f, -0.6615526f, -0.690101266f },
  {  -2.235147f, -1.01463759f, 0.26538524f, -0.413025141f, 1.5112f, 0.436894834f, 0.8698337f, -2.23475814f },
  { -0.10838531f, 1.18607664f,  -2.59568381f, 0.324735f,  1.52889311f, -0.794466f, 0.3075876f, 2.63443565f} };
    private float[] biasHid = { -0.0664124861f, -0.579240859f, 3.04535913f, -0.209700346f,
        -1.26377356f, -0.126239508f, -0.2755882f, -2.346931f };
    private float[] biasOut = { 1.199296f, 0.4537396f, -1.552201f };
    private void InitializeWeights(float[,] weights)
    {
        for (int i = 0; i < weights.GetLength(0); i++)
            for (int j = 0; j < weights.GetLength(1); j++)
                weights[i, j] = UnityEngine.Random.Range(-1f, 1f);
    }
    private float[] Softmax(float[] logits)
    {
        float max = logits.Max();
        float sum = 0f;
        float[] result = new float[logits.Length];
        for (int i = 0; i < logits.Length; i++)
        {
            result[i] = (float)Mathf.Exp(logits[i] - max);
            sum += result[i];
        }
        for (int i = 0; i < logits.Length; i++)
        {
            result[i] /= sum;
        }
        return result;
    }
    public int StartNetwork(float[] gh)
    {
        float[] hidden = new float[weightInpHid.GetLength(0)];
        for (int i = 0; i < weightInpHid.GetLength(0); i++)
        {
            float sum = biasHid[i];
            for (int j = 0; j < weightInpHid.GetLength(1); j++)
            {
                sum += weightInpHid[i, j] * gh[j];
            }
            hidden[i] = Mathf.Max(0, sum);
        }
        float[] output = new float[weightsHidOutput.GetLength(0)];
        for (int i = 0; i < weightsHidOutput.GetLength(0); i++)
        {
            float sum2 = biasOut[i];
            for (int j = 0; j < weightsHidOutput.GetLength(1); j++)
            {
                sum2 += weightsHidOutput[i, j] * hidden[j];
            }
            output[i] = sum2;
        }
        output = Softmax(output);
        return Array.IndexOf(output, output.Max()); ;
    }
}
