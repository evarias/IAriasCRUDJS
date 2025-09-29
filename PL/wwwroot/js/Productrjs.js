$(document).ready(function () { //click   
    GetAll();
    limpiar();   
});

function limpiar() {
    $('#exampleModal').on('hidden.bs.modal', function () {     
        $('#productName').val('');
        $('#QuantityPerUnit').val('');
        $('#UnitPrice').val('');
        $('#UnitsInStock').val('');
        $('#UnitsOnOrder').val('');
        $('#ReorderLevel').val('');
        $('#Discontinued').val('');
        $('#Supplier').val('');
        $('#Category').val('');
    });
}
function GetAll() {
    limpiar();
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5172/api/Product/GetAll',
        success: function (result) { //200 OK 
            $('#tableproduct tbody').empty();
            $.each(result.objects, function (i, producto) {
                var filas =
                    '<tr>'
                        + '<td class="text-center"> '
                    + '<a href="#" onclick="GetById(' + producto.productID + ')">'
                    + '<img  style="height: 25px; width: 25px;" src="https://www.svgrepo.com/show/402308/pencil.svg" />'
                            + '</a> '
                        + '</td>'
                    + "<td  id='id' class='text-center'>" + producto.productName + "</td>"
                    + "<td class='text-center'>" + producto.quantityPerUnit + "</td>"
                    + "<td class='text-center'>" + producto.unitPrice + "</ td>"
                    + "<td class='text-center'>" + producto.unitsInStock + "</ td>"
                    + "<td class='text-center'>" + producto.unitsOnOrder + "</ td>"
                    + "<td class='text-center'>" + producto.reorderLevel + "</ td>"
                    + "<td class='text-center'>" + producto.discontinued + "</ td>"
                    + "<td class='text-center'>" + producto.supplier.companyName + "</td>"
                    + "<td class='text-center'>" + producto.category.categoryName + "</td>"
                    + '<td class="text-center"> <button class="btn btn-danger" onclick="Delete(' + producto.productID + ')"> </button></td>'

                    + "</tr>";

                $("#tableproduct tbody").append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};

function AddProduct() {

    var product = {
       
        productName: $('#productName').val(),
        quantityPerUnit: $('#QuantityPerUnit').val(),
        unitPrice: Number($('#UnitPrice').val()),
        unitsInStock: Number($('#UnitsInStock').val()),
        unitsOnOrder: Number($('#UnitsOnOrder').val()),
        reorderLevel: Number($('#ReorderLevel').val()),
        discontinued: $('#Discontinued').is(':checked'),
        supplier: { supplierID: Number($('#Supplier').val()) },
        category: { categoryID: Number($('#Category').val()) }
    }

    $.ajax({
        type: "POST",
        url: 'http://localhost:5172/api/Product/Add',
        data: JSON.stringify(product),
        contentType: "application/json",
        success: function () {
            alert("Producto agregado");
            const modal = bootstrap.Modal.getInstance(document.getElementById('exampleModal'));
            modal.hide();
            GetAll();
        },
        error: function () {
            alert('Error en la consulta.');
        }
    })
}

function UpdateProduct() {
    var product = {
        productID: Number($('#productID').val()),
        productName: $('#productName').val(),
        quantityPerUnit: $('#QuantityPerUnit').val(),
        unitPrice: Number($('#UnitPrice').val()),
        unitsInStock: Number($('#UnitsInStock').val()),
        unitsOnOrder: Number($('#UnitsOnOrder').val()),
        reorderLevel: Number($('#ReorderLevel').val()),
        discontinued: $('#Discontinued').is(':checked'),
        supplier: { supplierID: $('#Supplier').val() },
        category: { categoryID: $('#Category').val() }
    }

    $.ajax({
        type: "PUT",
        url: 'http://localhost:5172/api/Product/Update',
        data: JSON.stringify(product),
        contentType: "application/json",
        success: function () {
            alert("Producto actualizado");
            const modal = bootstrap.Modal.getInstance(document.getElementById('exampleModal'));
            modal.hide();
            GetAll();
        },
        error: function () {
            alert('Error en la consulta.');
        }
    })
}

function GetById(productID) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5172/api/Product/GetById?id=' + productID,
        success: function (result) { 
            if (result.correct) {
                $('#productID').val(result.object.productID);
                $('#productName').val(result.object.productName);
                $('#QuantityPerUnit').val(result.object.quantityPerUnit);
                $('#UnitPrice').val(result.object.unitPrice);
                $('#UnitsInStock').val(result.object.unitsInStock);
                $('#UnitsOnOrder').val(result.object.unitsOnOrder);
                $('#ReorderLevel').val(result.object.reorderLevel);
                $('#Discontinued').val(result.object.discontinued);
                $('#Supplier').val(result.object.supplier.supplierID);
                $('#Category').val(result.object.category.categoryID);
              
                $('#exampleModal').modal('show');
                $('#add').hide();
                $('#update').show();

            }
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};


function Delete(productID) {
    $.ajax({
        type: 'DELETE',
        url: 'http://localhost:5172/api/Product/Delete?id=' + productID,
        success: function () {
            alert('producto borrado de manera exitosa');
            
            GetAll();
        },
        
    });
};