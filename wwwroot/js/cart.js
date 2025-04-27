document.addEventListener('DOMContentLoaded', function () {

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


    // Seleccionar todos los inputs de cantidad
    const cantidadInputs = document.querySelectorAll('.cantidad-input');

    // Agregar event listener a cada input
    cantidadInputs.forEach(input => {
        input.addEventListener('change', function () {
            const form = this.closest('.form-agregar-al-pedido');
            const productoId = form.querySelector('input[name="ProductoId"]').value;
            const cantidadSolicitada = parseInt(this.value);

            // Llamada AJAX para verificar disponibilidad
            consultarDisponibilidad(productoId, cantidadSolicitada, this);
        });
    });


    // Función para consultar disponibilidad mediante AJAX
    function consultarDisponibilidad(productoId, cantidad, inputElement) {
        fetch(`/Catalogo/ConsultarDisponibilidad?productoId=${productoId}&cantidad=${cantidad}`)
            .then(response => response.json())
            .then(data => {
                if (!data.disponible) {
                    alert(`No hay suficiente stock. Stock disponible: ${data.stockDisponible}`);
                    inputElement.value = data.stockDisponible > 0 ? data.stockDisponible : 1;
                }
            })
            .catch(error => {
                console.error('Error al verificar disponibilidad:', error);
            });
    }
});
