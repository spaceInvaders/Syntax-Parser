function Load(saveIndex) {

    var phraseToLoad = document.getElementById("saving_" + saveIndex).value;
    var existingPhrase = document.getElementById("textinput").value;
    var resultPhrase = existingPhrase + phraseToLoad;

    $('#textinput').val(resultPhrase);
}