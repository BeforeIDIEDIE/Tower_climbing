    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TurretManager : MonoBehaviour
    {
        public Transform turretBase;  //��ž ��ü
        public Transform turretHead;  //��ž �Ӹ�
        public Transform firePos;     //�Ѿ� ��ġ
        public float detectionRadius = 20f;
        public float rotationSpeed = 1f;
        public float fireRange = 15f; //����ĳ��Ʈ �Ÿ�
        public float fireInterval = 5f; //�߻� ����
        public string playerTag = "Player";
        public float FireForce = 20f;
        public GameObject bullet;

        private Transform player;

        private float lastFireTime;

        void Update()
        {
            DetectAndTrackPlayer();
            CheckAndFire();
        }

        void DetectAndTrackPlayer()
        {
            Collider[] hitColliders = Physics.OverlapSphere(turretBase.position, detectionRadius);//���� �����Ͽ� �ش� �� ���� �����ϴ� ��ü ����
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag(playerTag)&&hitCollider.transform.position.y>=turretBase.position.y)
                {
                    player = hitCollider.transform;
                    AimAtPlayer();
                    return;
                }
            }
            player = null;
        }

        void AimAtPlayer()//������ �÷��̾ ����
        {
            if (player != null)
            {
                Vector3 targetDirection = player.position - turretHead.position;
                targetDirection.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                turretHead.rotation = Quaternion.Slerp(turretHead.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }

        void CheckAndFire()
        {
            if (player != null && Time.time - lastFireTime >= fireInterval)
            {
                RaycastHit hit;
                if (Physics.Raycast(firePos.position, firePos.forward, out hit, fireRange))
                {
                    
                    if (hit.collider.CompareTag(playerTag))
                    {
                    Debug.Log("hit");
                    Fire();
                        lastFireTime = Time.time;
                    }
                }
            }
        }

        void Fire()
        {
            GameObject newbullet = Instantiate(bullet, firePos.position, firePos.rotation);
            Rigidbody rb = newbullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firePos.forward * FireForce, ForceMode.Impulse); 
            }
            Debug.Log("Fire!"); 
        }

        void OnDrawGizmosSelected()
        {
            if (turretBase != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(turretBase.position, detectionRadius);
            }

            if (firePos != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(firePos.position, firePos.forward * fireRange);
            }
        }
    }
