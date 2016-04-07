using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGXLEDApp
{
    class LEDFont
    {
        enum Font { English = 1, Hindi, Kannada,Punjabi,Tamil }
        string[] Fonts = { "Font1", "Font2", "Font3","font4","font5"};

        string[] EnglishFonts = { "Font 7x5", "Tohama Small", "Verdana","Courier","Tahoma Large","Georgia","RockWell","Thremin Gothic","Batang","Bitwise","Pirule" ,"Venus Rising",
"Engravers","Impact H15","Engravers H16","Venus Rising H16","Impact H16","Steelfish H16","Impact H32","Steelfish H32","Steelfish Reg H32","Impact Mid H32",
"Steelfish MidReg H32"};

        string[] KannadaFonts = { "BrH Kannada"};
        string[] HindiFonts = { "Hindi" };
        string[] TamilFonts = { "BRH Tamil" };
        string[] TeluguFonts = { "Telugu" };
        string[] Punjabifonts = { "Punjabi" };
        public string[] GetFonts()
        {
            return Fonts;
        }
        public string[] GetFonts(LEDLanguage.Language Lang)
        {
            var enumValues = Lang;
            switch (enumValues)
            {
                case LEDLanguage.Language.English:
                    return EnglishFonts;
                case LEDLanguage.Language.Hindi:
                    return HindiFonts;
                case LEDLanguage.Language.Kannada:
                    return KannadaFonts;
                case LEDLanguage.Language.Punjabi:
                    return Punjabifonts;
                case LEDLanguage.Language.Tamil:
                    return TamilFonts;


                default :
                    return EnglishFonts;

            }
            return Fonts;
        }
    }
    class LEDLanguage
    {
        public enum Language { English = 1, Hindi, Kannada,Punjabi,Tamil }
        string[] Languages = { "English", "Hindi", "Kannada","Punjabi", "Tamil"};
        public string[] GetLaguages()
        {
            return Languages;
        }

        public Language StringToEnum(String Language)
        {
            switch (Language)
            {
                case "English":
                    return LEDLanguage.Language.English;
                case "Hindi":
                    return LEDLanguage.Language.Hindi;
                case "Kannada":
                    return LEDLanguage.Language.Kannada;
                case "Punjabi":
                    return LEDLanguage.Language.Punjabi;
                case "Tamil":
                    return LEDLanguage.Language.Tamil;

            }
            return LEDLanguage.Language.English;
        }
            
        public byte FontID(Language Lang)
        {
            var enumValues = Lang;
            switch (enumValues)
            {
                case Language.English:
                    return (byte)Language.English;

            }
            return 0xff;
        }
    }
    class LEDSpeed
    {
        //enum Speed { English = 1, Hindi, Kannada, Tamil, Telgu, Marathi, Malayalum, Oriya, Bengali }
        enum Speed { English = 1, Hindi, Kannada,Punjabi,Tamil}
        string[] Speeds = { "Speed 1", "Speed 2", "Speed 3", "Speed 4", "Speed 5", "Speed 6", "Speed 7", "Speed 8", "Speed 9","Speed 10","Default" };
        public string[] GetSpeeds()
        {
            return Speeds;
        }
    }

    

    class LEDEffect
    {
        enum Effect { English = 1, Hindi, Kannada,Punjabi,Tamil }
        string[] Effects = { "Horizontal Scroll", "Still", "Horizontal Scroll Wavy", "Flash","Drop", "Up Down","Matrix","Diagonal","Right slide","Blooming","Left Slide" ,"Vertical Up","Verticle Down", "Jumping"  };
        public string[] GetEffects()
        {
            return Effects;
        }
    }
}
