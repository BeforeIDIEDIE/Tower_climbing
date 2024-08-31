using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public PlayerMovement Player;
    public PlayerHealthSystem PSHP;
    public GameObject fallingStone;
    public GameObject door;
    public Image fadeImage; // ���̵� ȿ���� ����� UI �̹���
    public float fadeDuration = 1f; // ���̵� ȿ���� ���� �ð�
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject DieCanvas;
    private bool isPaused = false;
    public bool isDied = false;
    void Start()
    {
        inGameCanvas.SetActive(true);
    }
        
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!isDied)
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            menuCanvas.SetActive(true);
            inGameCanvas.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            menuCanvas.SetActive(false);
            inGameCanvas.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void BossDied()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void PlayerDied()
    {
        isPaused=true;
        isDied=true;
        Time.timeScale = 0f;
        menuCanvas.SetActive(false);
        inGameCanvas.SetActive(false);
        DieCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void ResumeGame()
    {
        TogglePause();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(1f);
    //}
    //public void GameEnter()
    //{
    //    Player.LookUp();//�÷��̾ �ϴ��� �ٶ�
    //    fallingStone.SetActive(true);//�÷��̾����� ���� �߷��� ������ �޾� ������
    //    StartCoroutine(Wait());//1�� ���
    //    Player.transform.position = new Vector3(-5, 2, -11);//�÷��̾� ��ġ �̵�
    //    Player.isEntry = false;
    //    door.SetActive(true);
    //}

    public IEnumerator GameEnter()
    {
        Player.LookUp();
        fallingStone.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeOut());
        Player.transform.position = new Vector3(-5, 2, -11); 
        Player.isEntry = false;
        door.SetActive(true);
    }
    public void StartFadeOutEffect()
    {
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }

    //Ư��->normal
    private void MoveSpeedUp()
    {
        Player.setMoveSpeed(1.25f);
    }
    private void bulletSpeedUp()
    {
        Player.setBulletSpeed(1.25f);
    }
    private void FireInterval()
    {
        Player.setNextFireTime(0.75f);
    }

    private void GamblersRisk()
    {
        //50%Ȯ���� ü�� 1��
        //50%Ȯ���� ü�� 1�ٿ�
    }

    //Ư��->rare
    private void InvincibleTimeUp()
    {
        PSHP.InvincibleTimeUp();
    }
    private void ImFullingCharged()
    {
        PSHP.Heal(5);
    }
    private void SpikeBoots()
    {
        PSHP.SpikeImmuneOn();
    }
    private void AttackDamageUp()
    {
        //�� 1��
    }
    private void BeforeIDieDieDie()
    {
        //ü�� 1�� �� �� 2��
    }
    private void KillYourSelf()
    {
        //��� ���
    }
    private void HealthUp()
    {
        //�� 1 ��
    }
    private void revive()
    {
        //�ѹ� ������ �� 2ĭ�� ä�� ����
    }
    private void HighRiskHighReturn()
    {
        //�밭 �� 3�Ҹ� �� 2��
    }



}
