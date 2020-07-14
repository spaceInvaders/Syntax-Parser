function Save() {
    const localURL = "https://localhost:44365";
    const applicationURLforIOS = "https://localhost:5001";
    const deployedURL = "https://calcspace.azurewebsites.net";

    var phraseToSaveWithMailObject = {
        phraseToSave: encodeURIComponent(document.getElementById("textinput").value),
        mail: encodeURIComponent(document.getElementById("User_Identity_Name").value),
    };

    console.log(phraseToSaveWithMailObject.phraseToSave);
    console.log(phraseToSaveWithMailObject.mail);

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