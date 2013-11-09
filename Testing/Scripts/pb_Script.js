$(document).ready(function (e) {
    $('.main-menu-inner').hide(); //An menu sau khi load trang
    $("#logo").find('#pbox-logo').click(function (e) {
        $('.main-menu-inner').slideToggle(500);
    });
});