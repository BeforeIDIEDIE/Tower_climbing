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
    private void LuckUp()
    {
        //�ｺŶ �����++;
    }

    private void GamblersRisk()
    {
        //50%Ȯ���� ü�� 1��
        //50%Ȯ���� ü�� 1�ٿ�
    }

    private void CostDown()
    {
        //Ư���� ������� �ʿ��� �� óġ�� ���� ->�밭 10 �����ϸ� �ɵ�
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
