let mapa;


function iniciarMap() {
    var Ubicacion_Latitud = -17.781518506466846;
    var Ubicacion_Longitud = -63.181961124026;

    var coordenadas = {
        lng: Ubicacion_Longitud,
        lat: Ubicacion_Latitud
    }

    generarMapa(coordenadas);
}
function generarMapa(coordenadas) {
    mapa = new google.maps.Map(document.getElementById("mapa"), {
        center: new google.maps.LatLng(coordenadas.lat, coordenadas.lng),
        zoom: 13
    });
    marker = new google.maps.Marker({
        map: mapa,
        draggable: true,
        position: new google.maps.LatLng(coordenadas.lat, coordenadas.lng),
    });
    var contentStringAviso = ''+
                            '<div id="siteNotice">'+
                            '</div>' +
                                '<h5 id="firstHeading" style="font-weight:bold; text-align:center;" class="firstHeading">Consejo...</h5>' +
                            '<div id="bodyContent">'+
                                '<p>Arrastra el marcador hacia la ubicacion de la entrega.</p>'+
                            '</div>';

    var infoWindow = new google.maps.InfoWindow({
        content: contentStringAviso
    });
    infoWindow.open(mapa, marker);


    marker.addListener('mouseover', function () {
        infoWindow.close();
    });
    marker.addListener('mouseup', function () {
        infoWindow.open(mapa, marker);
    });

    marker.addListener('dragend', function () {
        document.getElementById("Ubicacion_Latitud").value = this.getPosition().lat();
        document.getElementById("Ubicacion_Longitud").value = this.getPosition().lng();
    });
}

function iniciarMapa(Ubicacion_Latitud, Ubicacion_Longitud) {
    document.getElementById("ubicacion_consulta").style.display = "flex";

    mapa = new google.maps.Map(document.getElementById("mapa_consulta"), {
        center: new google.maps.LatLng(Ubicacion_Latitud, Ubicacion_Longitud),
        zoom: 13
    });
    marker = new google.maps.Marker({
        map: mapa,
        draggable: false,
        position: new google.maps.LatLng(Ubicacion_Latitud, Ubicacion_Longitud),
    });
    
    const boton = document.getElementsByTagName("button");
    
}