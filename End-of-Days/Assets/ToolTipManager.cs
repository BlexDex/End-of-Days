using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;
public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager Instance;

    public TextMeshProUGUI toolTipCont;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetAndShowToolTip(string message)
    {
        gameObject.SetActive(true);
        toolTipCont.text = message;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        toolTipCont.text = string.Empty;
    }
}
