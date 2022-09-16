$(document).ready(function () {
    GetLocation();
    $('#txtLocation').change(function () {
        var id = $(this).val();
        $('#txtRoom').empty();
        $('#txtRoom').append('<option>--Seleccionar Sala--</option>'); 
        $.ajax({
            url: '/Home/txtRoom?id=' + id,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#txtRoom').append('<Option value=' + data.idMeetR + '>' + data.nameMeetR + '</Option>');
                });
            }
        });
    });
});

function GetLocation() {
    $.ajax({
        url: '/Home/txtLocation',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#txtLocation').append('<Option value=' + data.idLocation + '>' + data.nameLocation + '</Option>');
            });
        }
    });
};

