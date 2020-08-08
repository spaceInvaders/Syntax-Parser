function StartMessage() {

    var culture;

    if (window.navigator.languages) {
        culture = window.navigator.languages[0];
    }
    else {
        culture = window.navigator.userLanguage || window.navigator.language;
    }

    console.log(culture);

    $.ajax({

        url: window.location.protocol + "//" + window.location.host + "/ExecutePhrase/GetMessage",
        type: "POST",
        data: "culture=" + culture,

        success: function (data) {
            document.getElementById("result_notifier").innerHTML = data;
        },

        dataType: "text"
    });
}