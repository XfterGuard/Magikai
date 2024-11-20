using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using DamageNumbersPro;
using TMPro;
using UnityEngine;
using GameCreator.Runtime.Characters;

[Serializable]
[Title("Basic Popup")]
[Category("Damage Numbers Pro/Basic Popup")]
[Description("Spawn a text or number popup using the Damage Numbers Pro asset.")]
[HideLabelsInEditor(false)]
public class GC2_DNP_Basic : Instruction
{
    public override string Title => "Basic Popup";

    //Main:
    public DamageNumber prefab;

    //Position:
    public PropertyGetGameObject target = GetGameObjectPlayer.Create();
    public bool useTargetChild = false;
    [ConditionShow("useTargetChild")]
    public string childName = "";
    public Vector3 relativeOffset = Vector3.zero;
    public bool hasRandomOffset = false;
    [ConditionShow("hasRandomOffset")]
    public Vector3 randomOffset = Vector3.zero;

    //Number:
    public bool hasNumber;
    [ConditionShow("hasNumber")]
    public bool randomNumber;
    [ConditionShow("hasNumber")]
    [ConditionHide("randomNumber")]
    public float number = 1;
    [ConditionShow("hasNumber", "randomNumber")]
    public float fromNumber = 1;
    [ConditionShow("hasNumber", "randomNumber")]
    public float toNumber = 3;
    [ConditionShow("hasNumber")]
    public bool roundNumber;
    [ConditionShow("hasNumber")]
    public bool hideMinus;

    //Text:
    [Space]
    public bool modifyText = false;
    [ConditionShow("modifyText")]
    public bool hasLeftText;
    [ConditionShow("modifyText", "hasLeftText")]
    public string leftText;
    [ConditionShow("modifyText")]
    public bool hasRightText;
    [ConditionShow("modifyText", "hasRightText")]
    public string rightText;
    [ConditionShow("modifyText")]
    public bool hasTopText;
    [ConditionShow("modifyText", "hasTopText")]
    public string topText;
    [ConditionShow("modifyText")]
    public bool hasBottomText;
    [ConditionShow("modifyText", "hasBottomText")]
    public string bottomText;
    //Text Visuals:
    [Header("Visual:")]
    public bool changeFont;
    [ConditionShow("changeFont")]
    public TMP_FontAsset font;
    public bool changeColor;
    [ConditionShow("changeColor")]
    public Color color = Color.white;
    public bool randomColor;
    [ConditionShow("randomColor")]
    public Gradient colorGradient;
    public bool vertexColor;
    [ConditionShow("vertexColor")]
    public VertexGradient vertexGradient = new VertexGradient(Color.white, new Color(0.8f, 0.8f, 0.8f, 1), Color.gray, Color.black);

    //Utiltiy:
    [Header("Ee")]
    public bool followTarget;
    public bool isGUI;
    [ConditionShow("isGUI")]
    public Vector2 anchoredPosition;

    protected override Task Run(Args args)
    {
        if (prefab != null)
        {
            //Get Transform:
            Transform positionTarget = this.target.Get(args).transform;
            if (useTargetChild)
            {
                Transform positionChild = positionTarget.Find(childName);
                if (positionChild != null)
                {
                    positionTarget = positionChild;
                }
            }

            //Position:
            Vector3 position = positionTarget.position + relativeOffset;
            if (hasRandomOffset)
            {
                position.x += (UnityEngine.Random.value * 2 - 1) * randomOffset.x;
                position.y += (UnityEngine.Random.value * 2 - 1) * randomOffset.y;
                position.z += (UnityEngine.Random.value * 2 - 1) * randomOffset.z;
            }

            //Spawn:
            DamageNumber newPopup = prefab.Spawn(position);

            //Number:
            if (hasNumber)
            {
                newPopup.enableNumber = true;

                newPopup.number = randomNumber ? UnityEngine.Random.Range(fromNumber, toNumber) : number;
                if (hideMinus)
                {
                    newPopup.number = Mathf.Abs(newPopup.number);
                }
                if (roundNumber)
                {
                    newPopup.number = Mathf.Round(newPopup.number);
                }
            }
            else
            {
                newPopup.enableNumber = false;
            }

            //Text:
            if (modifyText)
            {
                newPopup.enableLeftText = hasLeftText;
                if (hasLeftText)
                {
                    newPopup.leftText = leftText;
                }

                newPopup.enableRightText = hasRightText;
                if (hasRightText)
                {
                    newPopup.rightText = rightText;
                }

                newPopup.enableTopText = hasTopText;
                if (hasTopText)
                {
                    newPopup.topText = topText;
                }

                newPopup.enableBottomText = hasBottomText;
                if (hasBottomText)
                {
                    newPopup.bottomText = bottomText;
                }
            }

            //Font:
            if (changeFont)
            {
                newPopup.SetFontMaterial(font);
            }

            //Color:
            if (changeColor)
            {
                newPopup.SetColor(color);
            }

            //Random Color:
            if (randomColor)
            {
                newPopup.SetRandomColor(colorGradient);
            }

            //Vertex Color:
            if (vertexColor)
            {
                newPopup.SetGradientColor(vertexGradient);
            }

            //Follow:
            if (followTarget && positionTarget != null)
            {
                newPopup.SetFollowedTarget(positionTarget);
            }

            //GUI:
            if (isGUI)
            {
                newPopup.SetAnchoredPosition(positionTarget, anchoredPosition);
            }
        }
        else
        {
            Debug.Log("Damage number prefab is missing.");
        }

        return DefaultResult;
    }

    //Copy Past

}
