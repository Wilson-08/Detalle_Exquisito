const cantidad = document.getElementsByClassName("textosCantidad");
const dedicatorias = document.getElementsByClassName("textosDedicatorias");
const antiguo = "";
const Pedir = document.getElementById("Pedir");

function subir(id, Cod_Producto, nombreProducto, precio) {
    let posicion = id.substring(6, id.length);
    const tr_copias = document.querySelectorAll("tr");
    cantidad[parseInt(posicion)].value = parseInt(cantidad[parseInt(posicion)].value) + 1;

    const cantidad_prod = cantidad[parseInt(posicion)].value;
    const dedicatoria_prod = dedicatorias[parseInt(posicion)].value;
    
    const padre = document.getElementById("Lista_Orden");

    var tr = document.createElement("tr");

    var tdProducto = document.createElement("td");
    var tdDedicatoria = document.createElement("td");
    var tdCantidad = document.createElement("td");
    var tdCosto = document.createElement("td");
    var tdCodigo = document.createElement("td");

    var contenidoProducto = document.createTextNode(nombreProducto);
    var contenidoCantidad = document.createTextNode(cantidad_prod);
    var contenidoCosto = document.createTextNode(precio + "Bs");
    var contenidoCodigo = document.createTextNode(Cod_Producto);
    var contenidoDedicatoria = document.createTextNode(dedicatoria_prod+" GA");


    tdProducto.appendChild(contenidoProducto);
    tdCantidad.appendChild(contenidoCantidad);
    tdCosto.appendChild(contenidoCosto);
    tdCodigo.appendChild(contenidoCodigo);
    tdDedicatoria.appendChild(contenidoDedicatoria);

    tdCodigo.setAttribute("hidden", "");
    tdDedicatoria.setAttribute("hidden", "");

    tr.appendChild(tdProducto);
    tr.appendChild(tdCantidad);
    tr.appendChild(tdCosto);
    tr.appendChild(tdCodigo)
    tr.appendChild(tdDedicatoria)

    const id_tr = "tr_" + Cod_Producto;
    tr.setAttribute("class", "sinEspacios");
    tr.setAttribute("id", id_tr);

    let copia = 0;
    for (var i = 0; i < tr_copias.length; i++) {
        if (tr_copias[i].id == id_tr) {
            
        } else {
            copia++;
        }
    }

    if (copia == tr_copias.length) {
        padre.appendChild(tr);
        PedirYa();
    } else {
        document.getElementById(id_tr).children[1].innerHTML = cantidad_prod;
        document.getElementById(id_tr).children[2].innerHTML = (parseInt(cantidad_prod) * parseInt(precio)) + "Bs";
    }
    var CostoTotal = 0;

    const conseguirTR = document.querySelectorAll("tr");
    for (var i = 1; i < conseguirTR.length; i++) {
        if (parseInt(conseguirTR[i].children[2].innerHTML) > 0) {
            CostoTotal = CostoTotal + parseInt(conseguirTR[i].children[2].innerHTML);
        }
    }
    document.getElementById("TotalCosto").children[1].innerHTML = CostoTotal;
}

function bajar(id, Cod_Producto, precio) {
    let posicion = id.substring(6, id.length);
    const padre = document.getElementById("Lista_Orden");

    if (cantidad[parseInt(posicion)].value > 0) {
        cantidad[parseInt(posicion)].value = parseInt(cantidad[parseInt(posicion)].value) - 1;
        let codigo = "tr_" + Cod_Producto;
        var hijos = document.getElementById(codigo);
        let precioN = document.getElementById(codigo).children[2].innerHTML.substring(0, (document.getElementById(codigo).children[2].innerHTML.length - 2));
        document.getElementById(codigo).children[1].innerHTML = cantidad[parseInt(posicion)].value;
        precioN = precioN - precio;
        if (precioN == 0) {
            padre.removeChild(hijos);
        } else {
            document.getElementById(codigo).children[2].innerHTML = precioN + "Bs";
        }
        var gatos = 0;
        var total = document.getElementById("TotalCosto").children[1].innerHTML;
        gatos = parseInt(total) - (parseInt(precio));
        document.getElementById("TotalCosto").children[1].innerHTML = gatos;
    }
}
function mostrar_imagen(Cod_Producto) {
    const div = document.getElementById("mostrar_completa");
    div.style.display = "block";
    document.getElementById("imagen-p").src = "/Productos/getImagen/" + Cod_Producto;
}

Pedir.addEventListener("click", function () {
    const padre = document.getElementById("Pedido_ordenes");
    const valores = document.getElementsByClassName("sinEspacios");

    for (var i = 0; i < valores.length; i++) {
        var inputCodigo = document.createElement("input");
        var inputDedicatoria = document.createElement("input");
        var inputCantidad = document.createElement("input");
        var input_Total = document.createElement("input");

        inputCodigo.name = "detalle[" + i + "].cod_prod_dc";
        inputCodigo.value = valores[i].children[3].innerHTML;
        inputCodigo.type = "hidden";

        inputDedicatoria.name = "detalle[" + i + "].dedicatoria";
        inputDedicatoria.value = valores[i].children[4].innerHTML;
        inputDedicatoria.type = "hidden";

        inputCantidad.name = "detalle[" + i + "].cantidad_compra";
        inputCantidad.value = valores[i].children[1].innerHTML;
        inputCantidad.type = "hidden";

        input_Total.name = "detalle[" + i + "].Total_Detalle";
        input_Total.value = parseInt(valores[i].children[2].innerHTML.substring(0, (valores[i].children[2].innerHTML.length - 2)));
        input_Total.type = "hidden";

        padre.appendChild(inputCodigo);
        padre.appendChild(inputDedicatoria);
        padre.appendChild(inputCantidad);
        padre.appendChild(input_Total);
    }

    var inputCostoTOTAL = document.createElement("input");

    inputCostoTOTAL.name = "Costo_Total";
    inputCostoTOTAL.value = document.getElementById("Costo_Total").innerHTML;
    inputCostoTOTAL.type = "hidden";

    padre.appendChild(inputCostoTOTAL);


    document.getElementById("formulario_Final").style.display = "flex";
});

document.getElementById("EnviarPedido").addEventListener("click", function () {
    
})

function PedirYa() {
    const Pedir = document.getElementById("Pedir");
    Pedir.removeAttribute("disabled","disable");
}


