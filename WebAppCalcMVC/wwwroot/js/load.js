function Load(number) {

    var phraseToLoad = document.getElementById("saving_" + number).value;
    var existingPhrase = document.getElementById("textinput").value;
    var resultPhrase = existingPhrase + phraseToLoad;

    $('#textinput').val(resultPhrase);
}