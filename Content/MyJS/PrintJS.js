﻿
//var cicle = true;
//while (cicle) {

//   goRocket() //запуск таймера
//};

//goRocket() //запуск таймера
var tt = $(".printDoc");

tt.value;

function display() {
    alert("Будет произведена печать документа!*!" +
        "Выберите принтер, количество экземпляров и нажмите кнопку печать! ");

    var pdfInBase64 = document.getElementById("myButton").value.toString();
    
    var URL = window.URL || window.webkitURL
    byteChars = atob(pdfInBase64),
        bytes = [],
        i = 0;

    for (; i < byteChars.length; i++)
        bytes[i] = byteChars.charCodeAt(i);

    var blob = new Blob([new Uint8Array(bytes)], { type: 'application/pdf' });
    // создаём object URL из Blob
    var downloadUrl = URL.createObjectURL(blob);

    if (window.navigator && window.navigator.msSaveOrOpenBlob)
        window.navigator.msSaveOrOpenBlob(blob);
    else {
        var newWin = window.open(downloadUrl, '_blank', 'width=500,height=500,menubar=yes,scrollbars=yes,status=yes,resizable=yes');
        newWin.focus();
        newWin.print();//
        URL.revokeObjectURL(downloadUrl);
    }
};

//Запуск таймера.
function goRocket() {

    var pdfInBase64 = document.getElementById("myButton").value.toString();

    if (pdfInBase64 == "") {
       // alert('Пустая строка');
        countdown2(); // запускаем цикл 
        return;
    }

    else {
        
  //  alert('Произошел Запуск таймера');
    display();
        cicle = false;
        pdfInBase64 = "";

    let timer; // пока пустая переменная
    let x = 10; // стартовое значение обратного отсчета

    countdown(); // вызов функции

    function countdown() {  // функция обратного отсчета
       // document.getElementById('rocket').innerHTML = x;
        x--; // уменьшаем число на единицу

        if (x < 0) {
            clearTimeout(timer); // таймер остановится на нуле
          //  alert('Стоп таймер ');
            cicle = false;
            //goRocket()
        }
        else {
            timer = setTimeout(countdown, 1000);
        }
        }

    }

}

//Запуск печати документа ПРИХОДИТ ССЫЛКА на первый из списка pdf документ.
function CallPrint(strid, id) {

    let a = "fileFrame" + id;
   
    var prtContent = document.getElementsByName(strid) ;


    //получить id документа
    for (var i = 0; i < prtContent.length; i++) {

        if (a == prtContent[i].id) {

            var ttt = prtContent[i].id;

            //window.parent.frames["fileFrame"].focus();
            //window..parent.frames["fileFrame"].print();

            window.frames[i].focus();
            window.frames[i].print();

            WinPrint.focus();
            WinPrint.onload = () => {
                WinPrint.print();
            }
        }

        else {
         
        }
        
     // window.location.reload();
    }

}

//тестовой запуск метода из контролера
var someFunction = function (SomeUrl) {
    $.ajax({
        type: "POST",
        url: SomeUrl.URL,
        data: {},
        success: function (data) {
            alert("some alert");
            $('#place_for_some_content').html(data);
        }
    });

}

