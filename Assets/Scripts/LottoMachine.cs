using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LottoMachine : MonoBehaviour
{
    public GameObject TitleObj, SettingObj, ResultObj;
    public InputField InputCount;

    public int minValue;
    public int maxValue;
    public int size;
    public int count;

    private Lotto.Lotto lotto;
    
    private void Start()
    {
        lotto = new Lotto.Lotto();
        ResultObj.SetActive(false);
        InputCount.text = this.count.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private List<int> list;

    public LottoRow[] rows;
    public void OnClickDrawBtn()
    {
        if (!ResultObj.activeSelf)
        {
            ResultObj.SetActive(true);
            TitleObj.SetActive(false);
        }

        count = int.Parse(InputCount.text);
        count = Mathf.Clamp(count, 1, 5);
        InputCount.text = count.ToString();

        LottoRow row;
        for(int i = 0; i < count; i++)
        {
            if (!rows[i].gameObject.activeSelf)
            {
                rows[i].gameObject.SetActive(true);
            }

            list = lotto.GetRandomNumberListOrdered(minInclusive: minValue, maxExclusive: maxValue + 1, size: size);

            row = rows[i];
            for (int j = 0; j < row.Numbers.Length; j++)
            {
                row.Numbers[j].text = list[j].ToString();
            }
        }

        for(int i = count; i < rows.Length; i++)
        {
            if (rows[i].gameObject.activeSelf)
            {
                rows[i].gameObject.SetActive(false);
            }
        }
    }

}
