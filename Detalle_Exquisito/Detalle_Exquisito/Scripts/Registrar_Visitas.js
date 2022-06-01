var pasos = 0;
var DivPadres = document.querySelectorAll("div");
var imgList = document.querySelectorAll("div ul li img");
var listasP = document.querySelectorAll("p");
var btn_Siguiente = document.getElementById("btn_siguiente");
var Guardar_Cuenta = document.getElementById("Guardar_Cuenta");
var i = 0;
var permiso1;
var permiso3;
var inputs = document.querySelectorAll("div input");
console.log(listasP);
console.log(inputs);
var text1Permisos;var text2Permisos; var text3Permisos; 
var text4Permisos; var text5Permisos; var text6Permisos;
btn_Siguiente.style.cursor = "not-allowed";
Guardar_Cuenta.style.cursor = "not-allowed";


inputs[1].addEventListener('keyup', function () {
    if (validarPalabra(inputs[1].value).length > 2) {
        text1Permisos = true;
        listasP[0].textContent = "";
        DivPadres[3].style.height = "50%";
    }
    else {
        text1Permisos = false;
        listasP[0].textContent = "El nombre introducido no es valido.";
        DivPadres[3].style.height = "60%";
    }
    if (text1Permisos == true & text2Permisos == true & text3Permisos == true) {
        inputs[1].value = validarPalabra(inputs[1].value);
        inputs[2].value = validarPalabra(inputs[2].value);
        inputs[3].value = validarPalabra(inputs[3].value);
        console.log("gatos");
        btn_Siguiente.style.cursor = "pointer";
        document.getElementById("btn_siguiente").setAttribute("onclick", "MostrarAlerta()");
    }
    else {
        btn_Siguiente.style.cursor = "not-allowed";
        document.getElementById("btn_siguiente").removeAttribute("onclick", "MostrarAlerta()");
    }
});


inputs[2].addEventListener('keyup', function () {
    if (validarPalabra(inputs[2].value).length > 2) {
        text2Permisos = true;
        listasP[1].textContent = "";
        DivPadres[3].style.height = "50%";
    }
    else {
        text2Permisos = false;
        listasP[1].textContent = "El Apellido Paterno no es valido.";
        DivPadres[3].style.height = "53%";
    }
    if (text1Permisos == true & text2Permisos == true & text3Permisos == true) {
        inputs[1].value = validarPalabra(inputs[1].value);
        inputs[2].value = validarPalabra(inputs[2].value);
        inputs[3].value = validarPalabra(inputs[3].value);
        console.log("gatos");
        btn_Siguiente.style.cursor = "pointer";
        document.getElementById("btn_siguiente").setAttribute("onclick", "MostrarAlerta()");
    }
    else {
        btn_Siguiente.style.cursor = "not-allowed";
        document.getElementById("btn_siguiente").removeAttribute("onclick", "MostrarAlerta()");
    }
});

inputs[3].addEventListener('keyup', function () {
    if (validarPalabra(inputs[3].value).length > 2) {
        text3Permisos = true;
        listasP[2].textContent = "";
        DivPadres[3].style.height = "50%";
    }
    else {
        text3Permisos = false;
        listasP[2].textContent = "El Apellido Materno no es valido.";
        DivPadres[3].style.height = "53%";
    }

    if (text1Permisos == true & text2Permisos == true & text3Permisos == true) {
        inputs[1].value = validarPalabra(inputs[1].value);
        inputs[2].value = validarPalabra(inputs[2].value);
        inputs[3].value = validarPalabra(inputs[3].value);
        btn_Siguiente.style.cursor = "pointer";
        document.getElementById("btn_siguiente").setAttribute("onclick", "MostrarAlerta()");
    }
    else {
        console.log("gatos");
        btn_Siguiente.style.cursor = "not-allowed";
        document.getElementById("btn_siguiente").removeAttribute("onclick", "MostrarAlerta()");
    }
});


const validarPalabra = function (valor) {
    var newAr = valor.split(/[ .,]+/).filter(String);

    valor = "";
    for (var i = 0; i < newAr.length; i++) {
        valor = valor + " " + newAr[i];
    }
    return valor;
}

inputs[4].addEventListener('keyup', function () {
    if (inputs[4].value.length > 6) {
        text4Permisos = true;
        listasP[3].textContent = "";
        DivPadres[7].style.height = "50%";
    }
    else {
        text4Permisos = false;
        listasP[3].textContent = "Su carnet de identidad no es valido.";
        DivPadres[7].style.height = "53%";
    }
    if (text4Permisos == true & text5Permisos == true & text6Permisos == true) {
        btn_Siguiente.style.cursor = "pointer";
        document.getElementById("btn_siguiente").setAttribute("onclick", "MostrarAlerta()");
    }
    else {
        btn_Siguiente.style.cursor = "not-allowed";
        document.getElementById("btn_siguiente").removeAttribute("onclick", "MostrarAlerta()");
    }
});


