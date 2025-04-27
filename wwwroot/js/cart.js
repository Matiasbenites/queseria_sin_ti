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

    // Validar también al enviar el formulario
    const formularios = document.querySelectorAll('.form-agregar-al-pedido');
    formularios.forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault(); // Prevenir envío tradicional del formulario
            const productoId = this.querySelector('input[name="ProductoId"]').value;
            const cantidadInput = this.querySelector('input[name="Cantidad"]');
            const cantidad = parseInt(cantidadInput.value);

            // Verificar disponibilidad antes de agregar al carrito
            fetch(`/Catalogo/ConsultarDisponibilidad?productoId=${productoId}&cantidad=${cantidad}`)
                .then(response => response.json())
                .then(data => {
                    if (data.disponible) {
                        // Si hay stock disponible, agregar al carrito con AJAX
                        const formData = new FormData(this);
                        fetch('/Carrito/AgregarProducto', {
                            method: 'POST',
                            body: formData
                        })
                            .then(response => response.json())
                            .then(data => {
                                if (data.success) {
                                    // Mostrar mensaje de éxito
                                    mostrarMensaje('Producto agregado al carrito correctamente', 'success');

                                    // Resetear input de cantidad
                                    cantidadInput.value = 1;

                                    // Actualizar contador del carrito si existe
                                    if (data.totalItems) {
                                        const cartCounter = document.querySelector('.cart-counter');
                                        if (cartCounter) {
                                            cartCounter.textContent = data.totalItems;
                                        }
                                    }
                                } else {
                                    mostrarMensaje(data.message || 'Error al agregar el producto', 'error');
                                }
                            })
                            .catch(error => {
                                console.error('Error al agregar al carrito:', error);
                                mostrarMensaje('Error al procesar la solicitud', 'error');
                            });
                    } else {
                        mostrarMensaje(`No hay suficiente stock. Stock disponible: ${data.stockDisponible}`, 'error');
                        cantidadInput.value = data.stockDisponible > 0 ? data.stockDisponible : 1;
                    }
                })
                .catch(error => {
                    console.error('Error al verificar disponibilidad:', error);
                    mostrarMensaje('Error al verificar disponibilidad', 'error');
                });
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

    // Función para agregar productos al carrito
    function agregarAlCarrito(productoId, cantidad) {
        debugger;
        const formData = new FormData();
        formData.append('productoId', productoId);
        formData.append('cantidad', cantidad);

        fetch('/Carrito/AgregarProducto', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    // Mostrar mensaje de éxito
                    mostrarMensaje('Producto agregado al carrito correctamente', 'success');

                    // Resetear input de cantidad
                    cantidadInput.value = 1;

                    // Actualizar contador del carrito si existe
                    if (data.totalItems) {
                        const cartCounter = document.querySelector('.cart-counter');
                        if (cartCounter) {
                            cartCounter.textContent = data.totalItems;
                        }
                    }
                } else {
                    mostrarMensaje(data.message || 'Error al agregar el producto', 'error');
                }
            })
            .catch(error => {
                console.error('Error al agregar al carrito:', error);
                alert('Error al agregar el producto al carrito. Intente nuevamente.');
            });
    }

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
