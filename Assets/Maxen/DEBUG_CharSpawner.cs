#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_CharSpawner : MonoBehaviour
{
    public CharacterInformation charPrefab;
    public CharacterGen generator;

    public Vector3 deltaPosition = new Vector3(2.0f, 0.0f, 0.0f);

    private Stack<CharacterInformation> CreatedCharacters = new Stack<CharacterInformation>();

    public enum BodyPart
    {
        BODY,
        HAIR,
        FACE,
        SHIRTBASE,
        SHIRTSLEEVE,
        PANTSBASE,
        PANTSLEG,
        SHOES
    }
    public BodyPart ShowPart;

    private void ClearOldInstances()
    {
        while(CreatedCharacters.Count > 0)
        {
            CharacterInformation ch = CreatedCharacters.Pop();
            Destroy(ch.gameObject);
        }
    }

    private void GenNewInstances()
    {
        CharacterGen.Appearance baseAppearance = generator.GenerateAppearance(-1);

        Vector3 offset = Vector3.zero;
        if (ShowPart == BodyPart.BODY)
        {
            foreach (CharacterGen.BodyCollection body in generator.AllBodyOptions)
            {
                CharacterInformation ch = Instantiate(charPrefab, transform.position + offset, Quaternion.identity);
                offset += deltaPosition;
                baseAppearance.Body = body;
                ch.SetAppearance(baseAppearance);
                CreatedCharacters.Push(ch);
            }
        }
        else
        {
            switch(ShowPart)
            {
                case BodyPart.HAIR:
                    {
                        foreach(Sprite sp in generator.AllHairOptions)
                        {
                            CharacterInformation ch = Instantiate(charPrefab, transform.position + offset, Quaternion.identity);
                            offset += deltaPosition;
                            baseAppearance.Hair = sp;
                            ch.SetAppearance(baseAppearance);
                            CreatedCharacters.Push(ch);
                        }
                        break;
                    }
                case BodyPart.FACE:
                    {
                        foreach (Sprite sp in generator.AllFaceOptions)
                        {
                            CharacterInformation ch = Instantiate(charPrefab, transform.position + offset, Quaternion.identity);
                            offset += deltaPosition;
                            baseAppearance.Face = sp;
                            ch.SetAppearance(baseAppearance);
                            CreatedCharacters.Push(ch);
                        }
                        break;
                    }
                case BodyPart.SHIRTBASE:
                    {
                        List<Sprite> baseShirts = new List<Sprite>(generator.AllFeminineShirtBaseOptions);
                        baseShirts.AddRange(generator.AllMasculineShirtBaseOptions);
                        foreach (Sprite sp in baseShirts)
                        {
                            CharacterInformation ch = Instantiate(charPrefab, transform.position + offset, Quaternion.identity);
                            offset += deltaPosition;
                            baseAppearance.Shirt.Base = sp;
                            ch.SetAppearance(baseAppearance);
                            CreatedCharacters.Push(ch);
                        }
                        break;
                    }
                case BodyPart.SHIRTSLEEVE:
                    {
                        foreach (Sprite sp in generator.AllShirtSleeveOptions)
                        {
                            CharacterInformation ch = Instantiate(charPrefab, transform.position + offset, Quaternion.identity);
                            offset += deltaPosition;
                            baseAppearance.Shirt.Limb = sp;
                            ch.SetAppearance(baseAppearance);
                            CreatedCharacters.Push(ch);
                        }
                        break;
                    }
                case BodyPart.PANTSBASE:
                    {
                        foreach (Sprite sp in generator.AllPantsWaistOptions)
                        {
                            CharacterInformation ch = Instantiate(charPrefab, transform.position + offset, Quaternion.identity);
                            offset += deltaPosition;
                            baseAppearance.Pants.Base = sp;
                            ch.SetAppearance(baseAppearance);
                            CreatedCharacters.Push(ch);
                        }
                        break;
                    }
                case BodyPart.PANTSLEG:
                    {
                        foreach (Sprite sp in generator.AllPantsLegOptions)
                        {
                            CharacterInformation ch = Instantiate(charPrefab, transform.position + offset, Quaternion.identity);
                            offset += deltaPosition;
                            baseAppearance.Pants.Limb = sp;
                            ch.SetAppearance(baseAppearance);
                            CreatedCharacters.Push(ch);
                        }
                        break;
                    }
                case BodyPart.SHOES:
                    {
                        foreach (Sprite sp in generator.AllShoeOptions)
                        {
                            CharacterInformation ch = Instantiate(charPrefab, transform.position + offset, Quaternion.identity);
                            offset += deltaPosition;
                            baseAppearance.Shoes = sp;
                            ch.SetAppearance(baseAppearance);
                            CreatedCharacters.Push(ch);
                        }
                        break;
                    }
            }
        }
    }

    private void Start()
    {
        GenNewInstances();
        oldPart = ShowPart;
    }

    private BodyPart oldPart;
    private void OnValidate()
    {
        if(oldPart != ShowPart && UnityEditor.EditorApplication.isPlaying)
        {
            ClearOldInstances();
            GenNewInstances();
        }

        oldPart = ShowPart;
    }
}
#endif