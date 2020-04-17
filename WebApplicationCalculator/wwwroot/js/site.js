// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function ExecutePhrase() {

    var inputPhrase = document.getElementById("textinput").value;

    var request = new XMLHttpRequest();
    
    function reqReadyStateChange()
    {
        if (request.readyState == 4)
        {
            if (request.status == 200)
            {
                let response = request.responseText;

                document.getElementById("result_decimal").innerHTML = "decimal: " + response;
                document.getElementById("result_binary").innerHTML = "binary: " + response;
                document.getElementById("result_hexadecimal").innerHTML = "hexadecimal: " + response;
            }
            else
            {
                document.write(request.statusText);
            }
        }
    }

    request.open("GET", "https://localhost:44372/ExecutePhrase/Calc?input=" + encodeURIComponent(inputPhrase), true);
    request.onreadystatechange = reqReadyStateChange;
    request.send();
}









/*document.getElementById('01').onkeypress = function (event) {
    console.log(event);
    if (event.keyCode == 42) {
            event.target.value += '\u2A2F';
        event.preventDefault();
    }
}*/