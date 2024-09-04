$(document).ready(function () {
    $('body').on('click', '.btnAddToCart',function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quatity = 1;
        var tQuatity = $('#quantity_value').text();
        if (tQuatity != '') {
            quatity = parseInt(tQuatity);
        }
        alert(id+" "+ quatity);
    })
})