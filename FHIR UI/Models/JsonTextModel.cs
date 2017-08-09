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
            this.JsonText_ = text;
        }
        public JsonTextModel()
        {

        }
    }
}
