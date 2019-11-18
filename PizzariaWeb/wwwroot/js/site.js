// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$('#cadastrarCargo').submit(function (e) {
    e.preventDefault();

    let dados = {
        "Nome": $('[name="Nome"]').val(),
        "Salario": $('[name="Salario"]').val()
    };

    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Cargo/Index",
        data: dados,
        beforeSend: function () {
            $('#btnCadastrarCargo').val('Cadastrando...')
        },
        success:
            setTimeout(function () {
                location.reload(true);
            }, 500)
    });
});
