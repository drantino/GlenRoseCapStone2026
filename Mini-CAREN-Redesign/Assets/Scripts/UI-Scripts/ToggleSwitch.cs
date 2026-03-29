using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{
    [Header("Slider Setup")]
    [SerializeField, Range(0, 1f)] private float sliderValue;
    public bool CurrentValue { get; private set; }
    private bool previousValue;

    private Slider _slider;

    [Header("Animation")]
    [SerializeField, Range(0, 1f)] private float animaitonDuration = 0.25f;
    [SerializeField]
    private AnimationCurve slideEase =
        AnimationCurve.EaseInOut(timeStart: 0, valueStart: 0, timeEnd: 1, valueEnd: 1);

    private Coroutine _animateSliderCoroutine;

    [Header("Events")]
    [SerializeField] private UnityEvent onToggleOn;
    [SerializeField] private UnityEvent onToggleOff;

    private ToggleSwitchGroupManager _toggleSwitchGroupManager;

    protected void OnValidate()
    {
        SetupToggleComponents();

        _slider.value = sliderValue;
    }

    private void SetupToggleComponents()
    {
        if (_slider != null)
            return;

        SetupSliderComponent();
    }

    private void SetupSliderComponent()
    {
        _slider = GetComponent<Slider>();

        if (_slider == null)
        {
            Debug.Log(message: "No slider found!", context: this);
            return;
        }

        _slider.interactable = false;
        var sliderColors = _slider.colors;
        sliderColors.disabledColor = Color.white;
        _slider.colors = sliderColors;
        _slider.transition = Selectable.Transition.None;
    }

    public void SetupForManager(ToggleSwitchGroupManager manager)
    {
        _toggleSwitchGroupManager = manager;
    }

    private void Awake()
    {
        SetupToggleComponents();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    private void Toggle()
    {
        if (_toggleSwitchGroupManager == null)
            SetStateAndStartAnimation(!CurrentValue);
    }

    public void ToggleByGroupManager(bool valueToSetTo)
    {
        SetStateAndStartAnimation(valueToSetTo);
    }

    private void SetStateAndStartAnimation(bool state)
    {
        previousValue = CurrentValue;
        CurrentValue = state;

        if (previousValue != CurrentValue)
        {
            if (CurrentValue)
                onToggleOn?.Invoke();
            else
                onToggleOff?.Invoke();
        }

        if (_animateSliderCoroutine != null)
            StopCoroutine(_animateSliderCoroutine);

        _animateSliderCoroutine = StartCoroutine(routine: AnimateSlider());
    }

    private IEnumerator AnimateSlider()
    {
        float startValue = _slider.value;
        float endValue = CurrentValue ? 1 : 0;

        float time = 0;
        if (animaitonDuration > 0)
        {
            while (time < animaitonDuration)
            {
                time += Time.deltaTime;

                float lerpFactor = slideEase.Evaluate(time: time / animaitonDuration);
                _slider.value = sliderValue = Mathf.Lerp(a: startValue, b: endValue, t: lerpFactor);

                yield return null;
            }
        }

        _slider.value = endValue;
    }

}

public class ToggleSwitchGroupManager : MonoBehaviour
{
    public void ToggleGroup(ToggleSwitch toggleSwitch)
    {

    }
}