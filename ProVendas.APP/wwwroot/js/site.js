
$(document).ready(function () {
    $('#dataTable').DataTable({
        "language": {
        "url": 'https://cdn.datatables.net/plug-ins/1.11.5/i18n/pt-BR.json',          
        },

        responsive: true
    });   
});

(() => {
   'use strict'
       const forms = document.querySelectorAll('.needs-validation')

    const nozero = document.querySelectorAll('.nosero')

            Array.from(forms).forEach(form => {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }

            form.classList.add('was-validated')
        }, false)
    })
})()
  
