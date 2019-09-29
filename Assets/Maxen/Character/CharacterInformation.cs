using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation : MonoBehaviour
{
    [System.Serializable]
    public struct Character
    {
        public int CharID;
        public string Name;

        public static Character Empty = new Character { CharID = -1, Name = "Random" };
    }

    protected Character _info = Character.Empty;

    public CharacterGen.Appearance Appearance
    {
        get;
        protected set;
    }
    [SerializeField] protected SpriteRenderer HeadRenderer;
    [SerializeField] protected SpriteRenderer ArmRendererLeft;
    [SerializeField] protected SpriteRenderer ArmRendererRight;
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

    public Character GetInfo()
    {
        //return Character.Empty;
        return _info;
    }
    
    public void AssignCharacter(Character newChar)
    {
        _info = newChar;
    }

    public void GenerateFeatures()
    {
        /*//If CharID >= 0
        int ranIndex = Random.Range(0, GameManager.Instance.AvailableCharFeatures.Count);
        CharFeature selectedFeature = GameManager.Instance.AvailableCharFeatures[ranIndex];

        //If this character is a celeb, remove that feature from the available features list
        if (_info.CharID >= 0)
        {
            GameManager.Instance.AvailableCharFeatures.RemoveAt(ranIndex);
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr)
        {
            sr.color = selectedFeature.BodyColor;
        }

        //Useful for later:
        //Random.InitState(_info.CharID);
        //I'll use this for picking char color instead of having it be a CharFeature. CharFeature is supposed to be stuff like clothes.
        */

        //New generation
        Appearance = GameManager.Instance.CharacterGenerator.GenerateAppearance(_info.CharID);

        LoadAppearance();
    }

    protected void LoadAppearance()
    {
        HeadRenderer.sprite = Appearance.Body.Head;
        ArmRendererLeft.sprite = Appearance.Body.Arm;
        ArmRendererRight.sprite = Appearance.Body.Arm;
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