inputs[5].addEventListener('keyup', function () {
    if (inputs[5].value.length > 6 & validarEmail(inputs[5].value)==true) {
        text5Permisos = true;
        listasP[4].textContent = "";
        DivPadres[7].style.height = "50%";
    }
    else {
        text5Permisos = false;
        listasP[4].textContent = "Cuenta de Email incorrecta.";
        DivPadres[7].style.height = "53%";
    }
    if (text4Permisos == true & text5Permisos == true & text6Permisos == true) {
        btn_Siguiente.style.cursor = "pointer";
        document.getElementById("btn_siguiente").setAttribute("onclick", "MostrarAlerta()");
    }
    else {
        btn_Siguiente.style.cursor = "not-allowed";
        document.getElementById("btn_siguiente").removeAttribute("onclick", "MostrarAlerta()");
    }
});

const validarEmail = function (valor) {
    if (/^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i.test(valor)) {
        return true;
    } else {
        return false;
    }
}
inputs[6].addEventListener('keyup', function () {
    if (validarPalabra(inputs[6].value).length > 5) {
        text6Permisos = true;
        listasP[5].textContent = "";
        DivPadres[7].style.height = "50%";
    }
    else {
        text6Permisos = false;
        listasP[5].textContent = "El numero de telefono/celular no es valido.";
        DivPadres[7].style.height = "53%";
    }
    if (text4Permisos == true & text5Permisos == true & text6Permisos == true) {
        btn_Siguiente.style.cursor = "pointer";
        document.getElementById("btn_siguiente").setAttribute("onclick", "MostrarAlerta()");
    }
    else {
        btn_Siguiente.style.cursor = "not-allowed";
        document.getElementById("btn_siguiente").removeAttribute("onclick", "MostrarAlerta()");
    }
});

inputs[7].addEventListener('keyup', function () {
    const nroLetras = inputs[7].value.length;

    if (nroLetras >= 8) {
        permiso3 = Regla3(inputs[7].value);
        imgList[1].src = "../Imagenes/marca_verificacion.png";
        permiso1 = Regla1(inputs[7].value);
    } else {
        imgList[1].src = "../Imagenes/marcaX.png";
        permiso3 = false;
        permiso1 = false;
    }
    i = nroLetras;

    if (permiso1 == true) {
        imgList[0].src = "../Imagenes/marca_verificacion.png"
    }
    else {
        imgList[0].src = "../Imagenes/marcaX.png";
    }

    if (permiso3 == true) {
        imgList[2].src = "../Imagenes/marca_verificacion.png"
    } else {
        imgList[2].src = "../Imagenes/marcaX.png";
    }

    if (nroLetras >= 8 & permiso3 == true & permiso1 == true) {
        Guardar_Cuenta.style.cursor = "pointer";
        Guardar_Cuenta.setAttribute("onclick", "desactivar()");
    }
    else {
        Guardar_Cuenta.style.cursor = "not-allowed";
        Guardar_Cuenta.removeAttribute("onclick", "desactivar()");
    }

});

const Regla1 = function (valor) {
    var newAr = valor.split(/[ .,]+/).filter(String);

    for (var i = 0; i < newAr.length; i++) {
        if (newAr[i].match(/[A-Z]/)) {
            return true;
        }
    }
    return false;
}

const Regla3 = function (valor) {
    var contraseña = valor;
    var decision1 = false;
    var decision2 = false;

    for (var i = 0; i < contraseña.length; i++) {
        if (i == contraseña.length) {
            valor = contraseña.substring(contraseña.length, contraseña.length);
        }
        else {
            valor = contraseña.substring(i, i + 1);
        }
        if (valor != " " & valor != "!" & valor != '"' & valor != "$" & valor != "%" & valor != "*" & valor != 0 & valor != 1 & valor != 2 & valor != 3 & valor != 4 & valor != 5 & valor != 6 & valor != 7 & valor != 8 & valor != 9) {
            decision1 = true;
        }
        if (valor == 0 || valor == 1 || valor == 2 || valor == 3 || valor == 4 || valor == 5 || valor == 6 || valor == 7 || valor == 8 || valor == 9) {
            decision2 = true;
        }
    }

    if (decision1 == decision2) {
        return true;
    }
    if (decision1 == decision2) {
        return false;
    }
}

function MostrarAlerta() {
    pasos++;
    if (pasos == 1) {
        DivPadres[3].style.left = "100px";
        DivPadres[3].style.display = "none";
        DivPadres[7].style.display = "flex";

    }

    if (pasos == 2) {
        DivPadres[7].style.display = "none";
        DivPadres[11].style.display = "flex";

        document.getElementById("Ventana_Registro").style.height = "315px";
        document.getElementById("Ventana_Registro").style.justifyContent = "space-evenly";

        document.getElementById("h3_titulos").innerHTML = "Elige una contraseña";
        document.getElementById("btn_siguiente").style.display = "none";
        document.getElementById("Guardar_Cuenta").style.display = "inline-block";
    }
}

function desactivar() {
    document.getElementById("mesajeUsuario").style.display="block";
}
