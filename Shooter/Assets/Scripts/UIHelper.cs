using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// <summary>
/// Handles things related to the canvas UI
/// </summary>
public class UIHelper : MonoBehaviour
{
    public FloatVariable scoreVar;
    public FloatVariable healthVar;

    public EventListenerSimple targetHitListener = new EventListenerSimple();
    public EventListenerSimple playerHitListener = new EventListenerSimple();
    public EventListenerBool gameActiveEventListener;

    public EventRaiserBool gameActiveRaiser = new EventRaiserBool();

    public Text scoreText;
    public Slider healthSlider;
    public Image sliderImage;

    public GameObject gameOverUI;
    public Text finalScoreText;

    void Start()
    {
        sliderImage.color = Color.Lerp(Color.red, Color.green, healthSlider.value / 20);
        UpdateScore();
    }

    private void OnEnable()
    {
        targetHitListener.RegisterListener();
        targetHitListener.onEventRaisedAction = UpdateScore;

        playerHitListener.RegisterListener();
        playerHitListener.onEventRaisedAction = UpdateHealth;

        gameActiveEventListener.RegisterListener();
        gameActiveEventListener.onEventRaisedAction = GameStatusChanged;
    }

    private void OnDisable()
    {
        targetHitListener.UnregisterListener();
        playerHitListener.UnregisterListener();
    }

    void UpdateScore()
    {
        scoreText.text = string.Format("Score: {0}", scoreVar.Value.ToString());
    }

    // <summary>
    /// Update health after player is hit, and update the UI to reflect this.
    /// </summary>
    void UpdateHealth()
    {
        healthVar.Value -= 10;
        healthSlider.value = healthVar.Value;
        sliderImage.color = Color.Lerp(Color.red, Color.green, healthSlider.value / 20);

        if (healthVar.Value <= 0)
        {
            gameActiveRaiser.RaiseEvent(false);
            finalScoreText.text = string.Format("Score: {0}", scoreVar.Value);
            gameOverUI.SetActive(true);
        }
    }

    // <summary>
    /// Handles changes to the game state
    /// </summary>
    private void GameStatusChanged(bool isActive)
    {
        if(isActive)
        {
            scoreVar.Value = 0;
            healthVar.Value = 100;
            healthSlider.value = 100;
            sliderImage.color = Color.Lerp(Color.red, Color.green, healthSlider.value / 20);
            gameOverUI.SetActive(false);
        }
    }
}
