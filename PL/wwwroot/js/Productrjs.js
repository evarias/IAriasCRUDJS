$(document).ready(function () { //click
    GetAll();
});
 
function GetAll() {    
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5172/api/Product/GetAll',
        //dataType: 'json',
        //contentType: "application/json",
        success: function (result)
        { //200 OK 
            $('#tableuser tbody').empty();
            $.each(result.Objects, function (i, tableuser) {
                var filas =
                    '<tr>'
                        + '<td class="text-center"> '
                    + '<a href="#" onclick="GetById(' + tableuser.IdUsuario + ')">'
                                + '<img  style="height: 25px; width: 25px;" src="../img/edit.ico" />'
                            + '</a> '
                        + '</td>'
                    + "<td  id='id' class='text-center'>" + tableuser.IdSubCategoria + "</td>"
                    + "<td class='text-center'>" + tableuser.Nombre + "</td>"
                    + "<td class='text-center'>" + tableuser.Descripcion + "</ td>"
                    + "<td class='text-center'>" + tableuser.Categoria.IdCategoria + "</td>"
                        //+ '<td class="text-center">  <a href="#" onclick="return Eliminar(' + subCategoria.IdSubCategoria + ')">' + '<img  style="height: 25px; width: 25px;" src="../img/delete.png" />' + '</a>    </td>'
                    + '<td class="text-center"> <button class="btn btn-danger" onclick="Eliminar(' + tableuser.IdSubCategoria + ')"><span class="glyphicon glyphicon-trash" style="color:#FFFFFF"></span></button></td>'
 
                    + "</tr>";
    
                    $("#SubCategorias tbody").append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};