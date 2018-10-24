﻿$(function () {
    $('#makeOrder').click(function () {
        let clientName = $('clientName').val();
        let clientNumber = $('clientNumber').val();
        let data = JSON.stringify({
            'clientName': clientName,
            'clientNumber': clientNumber
        });
        var url = $(this).data('url');
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            contentType: 'application/json',
            success: function () { }
        });
        $('#totalSum').empty();
        $('#orderList').empty();
        //$('#clientName').empty();
        //$('#clientNumber').empty();
    });
});