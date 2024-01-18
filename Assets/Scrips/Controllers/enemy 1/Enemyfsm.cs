using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyfsm : MonoBehaviour
{
    public Transform right;//���˻�����������֮���ж�����Զ���ᳬ����������
    public Transform left;
    public Transform ancher;
    public string status;
    private bool needChangeStatus;
    private GameObject player;
    public float speed;//�����˶����ٶ�
    private Animator enenmyAnimator;
    public int enemyhp;//����Ѫ����ÿ�ο�һ��
    public float attackZone;
    private bool needToAncher;
    private bool inAncher;
    private Rigidbody2D rigidbody2D;
    public GameObject AttackTrigger;


    public void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        needChangeStatus = false;
        enenmyAnimator = this.gameObject.GetComponent<Animator>();
        status = "idle";
        StartCoroutine(idle());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameFacade.Instance.gameState == GameState.Start)
        {
            if (enemyhp <= 0)
            {
                //���ٵ���
            }
            if (needChangeStatus)
            {
                if (status == "idle")
                {
                    StopAllCoroutines();
                    StartCoroutine(idle());
                    needChangeStatus = false;
                }
                else if (status == "attack")
                {
                    StopAllCoroutines();
                    StartCoroutine(attack());
                    needChangeStatus = false;
                }
                else if (status == "move")
                {
                    StopAllCoroutines();
                    StartCoroutine(move());
                    needChangeStatus = false;
                }
            }
        }
        if (enemyhp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void AttackStart()
    {
        AttackTrigger.SetActive(true);
    }
    public void AttackEnd()
    {
        AttackTrigger.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            needChangeStatus = true;
            status = "attack";
            rigidbody2D.velocity = Vector2.zero;
            PlayerController player = this.player.GetComponent<PlayerController>();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController player = this.player.GetComponent<PlayerController>();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            needChangeStatus = true;
            status = "move";
            PlayerController player = this.player.GetComponent<PlayerController>();
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            status = "move";
            needChangeStatus = true;
            needToAncher = false;
            inAncher = false;
        }
        if (other.gameObject.tag == "Skill")
        {
            enemyhp -= 1;
            GameObject.Find("Player").GetComponent<PlayerController>().HitEnemy();
        }
        else if (other.gameObject.tag == "block")
        {
            needToAncher = true;
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) > attackZone)
            {
                enenmyAnimator.SetBool("attack", false);
            }
            player = other.gameObject;
            status = "move";
            needChangeStatus = true;
            needToAncher = false;
            inAncher = false;
        }
        else if (other.gameObject.tag == "block")
        {
            needToAncher = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            needChangeStatus = true;
            status = "idle";
        }
    }
    IEnumerator move()
    {
        while (true)
        {
            //�������
            if (Vector3.Distance(transform.position,player.transform.position)<= attackZone)//distance <= attackZone
            {
                status = "attack";
                needChangeStatus = true;
            }

            else if (needToAncher)//�ߵ��߽���
            {
                if (transform.position.x - ancher.transform.position.x >= 0)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                Debug.Log("����ê��");

                if(Vector3.Distance(transform.position, player.transform.position)<=0.1)
                {
                    inAncher=true;  
                }
                if (inAncher)//�ߵ�ê����
                {
                    status = "idle";
                    needChangeStatus = true;
                    enenmyAnimator.SetBool("move", false);
                    enenmyAnimator.SetBool("idle", true);
                    enenmyAnimator.SetBool("attack", false);
                    needToAncher = false;
                }
            }
            else 
            {
                if (transform.position.x - player.transform.position.x >= 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                enenmyAnimator.SetBool("move", true);
                Debug.Log("׷�����");
                Vector2 direction = (player.transform.position - transform.position).normalized;
                Vector2 pos = new Vector2(direction.x, 0);
                rigidbody2D.velocity = pos * speed;
                
            }
            

            yield return null;
        }
    }

    IEnumerator idle()
    {
        while (true)
        {
            enenmyAnimator.SetBool("move", false);
            yield return null;
        }
    }

    IEnumerator attack()
    {
        while (true)
        {
            enenmyAnimator.SetBool("attack", true);
            if (player.transform.position.x-transform.position.x> attackZone)
            {
                Debug.Log("�л�����");
                needChangeStatus = true;
                enenmyAnimator.SetBool("move", true);
                enenmyAnimator.SetBool("attack", false);
                status = "move";
            }
            yield return null;
        }
    }
}
