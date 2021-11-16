using UnityEngine;

/// <summary>
/// ������ ��� <b>���������</b> ������, � <b>������ �������� ����� �� ������ FindObjectOfType</b>.
/// </summary>
public class LevelManager : MonoBehaviour
{
    public SpawnPoint playerSpawn { get; private set; }
    private void Awake(){ playerSpawn = GetComponentInChildren<SpawnPoint>();}
}
