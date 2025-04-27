$(document).ready(function () {
    // Validación para cada formulario de agregar al pedido
    $('.form-agregar-al-pedido').on('submit', function (e) {
        var cantidadInput = $(this).find('input[name="Cantidad"]');
        var cantidad = parseInt(cantidadInput.val(), 10);

        if (isNaN(cantidad) || cantidad < 1) {
            e.preventDefault(); // Evita el envío del formulario
            alert('Por favor ingrese una cantidad válida mayor o igual a 1.');
            cantidadInput.focus();
        }
    });
});
