using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement Player;
    public PlayerHealthSystem PSHP;
    void Start()
    {

    }

    void Update()
    {

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
    private void LuckUp()
    {
        //헬스킷 드랍율++;
    }

    private void GamblersRisk()
    {
        //50%확률로 체력 1업
        //50%확률로 체력 1다운
    }

    private void CostDown()
    {
        //특성을 얻기위해 필요한 적 처치양 감소 ->대강 10 감소하면 될듯
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
