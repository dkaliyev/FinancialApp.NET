$(document).ready(function() {
    $('.toggle').change(function() {
        var id = $(this).data("id");
        var value = $(this).val();
        var obj = { Id: id, State: value }
        console.log(id);
        $.ajax({
            url: "/Company/Toggle",
            method: "POST",
            data: obj
        });
    });

    $('.company').each(function() { $(this).css('top', $('#companies_header').height() + 100 + 'px'); });
});

function show(id) {
    $('.link').each(function() {
        $(this).removeClass("show");
        $(this).addClass("hide");
    });

    $('#link-' + id).removeClass('hide');
    $('#link-' + id).addClass('show');

    $('.company').each(function() {
        $(this).removeClass("show");
        $(this).addClass("hide");
    });

    $('#table-' + id).removeClass('hide');
    $('#table-' + id).addClass('show');
}

function toggle(dir) {
    $.ajax({
        url: "/Company/ToggleAll/"+dir,
        method: "GET",
        success: function() {
            $('.toggle').each(function () {
                if (dir == 0) {
                    $(this).removeProp("checked");
                } else {
                    $(this).prop("checked", "true");
                }
            });
        }
    });
}

function addCompany() {
    $('#overlay_blur').removeClass('normal');
    $('#overlay_blur').addClass('blured');
    $('.overlay').slideDown();
    $('.overlay').removeClass('hide');
    $('.overlay').addClass('show');
}

function closeOverlay() {
    $('.overlay').slideUp();
    $('#overlay_blur').removeClass('blured');
    $('#overlay_blur').addClass('normal');
    $('.overlay').removeClass('show');
    $('.overlay').addClass('hide');
}

function add() {
    var comp = $('#company_name').val();
    var exch = $('#exch_name').val();

    var data = { "StockName": exch, "Code": comp };

    $.ajax("/Company/AddCompany", {
        data: data,
        method: "POST",
        success: function(res) {
            $('.companies_table>tbody').append(res);
            closeOverlay();
        }
    });
}
function generateData(id) {
    $.ajax("/Data/Generate", {
        data: { "id": id },
        method: "POST",
        success: function (res) {

        }
    });
}