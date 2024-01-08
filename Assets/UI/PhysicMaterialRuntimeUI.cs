using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace LlamAcademy.UI
{
    public class PhysicMaterialRuntimeUI : VisualElement
    {
        private static VisualTreeAsset VisualTree;
        public new class UxmlFactory : UxmlFactory<PhysicMaterialRuntimeUI> {}
        public PhysicMaterialRuntimeUI() {}

        private Label Title => this.Q<Label>("header");
        private Slider StaticFrictionSlider => this.Q<Slider>("static-friction");
        private Slider DynamicFrictionSlider => this.Q<Slider>("dynamic-friction");
        private Slider BouncinessSlider => this.Q<Slider>("bounciness");
        private EnumField FrictionCombineField => this.Q<EnumField>("friction-combine");
        private EnumField BouncinessCombineField => this.Q<EnumField>("bounciness-combine");

        public PhysicMaterialRuntimeUI(PhysicMaterial material, string label = "")
        {
            Init(material, label);
        }

        public void Init(PhysicMaterial material, string label = "")
        {
            if (VisualTree == null)
            {
                VisualTree = Resources.Load<VisualTreeAsset>("UI/Templates/Physic Material Control");
            }
            VisualTree.CloneTree(this);

            StaticFrictionSlider.value = material.staticFriction;
            DynamicFrictionSlider.value = material.dynamicFriction;
            BouncinessSlider.value = material.bounciness;
            FrictionCombineField.value = material.frictionCombine;
            BouncinessCombineField.value = material.bounceCombine;
            
            StaticFrictionSlider.RegisterCallback<ChangeEvent<float>>(evt => material.staticFriction = evt.newValue);
            DynamicFrictionSlider.RegisterCallback<ChangeEvent<float>>(evt => material.dynamicFriction = evt.newValue);
            BouncinessSlider.RegisterCallback<ChangeEvent<float>>(evt => material.bounciness = evt.newValue);
            FrictionCombineField.RegisterCallback<ChangeEvent<Enum>>(evt => material.frictionCombine = (PhysicMaterialCombine) evt.newValue);
            BouncinessCombineField.RegisterCallback<ChangeEvent<Enum>>(evt => material.bounceCombine = (PhysicMaterialCombine) evt.newValue);
            Title.text = label;
        }
    }
}