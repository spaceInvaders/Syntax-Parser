using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace WebAppCalcMVC.Models
{
    public class PhraseWithMailToSave
    {
        [JsonConstructor]
        internal PhraseWithMailToSave(string phraseToSave, string mail, string dateOnClient)
        {
            PhraseToSave = phraseToSave;
            Mail = mail;
            DateOnClient = dateOnClient;
        }

        public string PhraseToSave { get; set; }
        public string Mail { get; set; }
        public string DateOnClient { get; set; }
    }
}
