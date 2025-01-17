function initializeMap(geoJsonData) {
    if (!geoJsonData || !geoJsonData.geometries) {
        console.error("Invalid GeoJSON data.");
        return;
    }

    let mapOptions = {
        center: [46.0569, 14.5058],
        zoom: 10
    };

    let map = new L.map('map', mapOptions);

    let layer = new L.TileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    });
    map.addLayer(layer);

    geoJsonData.geometries.forEach(function (feature) {
        if (feature.type === "Point" && Array.isArray(feature.coordinates)) {
            var lat = feature.coordinates[1];
            var lon = feature.coordinates[0];

            if (!isFinite(lat) || !isFinite(lon)) {
                console.error("Invalid coordinates:", feature.coordinates);
                return;
            }

            let iconOptions = {
                title: 'Park',
                draggable: true
            };

            let marker = new L.Marker([lat, lon], iconOptions);
            
            marker.bindPopup(
                `<div>
                    <a href='/home?location=${encodeURIComponent(lat)},${encodeURIComponent(lon)}' class='reserve-link'>
                        Reserve Now
                    </a>
                </div>`
            );

            marker.addTo(map);
        } else {
            console.warn("Unsupported feature type or invalid geometry:", feature);
        }
    });
}
