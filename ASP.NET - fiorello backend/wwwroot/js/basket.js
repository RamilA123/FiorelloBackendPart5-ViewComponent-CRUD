$(document).ready(function () {

    $(document).on("submit", "#basket-form", function (e) {

        e.preventDefault();
        let productId = $(this).attr("data-id");
        let data = { id: productId };
        $.ajax({
            url: "cart/addtocart",
            type: "Post",
            data: data,
            success: function (res) {
                $("sup").text(res);
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Product added to cart',
                    showConfirmButton: false,
                    timer: 1500
                })
            }
            })
        
    })


    // delete product from basket
    $(document).on("submit", "#basket-delete-form", function (e) {
        e.preventDefault();
        let productId = $(this).attr("data-id");
        $(this).parent().parent().remove()
        let data = { id: productId };
        $.ajax({
            url: "cart/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $("sup").text(res.count);
                if (res.count != 0) {
                    $(".total").text("Total: " + res.total + "₼");
                }
                else {
                    $(".products-table").addClass("d-none");
                    $(".alert-cart-empty").removeClass("d-none");
                }
              
                
            }
        })

    })
})