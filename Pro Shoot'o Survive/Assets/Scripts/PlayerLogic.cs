using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    static private PlayerLogic instance;
    static public PlayerLogic Instance { get { return instance; } }

    [SerializeField] float hpMax;
    [HideInInspector] public float actuallyMaxHp;

    [SerializeField] private float hp;
    public float HP { get { return hp; } set { hp = Mathf.Clamp(hp + value, 0, hpMax); } }
    [SerializeField] private float armor;
    [SerializeField] private float armorMax;
    public float Armor { get { return armor; } set { armor = Mathf.Clamp(armor + value, 0, armorMax); UpdateArmorText(); } }


    private float damageMultiplier = 1f;

    public float speedMultiplier = 1f;
    public float speed;

    public float speedBarValue;     //value from 0 to 1
    public float healthBarValue;    //value from 0 to 1
    public float damageBarValue;    //value from 0 to 1

    [SerializeField] TMP_Text armorValueText;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        actuallyMaxHp = hpMax;
        hp = actuallyMaxHp;
        BarsManager.Instance.playerRef = this;
    }

    // Update is called once per frame
    void Update()
    {
        BarsManager.Instance.setHpBar(hp / actuallyMaxHp);
        UpdateStatsMultiplier();
    }

    public void TakeDamage(float damage)
    {
        if (armor > 0)
        {
            armor -= damage * damageMultiplier;
            armor = Mathf.Clamp(armor, 0, armorMax);
            UpdateArmorText();
        }
        else
        {
            hp -= damage * damageMultiplier;
        }

        Debug.Log("Hp: " + hp);

        if (hp <= 0)
        {
            Destroy(gameObject);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(2);
        }

    }

    public void Heal(float HealAmount)
    {
        hp = Mathf.Clamp(hp + healthBarValue, 0, actuallyMaxHp);
        BarsManager.Instance.setHpBar(hp / actuallyMaxHp);
    }

    public void UpdateArmorText()
    {
        armorValueText.text = armor.ToString();
    }
    public void UpdateStatsMultiplier()
    {
        //multipliers affects player' speed or damage taken
        if (damageBarValue <= 0)
        {
            damageMultiplier = 2f;
        }
        else if (damageBarValue >= 1)
        {
            damageMultiplier = 0.5f;
        }
        else
        {
            damageMultiplier = 1f;
        }

        if (speedBarValue <= 0)
        {
            speedMultiplier = 0.5f;
        }
        else if (speedBarValue >= 1)
        {
            speedMultiplier = 2f;
        }
        else
        {
            speedMultiplier = 1f;
        }

    }

    private void OnDestroy()
    {
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
            //SceneManager.LoadScene(2);
    }

}
