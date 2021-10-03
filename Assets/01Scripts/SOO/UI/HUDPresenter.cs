using UnityEngine;
using UnityEngine.UI;

public class HUDPresenter : MonoBehaviour
{
    private UIElement<HealthBar> HealthBar;
    private UIElement<Text> SoulText;
    private UIElement<Slider> WaterBar;

    public void Awake()
    {
        SoulText = new UIElement<Text>("SoulText", this.gameObject);
        HealthBar = new UIElement<HealthBar>("HealthBar", this.gameObject);

        WaterBar = new UIElement<Slider>("WaterBar", this.gameObject);
        
        ParticleSystem soulParticle = SoulText.Component.transform.GetComponentInChildren<ParticleSystem>();

        PlayerStats.soulCallback +=
            (int _value) => SoulText.Component.text = _value.ToString();
        PlayerStats.soulCallback +=
            (int _value) => soulParticle.Play();

        WaterGunModel.maxWaterAmountCallback +=
            (int _value) => WaterBar.Component.maxValue = _value;
        WaterGunModel.currentWaterAmountCallback +=
            (int _value) => WaterBar.Component.value = _value;

        PlayerStats.maxHpCallback += HealthBar.Component.SizeChange;
        PlayerStats.currentHpCallback += HealthBar.Component.SetInsideGraphic;
    }

}
// TODO : �̰� �س��� (�̸�)
// FIXME : �̰� ���ĳ��� (�̸�)
// XXX : ���ߴ�. (�̸�)  <<���� ���� ���ľ��� ġ����
