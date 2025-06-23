document.addEventListener('DOMContentLoaded', function () {

    // Validación para cada formulario de agregar al pedido
    $('.form-agregar-al-pedido').on('submit', function (e) {
        var cantidadInput = $(this).find('input[name="Cantidad"]');
        var cantidad = parseInt(cantidadInput.val(), 10);

        if (isNaN(cantidad) || cantidad < 1) {
            e.preventDefault(); // Evita el envío del formulario
            //alert('Por favor ingrese una cantidad válida mayor o igual a 1.');
            cantidadInput.focus();
        }
    });

    // Validar también al enviar el formulario
    const formularios = document.querySelectorAll('.form-agregar-al-pedido');
    formularios.forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault(); // Prevenir envío tradicional del formulario
            const productoId = this.querySelector('input[name="ProductoId"]').value;
            const cantidadInput = this.querySelector('input[name="Cantidad"]');
            const cantidad = parseInt(cantidadInput.value);

            // Verificar disponibilidad antes de agregar al carrito
            fetch(`/Productos/ConsultarDisponible?productoId=${productoId}&cantidad=${cantidad}`)
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                    if (data.stockDisponible) {
                        // Si hay stock disponible, agregar al carrito con AJAX
                        const formData = new FormData(this);
                        fetch('/Carrito/AgregarProducto', {
                            method: 'POST',
                            body: formData
                        })
                            .then(response => response.json())
                            .then(data => {
                                if (data.success) {
                                    location.reload();

                                    // Mostrar mensaje de éxito
                                    mostrarMensaje('Producto agregado al carrito', 'success');
                                } else {
                                    mostrarMensaje(data.message || 'Error al agregar el producto', 'error');
                                }
                            })
                            .catch(error => {
                                console.error('Error al agregar al carrito:', error);
                                mostrarMensaje('Error al procesar la solicitud', 'error');
                            });
                    } else {
                        mostrarMensaje(data.message, 'error');
                    }
                })
                .catch(error => {
                    console.error('Error al verificar disponibilidad:', error);
                    mostrarMensaje('Error al verificar disponibilidad', 'error');
                });
        });
    });

    // Función para mostrar mensajes temporales
    function mostrarMensaje(mensaje, tipo) {
        // Crear elemento para el mensaje
        const mensajeDiv = document.createElement('div');
        mensajeDiv.classList.add('alert', tipo === 'success' ? 'alert-success' : 'alert-danger', 'mensaje-temporal');
        mensajeDiv.textContent = mensaje;

        // Añadir estilo para posicionar el mensaje
        mensajeDiv.style.position = 'fixed';
        mensajeDiv.style.top = '20px';
        mensajeDiv.style.right = '20px';
        mensajeDiv.style.zIndex = '9999';
        mensajeDiv.style.minWidth = '300px';

        // Añadir a la página
        document.body.appendChild(mensajeDiv);

        // Eliminar después de 3 segundos
        setTimeout(() => {
            mensajeDiv.style.opacity = '0';
            mensajeDiv.style.transition = 'opacity 0.5s ease';
            setTimeout(() => {
                document.body.removeChild(mensajeDiv);
            }, 500);
        }, 3000);
    }

       
});
