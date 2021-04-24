

$(document).ready(function () {
    $('.frmAddToCart').submit(function (e) {
        e.preventDefault();
        let productId = $(this).find('input[name=ProductId').val();
        let quantity = $(this).find('input[Name=Quantity]').val();
        const data = { ProductId: productId, Quantity: quantity };
        console.log(data);
        $.ajax({
            url: '/Cart/AddToCart',
            type: "POST",
            data: data,
            success: function (res) {
                console.log(res);
                if (res.state) {
                    $(document).trigger('addToCartEvent');
                }
                else {
                    alert(" thêm thất bại");
                }
            },
            error: function (err) {
                alert("add fail");
            }
        })
    });
    $(document).on('addToCartEvent', function () {
        $.ajax({
            url: '/Cart/CartSummary',
            type: "GET",
            success: function (res) {
                $('.cart-summary').html(res);
            },
            error: function (err) {
                console.log(err);
            }
        })
    })
})

function Function() {
    var author = document.getElementsByClassName("author").val();
    $(author).addClass("active");
}