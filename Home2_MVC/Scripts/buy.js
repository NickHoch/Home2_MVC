$(function () {
    $('td > button').click(function () {
        let id = $(this).data("rowid");
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
        $.ajax({
            type: "POST",
            url: 'Home/AddToBucket',
            data: data,
            contentType: 'application/json'
        });
    });
});
//url: '@Url.Action("AddToBucket", "Home")',