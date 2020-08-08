function Save(saveIndex) {

    var inputWithoutWhiteSpaces = document.getElementById("textinput").value.split(/\s+/).join('');

    if (inputWithoutWhiteSpaces == null || inputWithoutWhiteSpaces.length === 0) {
        document.getElementById("result_notifier").innerHTML = "nothing to save :( Type something";
    }
    else {

        for (var index = 1; index <= saveIndex; index++) {
            if (document.getElementById("saving_" + index).value === inputWithoutWhiteSpaces) {

                document.getElementById("result_notifier")
                    .innerHTML = `'${inputWithoutWhiteSpaces}' already exists in database :)`;

                return;
            }
        }

         var phraseToSaveWithMailObject = {
             phraseToSave: encodeURIComponent(document.getElementById("textinput").value),
             mail: encodeURIComponent(document.getElementById("User_Identity_Name").value),
             dateOnClient: new Date(),
         };
         
         console.log(phraseToSaveWithMailObject.phraseToSave);
         console.log(phraseToSaveWithMailObject.mail);
         console.log(phraseToSaveWithMailObject.dateOnClient);
         
         $.ajax({
         
             url: window.location.protocol + "//" + window.location.host + "/Save/SaveToDb",
             type: "POST",
             data: "serializedInput=" + JSON.stringify(phraseToSaveWithMailObject),
         
             success: function (data) {
         
                 var result = JSON.parse(data);
         
                 document.getElementById("result_notifier").innerHTML = result.Message;
         
                 document.getElementById("saving_1").setAttribute("value", result.Value_1);
                 document.getElementById("saving_2").setAttribute("value", result.Value_2);
                 document.getElementById("saving_3").setAttribute("value", result.Value_3);
             },
         
             dataType: "text"
         });
    }
}
