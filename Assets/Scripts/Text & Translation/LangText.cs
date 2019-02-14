using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace CRI.HelloHouston.Translation
{
    /// <summary>
    /// Contains all the text of a specific language. Is loaded from an XML files.
    /// </summary>
    [CreateAssetMenu(fileName = "New LangText", menuName = "Translation/LangText", order = 1)]
    public class LangText : ScriptableObject
    {
        /// <summary>
        /// The code ISO 639-1 of the language.
        /// </summary>
        public string code;

        /// <summary>
        /// All the text entries of a specific language.
        /// </summary>
        public LangTextEntry[] arrayOfLangTextEntry;

        public LangText() { }

        /// <summary>
        /// Instantiates a LangText from a code ISO 639-1.
        /// </summary>
        /// <param name="code">The code ISO 639-1 of the language.</param>
        public LangText(string code)
        {
            this.code = code;
        }

        /// <summary>
        /// Save the data to an json file.
        /// </summary>
        /// <param name="path">The path where the json file will be saved.</param>
        public void Save(string path)
        {
            string dataJson = JsonUtility.ToJson(this, true);
            string filePath = Path.Combine(Application.dataPath, path);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.Write(dataJson);
            }
        }

        /// <summary>
        /// Loads a lang text data from the json file at the specified path.
        /// </summary>
        /// <returns>The lang text data.</returns>
        /// <param name="path">The path here the XML file is located.</param>
        public static LangText Load(string path)
        {
            string filePath = Path.Combine(Application.dataPath, path);
            return LoadFromText(File.ReadAllText(filePath));
        }

        /// <summary>
        /// Loads a lang text from a json text.
        /// </summary>
        /// <returns>The lang setting data.</returns>
        /// <param name="text">A text in XML format that contains the data that will be loaded.</param>
        public static LangText LoadFromText(string text)
        {
            LangText res = CreateInstance<LangText>();
            JsonUtility.FromJsonOverwrite(text, res);
            return res;
        }
    }
}
