function StartMessage()
{
    const localURL = "https://localhost:44372";
    const applicationURLforIOS = "https://localhost:5001";
    const deployedURL = "https://calcspace.azurewebsites.net";

    var culture;

    if (window.navigator.languages)
    {
        culture = window.navigator.languages[0];
    }
    else
    {
        culture = window.navigator.userLanguage || window.navigator.language;
    }

    console.log(culture);

    $.ajax({
        url: localURL + "/ExecutePhrase/GetMessage",
        type: "POST",
        data: "culture=" + culture,
        success: function (data)
        {
            document.getElementById("result_notifier").innerHTML = data;
        },
        dataType: "text"
    });
}
