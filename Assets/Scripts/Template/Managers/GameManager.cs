using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStage { StartWait, Game, EndWait };
/// <summary>
/// ������� ��������.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// <b>���������</b> ��������� ��� ��������� � <b>��</b> ����������� ������� � ����������. 
    /// </summary>
    public static GameManager instance { get; private set; }

    //����������
    /// <summary>
    /// <i>��������:</i> <b>LevelManager</b> �������� ������� �� �����.
    /// </summary>
    public static LevelManager currentLevel { get; private set; }
    /// <summary>
    /// <i>��������:</i> ������� ����� �� �����.
    /// </summary>
    public static GameObject player { get; private set; }
    /// <summary>
    /// <i>��������:</i> <b>Canvas</b> �������� ������� �� �����.
    /// </summary>
    public static Canvas canvas { get; private set; }
    /// <summary>
    /// <i>��������:</i> ������ ���� �� ������� ������ ��������� �����. 
    /// </summary>
    public static GameStage gameStage;

    //������
    GameDataObject.GDOMain data;
    GameDataObject gdata;



    // ������ 
    /// <summary>
    /// <i>�����</i> ���������� ��� <b>GameStage</b> ������� <b>Game</b>
    /// </summary>
    public static event System.Action StartGame = delegate { }; //����� gameStage ���������� Game
    /// <summary>
    /// <i>�����</i> ���������� ��� <b>GameStage</b> ������� <b>EndWait</b>
    /// </summary>
    public static event System.Action EndGame = delegate { }; //����� gameStage ���������� EndWait
    /// <summary>
    /// <i>�����</i> ���������� ��� ������ ������� �� �����, ���� ��� ���� �������� <b>startByTab</b> � <b>GameData</b> ����� <b>True</b>
    /// </summary>
    public static event System.Action TapToPlayUI = delegate { }; //����� ����� ������ � ������ ��� ��� data.startByTap



    #region Mono
    public void Awake()
    {
        Application.targetFrameRate = 60;
        instance = this;

        StartGame = delegate { };
        EndGame = delegate { };
        TapToPlayUI = delegate { };

        gameStage = GameStage.StartWait;

        QualitySettings.SetQualityLevel(QualitySettings.names.Length - 1);

        gdata = GameDataObject.GetData();
        data = gdata.main;
        OnLevelStarted(data);
        LoadLevel();
    }
    private void Start()
    {
        

    }
    private void Update()
    {
        if (instance == null) instance = this;
        EditorControls();
        TapToStartCheck();
    }

    #endregion

    #region Gameplay

    /// <summary>
    /// ���� �������� <b>startByTab</b> � <b>GameData</b> ����� <b>True</b> �� ���������� ���� �����. �� ��������� ������ ���. ����� �������� TapToPlayUI().
    /// </summary>
    public void TapToStartCheck() //�������� �� ������ ��� 
    {
        if (data.startByTap)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (gameStage == GameStage.StartWait)
                {
                    StartGameByTap();
                    gameStage = GameStage.Game;
                    TapToPlayUI();
                    StartGame();
                }
            }
        }
    }

    /// <summary>
    /// ���� ����� ������� ������� �������, ������ � ������.
    /// </summary>
    public void LoadLevel() //�������� ������ 
    {
        var stdData = GameDataObject.GetMain(true);
        if (stdData.saves == null) { Debug.LogError("Yaroslav: Saves Not Found"); return; }
        if (stdData.levelList == null || stdData.levelList.Count == 0) { Debug.LogError("Yaroslav: Levels List in \"" + GameDataObject.GetData(true).name + "\" is empty"); return; }

        stdData.saves.SetLevel((int)stdData.saves.GetPref(Prefs.Level));
        currentLevel = Instantiate(stdData.levelList[(int)stdData.saves.GetPref(Prefs.Level)]);
        //����� � ������
        SpawnPlayer();
        SpawnCanvas();
    }

    /// <summary>
    /// ���� ����� ������� ������.
    /// </summary>
    public void SpawnPlayer()
    {
        if (data.playerPrefab)
        {
            Vector3 spawnPoint = Vector3.zero;
            if (currentLevel.playerSpawn != null)
            {
                spawnPoint = currentLevel.playerSpawn.transform.position;
                //��������� ������
            }
            else
            {
                Debug.LogError("Yaroslav: Spawn Not Found");
            }
            player = Instantiate(data.playerPrefab, spawnPoint, Quaternion.identity);
        }
    }

    /// <summary>
    /// ���� ����� ������� ������.
    /// </summary>
    public void SpawnCanvas()
    {
        if (data.canvas)
            canvas = Instantiate(data.canvas.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Canvas>();
    }

    /// <summary>
    /// ����� ��� �������� �� ����� ��������� ����.
    /// </summary>
    public void StopGamePlay() //��������� ���� (������ � ��.) 
    {
        //���������� ������ � ��.
    }

    /// <summary>
    /// ����� ��� �������� �� ����� ����.
    /// </summary>
    public void StartGameByTap() //��������� ��� ���� (������ � ��.)
    {
        //��������� ���������� � ��.
    }

    #endregion

    #region Editor
    /// <summary>
    /// ����� ������������ ������ �� ����� ���� � �������.
    /// </summary>
    public void EditorControls()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            data.saves.AddToPref(Prefs.Points, 10);
        }

#endif
    }
    #endregion

    #region Static
    /// <summary>
    /// ����� <b>GameStage</b> � �������� ������ ������ � ����� ���� � ����������� �� <b>GameStage</b> (�������� ������).
    /// </summary>
    /// <param name="data">GameData �������� ������</param>
    public static void OnLevelStarted(GameDataObject.GDOMain data)
    {
        if (data.startByTap)
        {
            gameStage = GameStage.StartWait;
            instance.StopGamePlay();
        }
        else
        {
            gameStage = GameStage.Game;
            StartGame();
        }
    }

    /// <summary>
    /// ����������� ������ � �������� ������. (����� ��� ������)
    /// </summary>
    /// <param name="win">������� ��������� ������</param>
    public static void OnLevelEnd(bool win = true)
    {
        instance.StopGamePlay();
        gameStage = GameStage.EndWait;
        EndGame();
        
        if (win)
        {
            Debug.Log("Win Event exec");
        }
        else
        {
            Debug.Log("Loose Event exec"); 
        }

        //������ ������
        //����� ������
    }

    /// <summary>
    /// ������������� ������� �����. 
    /// </summary>
    public static void Restart()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// ��������� ��������� ����� (������ ���������).
    /// </summary>
    public static void NextLevel()
    {
        var data = GameDataObject.GetMain(true);
        data.saves.SetPref(Prefs.Level, (int)data.saves.GetPref(Prefs.Level) + 1);
        data.saves.SetLevel((int)data.saves.GetPref(Prefs.Level));
        data.saves.AddToPref(Prefs.CompletedLevels, 1);
        SceneManager.LoadScene(0);
    }

    #endregion
}
