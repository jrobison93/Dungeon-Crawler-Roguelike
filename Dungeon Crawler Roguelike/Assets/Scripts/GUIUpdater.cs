using UnityEngine;
using UnityEngine.UI;

public class GUIUpdater : MonoBehaviour, Observer
{
    private Slider healthSlider;
    private Slider manaSlider;
    private int numKills;
    private Text killsText;
    public static GUIUpdater instance = null;

    // Use this for initialization
    void Start ()
    {
        numKills = 0;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SetupElements();
    }

    private void SetupElements()
    {
        GameObject health = GameObject.FindGameObjectWithTag("HealthSlider");
        healthSlider = health.GetComponent<Slider>();

        GameObject mana = GameObject.FindGameObjectWithTag("ManaSlider");
        manaSlider = mana.GetComponent<Slider>();

        GameObject kills = GameObject.FindGameObjectWithTag("KillsText");
        killsText = kills.GetComponent<Text>();
        killsText.text = numKills.ToString();
    }

    public void UpdateObserver(Subject subject, object[] args)
    {
        if(subject.GetType().Equals(typeof(PlayerHealth)))
        {
            PlayerHealth playerHealth = (PlayerHealth)subject;
            healthSlider.value = playerHealth.Percentage();
        }
        else if(subject.GetType().Equals(typeof(PlayerMana)))
        {
            PlayerMana playerMana = (PlayerMana)subject;
            manaSlider.value = playerMana.Percentage();

        }
        else if(subject.GetType().Equals(typeof(EnemyHealth)))
        {
            EnemyHealth enemyHealth = (EnemyHealth)subject;
            if(enemyHealth.IsDepleted())
            {
                numKills++;
            }

        }
        killsText.text = numKills.ToString();
    }
}
