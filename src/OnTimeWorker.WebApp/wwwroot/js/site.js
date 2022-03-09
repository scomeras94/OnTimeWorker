// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ShowComment(comment) {
    alert(`${comment}`);
}

function ChangeSelectedMonth() {
    let myselect = document.getElementById("months");
    let selectedMonth = myselect.options[myselect.selectedIndex].value;

    if (selectedMonth != null && selectedMonth != "") {
        window.location.href = `SetSelectedMonth?month=${selectedMonth}`;
    }
}

function ChangeSelectedYear() {
    let myselect = document.getElementById("years");
    let selectedYear = myselect.options[myselect.selectedIndex].value;

    if (selectedYear != null && selectedYear != "") {
        window.location.href = `/Register/FilterRegisters?year=${selectedYear}`;
    }
}