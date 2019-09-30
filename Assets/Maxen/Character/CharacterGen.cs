using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterGenerator", menuName = "New Character Generator")]
public class CharacterGen : ScriptableObject
{
    [System.Serializable]
    public struct BodyCollection
    {
        public Sprite Head;
        public Sprite Arm;
        public Sprite Hand;
        public Sprite Neck;
        public Sprite Leg;
    }
    [System.Serializable]
    public struct LimbedClothing
    {
        public Sprite Base;
        public Sprite Limb;
        public Color Col;
    }

    [System.Serializable]
    public struct Appearance
    {
        public BodyCollection Body;

        public Sprite Hair;
        public Sprite Face;

        public LimbedClothing Shirt;
        public LimbedClothing Pants;

        public Sprite Shoes;
    }

    [SerializeField] protected BodyCollection[] BodyOptions;
    [SerializeField] protected Sprite[] FacesOptions;
    [SerializeField] protected Sprite[] HairOptions;
    [SerializeField] protected Sprite[] MasculineShirtBaseOptions;
    [SerializeField] protected Sprite[] FeminineShirtBaseOptions;
    [SerializeField] protected Sprite[] ShirtSleeveOptions;
    [SerializeField] protected Sprite[] PantsWaistOptions;
    [SerializeField] protected Sprite[] PantsLegOptions;
    [SerializeField] protected Sprite[] ShoeOptions;

    public BodyCollection[] AllBodyOptions { get { return BodyOptions; } }
    public Sprite[] AllFaceOptions { get { return FacesOptions; } }
    public Sprite[] AllHairOptions { get { return HairOptions; } }
    public Sprite[] AllMasculineShirtBaseOptions { get { return MasculineShirtBaseOptions; } }
    public Sprite[] AllFeminineShirtBaseOptions { get { return FeminineShirtBaseOptions; } }
    public Sprite[] AllShirtSleeveOptions { get { return ShirtSleeveOptions; } }
    public Sprite[] AllPantsWaistOptions { get { return PantsWaistOptions; } }
    public Sprite[] AllPantsLegOptions { get { return PantsLegOptions; } }
    public Sprite[] AllShoeOptions { get { return AllShoeOptions; } }

    public Appearance GenerateAppearance(int CharacterID)
    {
        System.Random rng;
        if (CharacterID > 0)
        {
            rng = new System.Random(CharacterID);
        }
        else
        {
            rng = new System.Random();
        }

        //Pick a shirt
        //
        bool isFeminine = rng.Next(0, 3) == 0;
        LimbedClothing shirt = new LimbedClothing()
        {
            Base = isFeminine ? FeminineShirtBaseOptions[Random.Range(0, FeminineShirtBaseOptions.Length)] : MasculineShirtBaseOptions[Random.Range(0, MasculineShirtBaseOptions.Length)],
            Limb = ShirtSleeveOptions[Random.Range(0, ShirtSleeveOptions.Length)],
            Col = Random.ColorHSV()
        };
        //
        //Pick pants
        //
        LimbedClothing pants = new LimbedClothing()
        {
            Base = PantsWaistOptions[Random.Range(0, PantsWaistOptions.Length)],
            Limb = PantsLegOptions[Random.Range(0, PantsLegOptions.Length)],
            Col = Random.ColorHSV()
        };
        //

        //Use rng.next for features that we want to constant every time the celeb exists.
        //Random.range for when it doesn't matter if that feature is constant across instances
        Appearance charAppearance = new Appearance()
        {
            Body = BodyOptions[rng.Next(0, BodyOptions.Length)],
            Hair = HairOptions[Random.Range(0, HairOptions.Length)],
            Face = FacesOptions[rng.Next(0, FacesOptions.Length)],
            Shirt = shirt,
            Pants = pants,
            Shoes = ShoeOptions[Random.Range(0, ShoeOptions.Length)]
        };

        return charAppearance;
    }
}
