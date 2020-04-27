// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function ExecutePhrase()
{

    var language;

    if (window.navigator.languages)
    {
        language = window.navigator.languages[0];
    }
    else
    {
        language = window.navigator.userLanguage || window.navigator.language;
    }

    var outputObject = {

        expression: encodeURIComponent(document.getElementById("textinput").value),
        culture: language,

    };

    console.log(outputObject.expression);
    console.log(outputObject.culture);


    //Создаем объект запроса
    let xhr = new XMLHttpRequest();

    function reqReadyStateChange()
    {
        //Проверим состояние запроса, нас интересует случай когда он завершен ( DONE )
        if (xhr.readyState == 4)
        {
            //Дальше проверим какой код ответа нам выдал сервер
            if (xhr.status == 200)
            {
                //Если попали сюда, значит можно выполнить функцию, которую вам нужно

                var calcResult = JSON.parse(xhr.responseText);
                //console.log(xhr.responseText);

                document.getElementById("result_decimal").innerHTML = "decimal: " + calcResult.DecimalResult;
                document.getElementById("result_binary").innerHTML = "binary: " + calcResult.BinaryResult;
                document.getElementById("result_hexadecimal").innerHTML = "hexadecimal: " + calcResult.HexadecimalResult;
                document.getElementById("result_notifier").innerHTML = calcResult.Message;
            }
            else
            {
                document.write(xhr.statusText);
            }
        }
    }

    xhr.open("GET", "https://localhost:44372/ExecutePhrase/GetResult?serializedInput=" + JSON.stringify(outputObject), true);
    xhr.onreadystatechange = reqReadyStateChange;
    xhr.send();
    
    //xhr.open("POST", "https://localhost:44372/ExecutePhrase/GetResult");

    ////Установим заголовок запроса, где явно укажем тип данных, который будем передавать. В нашем случае это будет объект в формате JSON
    //xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");

    //xhr.onreadystatechange = reqReadyStateChange;
    ////Отправим запрос, передав в тело запроса наш сериализованый в формат JSON наш объект
    //xhr.send(JSON.stringify(outputObject));
}









/*
 *
    var language;
    if (window.navigator.languages) {
        language = window.navigator.languages[0];
    } else {
        language = window.navigator.userLanguage || window.navigator.language;
    }
 * 
 * document.getElementById('textinput').onkeypress = function (event) {
    console.log(event);
    if (event.keyCode == 42) {
            event.target.value += '\u2A2F';
        event.preventDefault();
    }
}*/