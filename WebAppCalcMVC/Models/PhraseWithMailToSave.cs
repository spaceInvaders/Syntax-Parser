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
        internal PhraseWithMailToSave(string phraseToSave, string mail)
        {
            PhraseToSave = phraseToSave;
            Mail = mail;
        }

        public string PhraseToSave { get; set; }
        public string Mail { get; set; }
    }
}
