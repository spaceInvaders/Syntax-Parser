function SetNameForLoadButton() {

    var textEmail = document.getElementById("User_Identity_Name").value;

    $.ajax({

        url: window.location.protocol + "//" + window.location.host + "/SetNameForLoadButton/GetPhraseFromDb",
        type: "POST",
        data: "email=" + textEmail,

        success: function (data) {

            var result = JSON.parse(data);

            document.getElementById("saving_1").setAttribute("value", result.Value_1);
            document.getElementById("saving_2").setAttribute("value", result.Value_2);
            document.getElementById("saving_3").setAttribute("value", result.Value_3);
            document.getElementById("saving_4").setAttribute("value", result.Value_4);
            document.getElementById("saving_5").setAttribute("value", result.Value_5);
        },

        dataType: "text"
    });
}