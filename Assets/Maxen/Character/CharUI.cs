using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharUI : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer HeadRenderer;
    [SerializeField] protected SpriteRenderer ArmRendererLeft;
    [SerializeField] protected SpriteRenderer ArmRendererRight;
    [SerializeField] protected SpriteRenderer HandRendererLeft;
    [SerializeField] protected SpriteRenderer HandRendererRight;
    [SerializeField] protected SpriteRenderer NeckRenderer;
    [SerializeField] protected SpriteRenderer LegRendererLeft;
    [SerializeField] protected SpriteRenderer LegRendererRight;
    [SerializeField] protected SpriteRenderer HairRenderer;
    [SerializeField] protected SpriteRenderer FaceRenderer;
    [SerializeField] protected SpriteRenderer ShirtBaseRenderer;
    [SerializeField] protected SpriteRenderer ShirtSleeveRendererLeft;
    [SerializeField] protected SpriteRenderer ShirtSleeveRendererRight;
    [SerializeField] protected SpriteRenderer PantsWaistRenderer;
    [SerializeField] protected SpriteRenderer PantsLegRendererLeft;
    [SerializeField] protected SpriteRenderer PantsLegRendererRight;
    [SerializeField] protected SpriteRenderer ShoesRendererLeft;
    [SerializeField] protected SpriteRenderer ShoesRendererRight;

    public void SetAppearance(CharacterGen.Appearance Appearance)
    {
        HeadRenderer.sprite = Appearance.Body.Head;
        ArmRendererLeft.sprite = Appearance.Body.Arm;
        ArmRendererRight.sprite = Appearance.Body.Arm;
        HandRendererLeft.sprite = Appearance.Body.Hand;
        HandRendererRight.sprite = Appearance.Body.Hand;
        NeckRenderer.sprite = Appearance.Body.Neck;
        LegRendererLeft.sprite = Appearance.Body.Leg;
        LegRendererRight.sprite = Appearance.Body.Leg;
        HairRenderer.sprite = Appearance.Hair;
        FaceRenderer.sprite = Appearance.Face;
        ShoesRendererLeft.sprite = Appearance.Shoes;
        ShoesRendererRight.sprite = Appearance.Shoes;

        //Shirt
        ShirtBaseRenderer.sprite = Appearance.Shirt.Base;
        ShirtSleeveRendererLeft.sprite = Appearance.Shirt.Limb;
        ShirtSleeveRendererRight.sprite = Appearance.Shirt.Limb;
        ShirtBaseRenderer.color = Appearance.Shirt.Col;
        ShirtSleeveRendererLeft.color = Appearance.Shirt.Col;
        ShirtSleeveRendererRight.color = Appearance.Shirt.Col;

        //Pants
        PantsWaistRenderer.sprite = Appearance.Pants.Base;
        PantsLegRendererLeft.sprite = Appearance.Pants.Limb;
        PantsLegRendererRight.sprite = Appearance.Pants.Limb;
        PantsWaistRenderer.color = Appearance.Pants.Col;
        PantsLegRendererLeft.color = Appearance.Pants.Col;
        PantsLegRendererRight.color = Appearance.Pants.Col;
    }
}
