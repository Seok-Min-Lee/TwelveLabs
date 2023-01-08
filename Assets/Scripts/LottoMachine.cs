using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LottoMachine : MonoBehaviour
{
    public GameObject ResultObj;
    public InputField InputCount;

    private int minValue;
    private int maxValue;
    private int drawCount;
    private int repeatCount;

    private Lotto.Lotto lotto;
    
    private void Start()
    {
        lotto = new Lotto.Lotto();
        ResultObj.SetActive(false);
        
        SetupAttributes();
        
        InputCount.text = this.repeatCount.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickBackBtn();
        }
    }

    public void OnClickRedrawBtn()
    {
        DrawLotto();
    }
    public void OnClickBackBtn()
    {
        if (ResultObj.activeSelf)
        {
            ResultObj.SetActive(false);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.TITLE);
        }
    }

    public void OnClickAddBtn()
    {
        repeatCount++;
        repeatCount = Mathf.Clamp(repeatCount, 1, 5);
        InputCount.text = repeatCount.ToString();
    }

    public void OnClickMinusBtn()
    {
        repeatCount--;
        repeatCount = Mathf.Clamp(repeatCount, 1, 5);
        InputCount.text = repeatCount.ToString();
    }

    public LottoRow[] rows;
    private List<int> list;
    public void OnClickDrawBtn()
    {
        // 반복 횟수 구하기
        repeatCount = int.Parse(InputCount.text);
        repeatCount = Mathf.Clamp(repeatCount, 1, 5);
        InputCount.text = repeatCount.ToString();

        // 결과 출력할 오브젝트 활성화
        if (!ResultObj.activeSelf)
        {
            ResultObj.SetActive(true);
        }

        DrawLotto();
    }

    private void DrawLotto()
    {
        LottoRow row;
        for (int i = 0; i < repeatCount; i++)
        {
            // row 활성화
            if (!rows[i].gameObject.activeSelf)
            {
                rows[i].gameObject.SetActive(true);
            }

            // 로또 값 구하기
            list = lotto.GetRandomNumberListOrdered(
                minInclusive: minValue, 
                maxExclusive: maxValue + 1, 
                size: drawCount
            );

            // 출력
            row = rows[i];
            for (int j = 0; j < row.Numbers.Length; j++)
            {
                row.Images[j].color = GetColorByRange(list[j]);
                row.Numbers[j].text = list[j].ToString();
            }
        }

        // 사용하지 않는 row 비활성화
        for (int i = repeatCount; i < rows.Length; i++)
        {
            if (rows[i].gameObject.activeSelf)
            {
                rows[i].gameObject.SetActive(false);
            }
        }
    }

    private Color GetColorByRange(int num)
    {
        Color color;

        if (num <= 10)
        {
            color = new Color(0.976f, 0.753f, 0.145f);  // orange
        }
        else if (num > 10 && num <= 20)
        {
            color = new Color(0.439f, 0.796f, 0.945f);  // skyblue
        }
        else if (num > 20 && num <= 30)
        {
            color = new Color(0.98f, 0.424f, 0.447f);   // lightRed
        }
        else if (num > 30 && num <= 40)
        {
            color = new Color(0.667f, 0.667f, 0.667f);  // gray
        }
        else
        {
            color = new Color(0.702f, 0.839f, 0.298f);  // lightGreen
        }

        return color;
    }
    public Text TempText;
    private void SetupAttributes()
    {
        if (DataManager.setupAttributes?.Count > 0)
        {
            SetupAttributesByDatabase();
            TempText.text = "Setup from Database";
        }
        else
        {
            SetupAttributesDefault();
            TempText.text = "Setup from Default";
        }
    }
    private void SetupAttributesByDatabase()
    {
        SetupAttribute attribute;
        string attributeName;
        int result;

        for (int i = 0; i < DataManager.setupAttributes.Count; i++)
        {
            attribute = DataManager.setupAttributes[i];

            if (attribute.Group.Equals(AttributeNames.GROUP_LOTTO))
            {
                attributeName = attribute.Name;

                if (attributeName.Equals(AttributeNames.MIN_VLAUE))
                {
                    if (int.TryParse(s: attribute.Value, result: out result))
                    {
                        this.minValue = result;
                    }
                }
                else if (attributeName.Equals(AttributeNames.MAX_VLAUE))
                {
                    if (int.TryParse(s: attribute.Value, result: out result))
                    {
                        this.maxValue = result;
                    }
                }
                else if (attributeName.Equals(AttributeNames.DRAW_COUNT))
                {
                    if (int.TryParse(s: attribute.Value, result: out result))
                    {
                        this.drawCount = result;
                    }
                }
                else if (attributeName.Equals(AttributeNames.REPEAT_COUNT))
                {
                    if (int.TryParse(s: attribute.Value, result: out result))
                    {
                        this.repeatCount = result;
                    }
                }
            }
        }
    }
    private void SetupAttributesDefault()
    {
        this.minValue = 1;
        this.maxValue = 45;
        this.drawCount = 6;
        this.repeatCount = 5;
    }
}
