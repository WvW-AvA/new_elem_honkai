using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class UI_SaveFileWindow : UI_Window
{
    public GameObject fileTagPrefab;

    private List<GameObject> fileTagsList;
    protected override void OnUIInit()
    {
        base.OnUIInit();
        fileTagsList = new List<GameObject>();
        var fileTags = transform.Find("FileTags");
        for (int i = 0; i < 8; i++)
        {
            var temp = GameObject.Instantiate(fileTagPrefab, fileTags);
            InitFileTag(temp, i);
            fileTagsList.Add(temp);
        }
    }

    private void InitFileTag(GameObject fileTag, int index)
    {
        var saveFilesInfo = SaveSystem.Instance.saveFilesInfo;
        var btn = fileTag.GetComponent<Button>();
        if (index < saveFilesInfo.Count)
        {
            FillFileTag(fileTag, saveFilesInfo[index]);
            btn.onClick.AddListener(() =>
            {
                SaveSystem.Load(index);
                CloseWindow();
            });
        }
        else
        {
            FillFileTag(fileTag);
            btn.onClick.AddListener(() =>
            {
                var sure = UIManager.PushBackWindow(UIConst.SureWindow) as UI_SureWindow;
                sure.text = "确定要新建存档吗?";
                sure.AddSureEvent(() =>
                {
                    SaveSystem.Load(SaveSystem.CreateNewSaveFile());
                    CloseWindow();
                });
            });
        }
    }

    private void FillFileTag(GameObject fileTag, SaveSystem.SaveFileInfo fileInfo = null)
    {
        if (fileInfo == null)
        {
            return;
        }
        fileTag.transform.Find("SceneName").GetComponent<Text>().text = fileInfo.scene;
        fileTag.transform.Find("Player_HP").GetComponent<Text>().text = fileInfo.HP.ToString();
    }
}
