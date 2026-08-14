using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : MonoBehaviour
{
    private Label _timeCounter;
    private ProgressBar _experienceBar;
    private float _timer = 0;

    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        _timeCounter = uiDocument.rootVisualElement.Q("TimeCounter") as Label;
        _experienceBar = uiDocument.rootVisualElement.Q("ExperienceBar") as ProgressBar;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        MockExperienceGain();
    }

    private void UpdateTimer()
    {
        _timer += Time.deltaTime;
        if (_timeCounter != null)
        {
            _timeCounter.text = FormatTime(_timer);
        }
    }

    private string FormatTime(float seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return time.ToString(@"mm\:ss");
    }

    private void MockExperienceGain()
    {
        if (_experienceBar == null) return;

        float newVal = _experienceBar.value;
        newVal += Time.deltaTime;
        if (newVal >= 100)
        {
            newVal = 0;
        }
        _experienceBar.value = newVal;
    }
}
