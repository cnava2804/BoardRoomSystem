$(document).ready(function () {
    GetLocations();
    $('#loc_selector').change(function () {
        var id = $(this).val();
        $('#room_selector').empty();
        $('#room_selector').append('<option>--Seleccionar Sala--</option>');
        $.ajax({
            url: '/Home/room_selector?id=' + id,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#room_selector').append('<Option value=' + data.idMeetR + '>' + data.nameMeetR + '</Option>');
                });
            }
        });
    });
});

function GetLocations() {
    $.ajax({
        url: '/Home/loc_selector',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#loc_selector').append('<Option value=' + data.idLocation + '>' + data.nameLocation + '</Option>');
            });
        }
    });
}

