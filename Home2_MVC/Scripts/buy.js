$(function () {
    $('td > button').click(function () {
        let id = $(this).data('rowid');
        let name = $('#' + id + ' #pizzaName').text();
        let description = $('#' + id + ' #pizzaDescription').text();
        let quantity = parseInt($('#' + id + ' input').val());
        let totalSum = parseFloat($('#totalSum').text() || 0);
        let price = parseFloat($(this).data("price"));
        $('#orderList').append(name + " - " + description + " - " + quantity + " x " + price + "uah<br>");
        totalSum += price * quantity;
        $('#totalSum').text(totalSum + "uah");

        let productId = id.replace('tr');
        var data = JSON.stringify({
            'id': productId,
            'quantity': quantity
        });

        var url = $(this).data('url');
        console.log(url);
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            async: true,
            contentType: 'application/json',
            success: function () { }
        });
    });
});