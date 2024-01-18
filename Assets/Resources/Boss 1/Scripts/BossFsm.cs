using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFsm : MonoBehaviour
{

    public string status;
    private bool canchangebossitem=true;
    public int num;
    private bool needChangeStatus;
    private Animator enenmyAnimator;
    public int enemyhp;//����Ѫ����ÿ�ο�һ��
    public GameObject[] vines;
    public GameObject[] guns;
    public NormalLightRecive[] normallights;
    public BossBody[] bodys;
    public Animator eye;
    public GameObject round;
    private int currentVine;
    private float palsytime;
    public bool canBeAttack;
    private int beAttackCount;
    public bool is2s;
    private float lightTime;
    public bool setActive;
    private Collider2D trigger;
    private MyTimer timer = new MyTimer();
    public float shoottime;
    private bool finded;
    public CircleCollider2D maincol;
    public bool died;
    public GameObject Light;
    public bool findplayer;
    /// <summary>
    /// ��ʼ����������
    /// </summary>
    void Start()
    {
        palsytime = 0;
        setActive = false;
        lightTime = 0;
        currentVine = 0;
        enemyhp = 19;
        is2s = false;
        canBeAttack = false;
        beAttackCount = 0;
        needChangeStatus = false;
        for (int i = 0; i < vines.Length; i++)
        {
            if (vines[i] != null)
            {
                vines[i].transform.parent.GetComponent<Animator>().speed = 0.0f;
            }
        }
        enenmyAnimator = this.gameObject.GetComponent<Animator>();
       // status = "idle";
       // StartCoroutine(idle());
    }
    public void BackAttack()
    {
        needChangeStatus = true;
        status = "attack";
    }
    // Update is called once per frame
    void Update()
    {
        if (finded)
        {
            float time = shoottime;
            if (died)
            {
                needChangeStatus = true;
                status = "palsy";
            }
            if (enemyhp <= 4)
            {
                maincol.enabled = true;
            }
            if (enemyhp <= 0)
            {
                //�����ɼӸ�����
                // Destroy(gameObject,3);
            }
            if (shoottime > 5)
            {
                needChangeStatus = true;
                shoottime = 0;
                status = "palsy";
            }
            if (needChangeStatus)
            {
                if (status == "attack")
                {
                    StopAllCoroutines();
                    StartCoroutine(attack());
                    needChangeStatus = false;
                }
                else if (status == "palsy")
                {
                    StopAllCoroutines();
                    StartCoroutine(palsy());
                    needChangeStatus = false;
                }
                else if (status == "idle")
                {
                    StopAllCoroutines();
                    StartCoroutine(idle());
                    needChangeStatus = false;
                }
            }
            if (canBeAttack)
            {
                canchangebossitem = false;
            }
            if (canchangebossitem)
            {
                num = Random.Range(0, 4);
                canchangebossitem = false;
                normallights[num].canview = true;
                StartCoroutine(Randomlight());
            }
            if (time == shoottime)
            {
                Light.SetActive(false);
            }
        }
        if (findplayer&&finded==false)
        {
            needChangeStatus = true;
            finded = true;
            eye.SetBool("FindPlayer", true);
            GameFacade.Instance.GetComponent<AudioSource>().Play();
            status = "idle";
        }
    }
    IEnumerator Randomlight()
    {
        //��֮ǰ�����Ĺر�
        yield return new WaitForSecondsRealtime(5f);
        for (int i = 0; i < normallights.Length; i++)
            {
              normallights[i].canview = false;
            if (normallights[i].showed)
                {
                    normallights[i].Hide();
                }
            }
            normallights[num].Show();
            print(normallights[num]);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "light" && lightTime <= 2)
        {
            lightTime += Time.deltaTime;

        }
        else if (lightTime > 2)
        {
            //����̱��ģʽ
            is2s = true;

        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "skill" && canBeAttack)
        {
            if (beAttackCount <= 3)
            {
                enemyhp--;
                beAttackCount++;
            }

        }
    }


    IEnumerator idle()
    {
        Debug.Log("����ȴ�ģʽ");
        /*
        while (!setActive)
        {
            yield return null;
        }
        */
        Debug.Log("boss����");
        //�����ջ�
        for (int i = 0; i < vines.Length; i++)
        {
            if (vines[i] != null)
            {
                if (vines[i].transform.parent.GetComponent<Animator>().speed < 1)
                {
                    vines[i].transform.parent.GetComponent<Animator>().speed += 0.1f;
                }
            }
        }

        yield return new WaitForSeconds(3);
        /*
        while (!setActive)
        {
            yield return null;
        }
        */
        //���빥��״̬
        needChangeStatus = true;
        status = "attack";
       

    }

    IEnumerator palsy()
    {
        //�۾�����
        foreach (var item in normallights)
        {
            item.Hide();
        }
        shoottime = 0f;
        canBeAttack = true;
        eye.SetBool("CanBeAttack", true);
        for (int i = 0; i < vines.Length; i++)
        {
            if (vines[i] != null)
            {
                if (vines[i].transform.parent.GetComponent<Animator>().speed >= 0)
                {
                    vines[i].transform.parent.GetComponent<Animator>().speed -= 0.1f;
                }
            }
        }
        if (enemyhp<=0)
        {
            canBeAttack=false;
            GameObject.Destroy(gameObject,4);
        }

        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(false);
        }
        yield return new WaitForSecondsRealtime(12f);
        needChangeStatus = true;
        status = "attack";
        /*
        while (true)
        {
            palsytime += Time.deltaTime;
            Debug.Log("̱����");
            if (beAttackCount >= 3 || palsytime >= 10)//�������������λ���ʱ�䳬��10��
            {
                status = "idle";
             //   eye.SetBool("CanBeAttack", false);
                needChangeStatus = true;
                if (beAttackCount >= 3)
                {
                    vines[currentVine].AddComponent<Rigidbody2D>();
                    //�Ѷ���ͣ��
                    vines[currentVine].transform.parent.GetComponent<Animator>().speed = 0.05f;
                    GameObject.Destroy(vines[currentVine].transform.parent.gameObject, 4);

                }

                currentVine++;
                canBeAttack = false;

                if (palsytime >= 10)
                {
                    enemyhp += beAttackCount;
                }
                beAttackCount = 0;
                palsytime = 0;
            }

            yield return null;
        }
        */
    }

    IEnumerator attack()
    {
        normallights[num].canview = false;
        normallights[num].Hide();
        canBeAttack = false;
        canchangebossitem = true;
        eye.SetBool("CanBeAttack", false);
        for (int i = 0; i < vines.Length; i++)
        {
            if (vines[i] != null)
            {
                if (vines[i].transform.parent.GetComponent<Animator>().speed < 1)
                {
                    vines[i].transform.parent.GetComponent<Animator>().speed += 0.1f;
                }
            }
        }
        //����̨����
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(true);
           // yield return new WaitForSeconds(1f);
        }
        //�۾�����




        //�������

        while (true)
        {
            Debug.Log("���ڹ���");
            round.transform.Rotate(Vector3.forward * 2);
            yield return null;
        }
    }



    /// <summary>
    /// ������
    /// </summary>
    public void butattack()
    {
        beAttackCount++;
        enemyhp--;
    }

    /// <summary>
    /// ���ô˷�������boss����ٴ�֮ǰ���Խ��й�����̸�ͽ�ѧ
    /// </summary>
    public void SetBossActive()
    {
        setActive = true;
    }
}
