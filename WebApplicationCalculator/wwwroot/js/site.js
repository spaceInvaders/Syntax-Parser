// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function ExecutePhrase() {

    var inputPhrase = document.getElementById("01").value;

    document.getElementById("result_decimal").innerHTML = "decimal:  " + inputPhrase;
    document.getElementById("result_binary").innerHTML = "binary:  " + inputPhrase;
    document.getElementById("result_hexadecimal").innerHTML = "hexadecimal:  " + inputPhrase;

}
