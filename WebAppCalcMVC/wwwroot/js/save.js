function Save() {
    const localURL = "https://localhost:44365";
    const applicationURLforIOS = "https://localhost:5001";
    const deployedURL = "https://calcspace.azurewebsites.net";

    var textInput = document.getElementById("textinput").value;

    if (textInput == null || textInput.trim().length === 0) {
        document.getElementById("result_notifier").innerHTML = "Nothing to save :( Type something";
    }
    else {
        var phraseToSaveWithMailObject = {
            phraseToSave: encodeURIComponent(document.getElementById("textinput").value),
            mail: encodeURIComponent(document.getElementById("User_Identity_Name").value),
            dateOnClient: new Date(),
        };

        console.log(phraseToSaveWithMailObject.phraseToSave);
        console.log(phraseToSaveWithMailObject.mail);
        console.log(phraseToSaveWithMailObject.dateOnClient);

        $.ajax({
            url: localURL + "/Save/SaveToDb",
            type: "POST",
            data: "serializedInput=" + JSON.stringify(phraseToSaveWithMailObject),
            success: function (data) {
                document.getElementById("result_notifier").innerHTML = data;
            },
            dataType: "text"
        });
    }
}

// var el = document.getElementById("saveButton");
// el.addEventListener("click", Save, false);