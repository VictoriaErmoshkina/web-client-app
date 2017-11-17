using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FHIR_UI.Models
{
    /// <summary>
    /// Contains text of JSON Resource
    /// </summary>
    public class JsonTextModel
    {
        public const string CREATED  = "CREATED";
        public const string UPDATED  = "UPDATED";
        public const string UPDATING = "UPDATING";
        public const string READING  = "READING";
        public const string FAILED   = "FAILED";
        public const string CREATING = "CREATING";

        [DataType(DataType.MultilineText)]
        public String JsonText_ { get; set; }
        public string Status_ { get; set; }
        public string ResType_ { get; set; }
        public string ResId_ { get; set; }
        public string ResVersion_ { get; set; }


        public JsonTextModel(string type, string id, string version, string text)
        {
            this.ResType_ = type;
            this.ResId_ = id;
            this.ResVersion_ = version;
            this.JsonText_ = FormatJson(text);
            
        }

        private const string INDENT_STRING = "    ";
        private string FormatJson(string json)
        {
            int indentation = 0;
            int quoteCount = 0;
            var result =
                from ch in json
                let quotes = ch == '"' ? quoteCount++ : quoteCount
                let lineBreak = ch == ',' && quotes % 2 == 0 ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, indentation)) : null
                let openChar = ch == '{' || ch == '[' ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, ++indentation)) : ch.ToString()
                let closeChar = ch == '}' || ch == ']' ? Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, --indentation)) + ch : ch.ToString()
                select lineBreak == null
                            ? openChar.Length > 1
                                ? openChar
                                : closeChar
                            : lineBreak;

            return String.Concat(result);
        }

        public JsonTextModel()
        {

        }
    }
}
