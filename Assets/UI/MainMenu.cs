using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace LlamAcademy.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class MainMenu : MonoBehaviour
    {
        private UIDocument MainDocument;

        [Header("Scene References")] [SerializeField]
        private Rigidbody[] Balls;

        [Header("Material References")] [SerializeField]
        private PhysicMaterial[] WorldPhysicMaterials;

        [SerializeField] private PhysicMaterial[] DynamicPhysicMaterials;

        private VisualElement WorldMaterialParent;
        private VisualElement DynamicMaterialParent;
        private Button ResetButton;
        private Button StartButton;
        private IntegerField ForceField;

        [SerializeField]
        private Vector3[] StartPositions;

        private void Awake()
        {
            MainDocument = GetComponent<UIDocument>();
            WorldMaterialParent = MainDocument.rootVisualElement.Q<VisualElement>("world-material-container");
            DynamicMaterialParent = MainDocument.rootVisualElement.Q<VisualElement>("dynamic-material-container");
            ResetButton = MainDocument.rootVisualElement.Q<Button>("reset");
            StartButton = MainDocument.rootVisualElement.Q<Button>("start");
            ForceField = MainDocument.rootVisualElement.Q<IntegerField>("force-field");

            ResetButton.RegisterCallback<ClickEvent>(Reset);
            StartButton.RegisterCallback<ClickEvent>(AddForceToBalls);
        }

        private void AddForceToBalls(ClickEvent _)
        {
            foreach (Rigidbody ball in Balls)
            {
                ball.useGravity = true;
                ball.AddForce(Vector3.right * ForceField.value);
            }
        }

        private void Reset(ClickEvent _)
        {
            for (int i = 0; i < Balls.Length; i++)
            {
                Rigidbody ball = Balls[i];
                ball.velocity = Vector3.zero;
                ball.angularVelocity = Vector3.zero;
                ball.useGravity = false;
                ball.transform.position = StartPositions[i];
            }
        }

        private void Start()
        {
            foreach (PhysicMaterial material in WorldPhysicMaterials)
            {
                PhysicMaterialRuntimeUI ui = new(material, material.name);
                WorldMaterialParent.Add(ui);
            }

            foreach (PhysicMaterial material in DynamicPhysicMaterials)
            {
                PhysicMaterialRuntimeUI ui = new(material, material.name);
                DynamicMaterialParent.Add(ui);
            }
        }
    }
}