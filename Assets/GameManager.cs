using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public PlayerMovement Player;
    public PlayerHealthSystem PSHP;
    public GameObject fallingStone;
    public GameObject door;
    public Image fadeImage; // 페이드 효과에 사용할 UI 이미지
    public float fadeDuration = 1f; // 페이드 효과의 지속 시간
    void Start()
    {

    }
        
    void Update()
    {

    }

    //IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(1f);
    //}
    //public void GameEnter()
    //{
    //    Player.LookUp();//플레이어가 하늘을 바라봄
    //    fallingStone.SetActive(true);//플레이어위의 공이 중력의 영향을 받아 떨어짐
    //    StartCoroutine(Wait());//1초 대기
    //    Player.transform.position = new Vector3(-5, 2, -11);//플레이어 위치 이동
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

    //특성->normal
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
        //50%확률로 체력 1업
        //50%확률로 체력 1다운
    }

    //특성->rare
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
        //공 1업
    }
    private void BeforeIDieDieDie()
    {
        //체력 1일 시 공 2업
    }
    private void KillYourSelf()
    {
        //즉시 사망
    }
    private void HealthUp()
    {
        //피 1 업
    }
    private void revive()
    {
        //한번 죽을시 피 2칸인 채로 생존
    }
    private void HighRiskHighReturn()
    {
        //대강 피 3소모 공 2업
    }



}
