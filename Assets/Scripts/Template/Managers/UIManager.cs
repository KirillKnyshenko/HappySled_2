using UnityEngine;

/// <summary>
/// ������ ������� ��������� �� ������� � ��������� ������� UI
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// <b>���������</b> ��������� ��� ��������� � <b>��</b> ����������� ������� � ����������. 
    /// </summary>
    public static UIManager instance;

    [SerializeField] GameObject deathUI, winUI;
    [SerializeField] GameObject tapToPlay;

    #region Mono
    private void Start()
    {
        instance = this;
        InitTapToPlay();
    }

    /// <summary>
    /// ������� ������ TapToPlay ��� ������ ������� ���� ������������ � ������. 
    /// </summary>
    public void InitTapToPlay()
    {
        if (tapToPlay != null)
        {
            if (GameManager.gameStage == GameStage.StartWait)
            {
                tapToPlay.SetActive(true);
                GameManager.TapToPlayUI += () => { Tweaks.AnimationPlayType(tapToPlay, PlayType.Rewind); }; //�������� � ������ ����
            }
            else
            {
                tapToPlay.SetActive(false);
            }
        }
    }

    private void Update()
    {
        EditorControls();
    }

    #endregion

    #region Buttons

    /// <summary>
    /// ����� ������������ ������ �� ����� ���� � �������.
    /// </summary>
    public void EditorControls()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F))
        {
            Win();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Loose();
        }
#endif
    }

    /// <summary>
    /// ����� <b>NextLevel</b> � GameManager
    /// </summary>
    public void NextLevel()
    {
        GameManager.NextLevel();
    }

    /// <summary>
    /// ����� <b>Restart</b> � GameManager
    /// </summary>
    public void Restart()
    {
        GameManager.Restart();
    }

    #endregion

    #region Evens_Win_Loose
    /// <summary>
    /// ����� ������. �������� �������� ��������� � ���������� ������ � UI.
    /// </summary>
    public void Win()
    {
        if (!winUI.active && !deathUI.active)
        {
            GameManager.OnLevelEnd();
            winUI.SetActive(true);
        }
    }

    /// <summary>
    /// ����� ���������. �������� �������� ��������� � ���������� ������ � UI.
    /// </summary>
    public void Loose()
    {
        if (!winUI.active && !deathUI.active)
        {
            GameManager.OnLevelEnd(false);
            deathUI.SetActive(true);
        }
    }

    #endregion
}
