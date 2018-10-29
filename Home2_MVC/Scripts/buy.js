$(function () {
    $('td > button').click(function () {
        let id = $(this).data('id');
        let name = $('#' + id + ' #pizzaName').text();
        let description = $('#' + id + ' #pizzaDescription').text();
        let quantity = parseInt($('#' + id + ' input').val());
        let totalSum = parseFloat($('#totalSum').text() || 0);
        let price = parseFloat($(this).data('price'));
        let orderItem = name + ' - ' + description + ' - ' + quantity + ' x ' + price + 'uah<br>';
        $('#orderList').append(orderItem);
        totalSum += price * quantity;
        $('#totalSum').text(totalSum + 'uah');
        let totalSumString = $('#totalSum').text();

        let productId = parseInt(id.replace('tr', ''));
        let data = JSON.stringify({
            'id': productId,
            'quantity': quantity,
            'orderItem': orderItem,
            'totalSum': totalSumString
        });
        let url = $(this).data('url');
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            contentType: 'application/json',
            success: function () { }
        });
    });
});