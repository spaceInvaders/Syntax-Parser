// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function ExecutePhrase() {

    var language;

    if (window.navigator.languages) {
        language = window.navigator.languages[0];
    }
    else {
        language = window.navigator.userLanguage || window.navigator.language;
    }

    var outputObject = {
        expression: encodeURIComponent(document.getElementById("textinput").value),
        culture: language,
    };

    console.log(outputObject.expression);
    console.log(outputObject.culture);

    $.ajax({
        url: window.location.protocol + "//" + window.location.host + "/ExecutePhrase/GetResult",
        type: "POST",
        data: "serializedInput=" + JSON.stringify(outputObject),
        success: function (data) {
            var calcResult = JSON.parse(data);

            document.getElementById("result_decimal").innerHTML = "decimal: " + calcResult.DecimalResult;
            document.getElementById("result_binary").innerHTML = "binary: " + calcResult.BinaryResult;
            document.getElementById("result_hexadecimal").innerHTML = "hexadecimal: " + calcResult.HexadecimalResult;
            document.getElementById("result_notifier").innerHTML = calcResult.Message;
        },
        dataType: "text"
    });
}
