function initializeMap(geoJsonData) {
    let mapOptions = {
        center:[46.0569, 14.5058],
        zoom:10
       }
    let map = new L.map('map' , mapOptions);
    let layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');
    map.addLayer(layer);
    let iconOptions = {
        title:'park',
        draggable:true,
       }
    let marker = new L.Marker([46.0569, 14.5058], iconOptions);
    marker.addTo(map);
    geoJsonData.geometries.forEach(function (feature) {
        console.log(feature);  
        if (feature.type === "Point") {
            var lat = feature.coordinates[1]; // Latitude
            var lon = feature.coordinates[0]; // Longitude
            let iconOptions = {
                title: 'park',
                draggable: true,
            };
            let marker = new L.Marker([lat, lon], iconOptions); // Correct lat/lon
            marker.addTo(map);
        }
    });
    
}
