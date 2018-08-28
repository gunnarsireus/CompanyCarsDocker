// Write your JavaScript code.
$(document).ready(function () {
    timerJob();
    console.log('documentReady');
});

$(".uppercase").keyup(function () {
    var text = $(this).val();
    $(this).val(text.toUpperCase());
});

function clearErrors() {
    $(".validation-summary-errors").empty();
};

function timerJob() {
    const tenSeconds = 10000;
    const oneSecond = 1000;
    $.ajax({
        url: "http://localhost:54411/api/car",
        type: "GET",
        dataType: "json",
        success: function (cars) {
            if (cars.length === 0) {
                setTimeout(timerJob, oneSecond);
                console.log("No vehicles found!");
                return;
            }
            const selectedItem = Math.floor(Math.random() * cars.length);
            let selectedCar = cars[selectedItem];
            if (selectedCar.disabled === true) {
                console.log(selectedCar.regNr + " is blocked for uppdating of Online/Offline!");
                return;
            }
            selectedCar.online = !selectedCar.online;
            $.ajax({
                url: 'http://localhost:54411/api/car/' + selectedCar.id,
                contentType: "application/json",
                type: "PUT",
                data: JSON.stringify(selectedCar),
                dataType: "json"
            });

            const selector = `#${selectedCar.id} td:eq(2)`;
            const selector2 = `#${selectedCar.id + "_2"} td:eq(3)`;
            const selector3 = `#${selectedCar.id + "_3"}`;
            if (selectedCar.online === true) {
                $(selector).text("Online");
                $(selector).removeClass("alert-danger");
                $(selector2).text("Online");
                $(selector2).removeClass("alert-danger");
                $(selector3).text("Online");
                $(selector3).removeClass("alert-danger");
                console.log(selectedCar.regNr + " is Online!");
            }
            else {
                $(selector).text("Offline");
                $(selector).addClass("alert-danger");
                $(selector2).text("Offline");
                $(selector2).addClass("alert-danger");
                $(selector3).text("Offline");
                $(selector3).addClass("alert-danger");
                console.log(selectedCar.regNr + " is Offline!");
            }
            if (document.getElementById("All") !== null) {
                doFiltering();
            }
        }
    });
    setTimeout(timerJob, tenSeconds);
}

function doFiltering() {
    let selection = 0;
    let radiobtn = document.getElementById("All");
    if (radiobtn.checked === false) {
        radiobtn = document.getElementById("Online");
        if (radiobtn.checked === true) {
            selection = 1;
        }
        else {
            selection = 2;
        }
    }

    var table = $('#cars > tbody');
    $('tr', table).each(function () {
        $(this).removeClass("hidden");
        let td = $('td:eq(2)', $(this)).html();
        if (td !== undefined) {
            td = td.trim();
        }
        if (td === "Offline" && selection === 1) {
            $(this).addClass("hidden");  //Show only Online
        }
        if (td === "Online" && selection === 2) {
            $(this).addClass("hidden"); //Show only Offline
        }
    });
};

function showModals() {
    window.open("./html/Car.html", "_blank", "toolbar=yes,scrollbars=yes,resizable=yes,top=50,left=50,width=500,height=400");
}
