    const cantidad = document.querySelectorAll("input");
    const antiguo = "";

    function subir(id) {
        let posicion = id.substring(6, id.length);
    cantidad[parseInt(posicion)].value = parseInt(cantidad[parseInt(posicion)].value) + 1;
    }

    function bajar(id) {
        let posicion = id.substring(6, id.length);
        if (cantidad[parseInt(posicion)].value > 0) {
        cantidad[parseInt(posicion)].value = parseInt(cantidad[parseInt(posicion)].value) - 1;
        }
    }
    function mostrar_imagen(Cod_Producto) {
        const div = document.getElementById("mostrar_completa");
        div.style.display = "block";
        document.getElementById("imagen-p").src = "/Productos/getImagen/" + Cod_Producto;
    }

addEventListener("keydown", function (e) {
    const ventana = document.getElementById("mostrar_completa");
    if (ventana.style.display=="block") {
        if (this.event.keyCode == 27) {
            ventana.style.display = "none";
        }
    }
});

function mostrar_Descripcion(Cod_Producto) {
    document.getElementById("p_" + Cod_Producto).style.display = "block";
}
function ocultar_Descripcion(Cod_Producto) {
    document.getElementById("p_" + Cod_Producto).style.display = "none";
}

/*document.getElementById("Producto_Lista").addEventListener("click", function () {
    console.log("gatos");
});
document.onmousemove = function (e) {
    console.log("Ubicacion X: " + e.pageX + " Ubicaciom Y: " + e.pageY)
    if (parseInt(e.pageX) < 323 & parseInt(e.pageX) > 1131) {
        if (e.pageY < 108 & e.pageY > 578) {
            console.log("me gustan los gatos :3");
        }
    }
}*/