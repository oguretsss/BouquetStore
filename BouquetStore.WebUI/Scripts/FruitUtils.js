$(document).ready(function () {

  $('#categories a').click(function () {
    $('#categories a').removeClass('current-category');
    var position = $(window).scrollTop();
    var url = $(this).attr('href');
    $(this).addClass('current-category');
    $('#grid-container').load(url + ' #product-grid');
    $(window).scrollTop(position);
    return false;
  });

  $('#grid-container').on('click', '.btn-open-description', ShowModal);
  $('#product-promo-grid').on('click', '.btn-open-description', ShowModal);
});
function OnBegin() {
  $(this).find('button').html('<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;...&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>');
}
function OnSuccess(data) {
  var elem = $(this).find('button');
  elem.html('<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-check" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>');
  setTimeout(function () { elem.html('В корзину'); }, 3000)
}

function ShowModal() {
  var windowInfo = $(this).children(".product-modal-info");
  var modalText = windowInfo.children(".product-description").text();
  var modalHeader = windowInfo.children(".product-name").text();
  var modalCategory = windowInfo.children(".product-category").text();
  var imgSrc = windowInfo.children(".product-img-src").text();
  $("#myModal").modal();
  $("#myModal .modal-body .modal-img").attr('src', imgSrc);
  $("#myModal .modal-body .category").text(modalCategory);
  $("#myModal .modal-body p").text(modalText);
  $("#myModal .modal-header h3").text(modalHeader);
  $("#myModal .modal-footer form").remove();
  $("#myModal .modal-footer").append($(this).parent().children('.to-cart-form').clone());
}