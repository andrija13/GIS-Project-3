// Leaflet map
var map;

// Layers
var education;
var hospital;
var gas;
var sport;
var pharmacy;
var forest;
var river;
var railway;
var roads;
var basicFilterLayer;
var spatialFilterLayer;
var temporalLayer;
var trajectoryLayer;
var trafficLayer;

//Layers styles
var educationStyle = { "color": "#684013", "weight": 4, "opacity": 0.7 };
var hospitalStyle = { "color": "#d4030ab3", "weight": 4, "opacity": 0.7 };
var sportStyle = { "color": "#f7bd07b3", "weight": 4, "opacity": 0.7 };
var forestStyle = { "color": "black", "fillColor": "#66FF66", "fillOpacity":1, "weight": 1, "opacity": 1 };
var riverStyle = { "color": "#0000FF", "weight": 2, "opacity": 1 };
var railwayStyle = { "color": "#000000", "weight": 2, "opacity": 1 };
var roadStyle = { "color": "#AA3333", "weight": 2, "opacity": 1 };

//Feature type
const line = "planet_osm_line";
const point = "planet_osm_point";
const poly = "planet_osm_polygon";
const traffic = "sumo_fcd_data";

//Deafult filters;
const defEducationFiler = "(amenity IN ('kindergarten', 'school', 'college', 'university','language_school') OR office='educational_institution')"
const defHospitalFiler = "amenity IN ('hospital')";
const defSportFilter = "leisure IN ('pitch','sports_centre','sports_hall','stadium')";
const defGasFilter = "amenity IN ('fuel')";
const defPharmacyFilter = "amenity IN ('pharmacy')";
const defForestFilter = "(landuse IN ('forest') OR natural IN ('wood'))";
const defRiverFilter = "waterway IN ('river', 'stream')";
const defRailwayFilter = "railway IS NOT NULL";
const defRoadsFilter = "highway IS NOT NULL";

//Layer group
var layerGroup = new L.LayerGroup();

//Legend
var legend = L.control({ position: 'topright' });

//Properties
var lineProperties = [];
var pointProperties = [];
var polyProperties = [];

$(document).ready(function () {
    map = L.map('map').setView([43.323, 21.894700], 14);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    layerGroup.addTo(map);
    legend.addTo(map);

    getProperties(line);
    getProperties(point);
    getProperties(poly);
});

function educationChange() {
    if ($('#educationCheck').is(':checked')) {
        var filter = defEducationFiler;
        callWFSService(education, educationStyle, poly, filter).then((res) => {
            education = res;
        });
    }
    else {
        layerGroup.removeLayer(education);
    }
}

function hospitalChange() {
    if ($('#hospitalCheck').is(':checked')) {
        var filter = defHospitalFiler;
        callWFSService(hospital, hospitalStyle, poly, filter).then((res) => {
            hospital = res;
        });
    }
    else {
        layerGroup.removeLayer(hospital);
    }
}

function gasChange() {
    if ($('#gasCheck').is(':checked')) {
        var filter = defGasFilter;
        callWFSService(gas, null, point, filter).then((res) => {
            gas = res;
        });
    }
    else {
        layerGroup.removeLayer(gas);
    }
}

function sportChange() {
    if ($('#sportCheck').is(':checked')) {
        var filter = defSportFilter;
        callWFSService(sport, sportStyle, poly, filter).then((res) => {
            sport = res;
        });
    }
    else {
        layerGroup.removeLayer(sport);
    }
}

function pharmacyChange() {
    if ($('#pharmacyCheck').is(':checked')) {
        var filter = defPharmacyFilter;
        callWFSService(pharmacy, null, point, filter).then((res) => {
            pharmacy = res;
        });
    }
    else {
        layerGroup.removeLayer(pharmacy);
    }
}

function forestChange() {
    if ($('#forestCheck').is(':checked')) {
        var filter = defForestFilter;
        forest = callWMSService(forest, "ForestStyle", poly, filter)
    }
    else {
        layerGroup.removeLayer(forest);
    }
}

function riverChange() {
    if ($('#riverCheck').is(':checked')) {
        var filter = defRiverFilter;
        river = callWMSService(river, 'line', line, filter)
    }
    else {
        layerGroup.removeLayer(river);
    }
}

function railwayChange() {
    if ($('#railwayCheck').is(':checked')) {
        var filter = defRailwayFilter;
        railway = callWMSService(railway, 'RailwaysStyle', line, filter)
    }
    else {
        layerGroup.removeLayer(railway);
    }
}

function roadChange() {
    if ($('#roadCheck').is(':checked')) {
        var filter = defRoadsFilter;
        roads = callWMSService(roads, 'simple_roads', line, filter)
    }
    else {
        layerGroup.removeLayer(roads);
    }
}

// Ajax call WFS service - GetFeature request
function callWFSService(layer, style, type, filter, addToMap = 1) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "http://localhost:8080/geoserver/GIS/wfs",
            data: {
                service: "WFS",
                version: "1.0.0",
                request: "GetFeature",
                typeName: type,
                cql_filter: filter,
                outputFormat: "application/json",
                srsName: "epsg:4326",
            },
            dataType: "json",
            success: function (response) {
                layer = L.geoJSON(response, {
                    style: style,
                    pointToLayer: createCustomMarker,
                    onEachFeature: addPopup
                });

                if (addToMap) {
                    layerGroup.addLayer(layer);
                }

                resolve(layer);
            },
            error: function (error) {
                reject(error)
            }
        })
    })
}

// Leaflet call WMS service - GetMap request
function callWMSService(layer, style, type, filter) {
    var layer = new L.tileLayer.wms(
        'http://localhost:8080/geoserver/GIS/wms',
        {
            layers: type,
            format: 'image/png',
            styles: style,
            transparent: true,
            cql_filter: filter
        },
    )
    layerGroup.addLayer(layer);
    return layer;
}

function getProperties(type) {
    $.ajax({
        url: "http://localhost:8080/geoserver/GIS/wfs",
        data: {
            service: "WFS",
            version: "1.0.0",
            request: "DescribeFeatureType",
            typeName: type,
            outputFormat: "application/json"
        },
        dataType: "json",
        success: function (response) {
            if (type == "planet_osm_line" && lineProperties.length == 0) {
                response.featureTypes[0].properties.forEach(x => {
                    if (x.name != 'osm_id' && x.name != 'z_order' && x.name != 'way'
                        && x.name != 'way_area' && x.name != 'access' && x.name != 'admin_level') {
                        lineProperties.push(x.name)
                    }
                });
            }

            if (type == "planet_osm_polygon" && polyProperties.length == 0) {
                response.featureTypes[0].properties.forEach(x => {
                    if (x.name != 'osm_id' && x.name != 'z_order' && x.name != 'way'
                        && x.name != 'way_area' && x.name != 'access' && x.name != 'admin_level') {
                        polyProperties.push(x.name)
                    }
                });
            }

            if (type == "planet_osm_point" && pointProperties.length == 0) {
                response.featureTypes[0].properties.forEach(x => {
                    if (x.name != 'osm_id' && x.name != 'z_order' && x.name != 'way'
                        && x.name != 'way_area' && x.name != 'access' && x.name != 'admin_level') {
                        pointProperties.push(x.name)
                    }
                });
            }
        },
        error: function (error) {
            console.log(error);
        }
    })
}

// Add popup to layer
function addPopup(feature, layer) {
    if (feature.properties != null) {
        var infoText = '';
        for (const [key, value] of Object.entries(feature.properties)) {
            if (key != 'osm_id' && key != 'z_order' && key != 'way_area' && value != null) {
                if (key == "vehicle_speed" || key == "average_speed") {
                    infoText += `${key}: ${parseFloat(value * 1.609344).toFixed(2)}, `;
                }
                else {
                    infoText += `${key}: ${value}, `;
                }
            }

        }
        layer.bindPopup(infoText);
    }
}

// Custom marker to layer
function createCustomMarker(feature, latlng) {
    if (feature.properties != null && feature.properties.amenity != null && feature.properties.amenity == 'pharmacy') {
        var myIcon = L.icon({
            iconUrl: 'marker-red.png',
            iconSize: [20, 35],
            iconAnchor: [10, 35],
            popupAnchor: [0, -35],
        })
        return L.marker(latlng, { icon: myIcon });
    }
    else if (feature.properties != null && feature.properties.amenity != null && feature.properties.amenity == 'fuel') {
        var myIcon = L.icon({
            iconUrl: 'marker-default.png',
            iconSize: [20, 35],
            iconAnchor: [10, 35],
            popupAnchor: [0, -35],
        })
        return L.marker(latlng, { icon: myIcon });
    }
    else{
        var style = {
            radius: 3,
            color: '#FF0000',
            opacity: 0.75,
        }
        return L.circleMarker(latlng, style);
    }
}

$('#BasicFilterLayer').on('change', function (e) {
    $('#BasicAttribute').empty().append('<option selected="selected" hidden disabled>Select attribute</option>')
    var arrayAttribute = getPropertyArrayBySelectValue(this.value);
    for (const att of arrayAttribute) {
        $('#BasicAttribute').append($(document.createElement('option')).prop({
            value: att,
            text: att
        }))
    }
});


$('#SpatialOperator').on('change', function (e) {
    if (this.value == "4") {
        $('#spatialDistance').show();
    }
    else {
        $('#spatialDistance').hide();
    }
});

function basicSearch() {
    var layer = $('#BasicFilterLayer').val();
    if (layer == "" || layer == null) {
        alert('Please choose layer');
        return;
    }
    
    var attribute = $('#BasicAttribute').val();
    if (attribute == "" || attribute == null) {
        alert('Please choose attribute');
        return;
    }

    var condition = $('#basicFilterCondition').val();
    if (condition == "" || condition == null) {
        alert('Please type condition');
        return;
    }

    var type = getFeatureTypeBySelectValue(layer);
    var defaultFilter = getDefaultFilterBySelectValue(layer);
    var style = getLayerStyleBySelectValue(layer);

    if (basicFilterLayer != undefined && basicFilterLayer != null) {
        layerGroup.removeLayer(basicFilterLayer);
    }

    callWFSService(basicFilterLayer, style, type, defaultFilter + " AND " + attribute + " " + condition)
        .then((res) => {
        basicFilterLayer = res;
    });
}

function resetBasicSearch() {
    $('#BasicFilterLayer').val("");
    $('#BasicAttribute').val("");
    $('#basicFilterCondition').val("");
    $('#BasicAttribute').empty().append('<option selected="selected" hidden disabled>Select attribute</option>')

    if (basicFilterLayer != undefined && basicFilterLayer != null) {
        layerGroup.removeLayer(basicFilterLayer);
    }
}

function spatialSearch() {
    var leftLayer = $('#SpatialLeftLayer').val();
    if (leftLayer == "" || leftLayer == null) {
        alert('Please choose first layer');
        return;
    }

    var operator = $('#SpatialOperator').val();
    if (operator == "" || operator == null) {
        alert('Please choose operator');
        return;
    }

    var distance = null;
    if (operator == "4") {
        distance = $('#spatialDistance').val();
        if (distance == "" || distance == null) {
            alert('Please choose distance');
            return;
        }
    }

    var rightLayer = $('#SpatialRightLayer').val();
    if (rightLayer == "" || rightLayer == null) {
        alert('Please choose second layer');
        return;
    }

    var style = getLayerStyleBySelectValue(leftLayer);

    if (spatialFilterLayer != undefined && spatialFilterLayer != null) {
        layerGroup.removeLayer(spatialFilterLayer);
    }

    getSpatialJoinData(leftLayer, operator, distance, rightLayer, style);
}

function getPropertyArrayBySelectValue(value) {
    switch (value) {
        case "0":
        case "1":
        case "2":
        case "5": return polyProperties;
        case "3":
        case "4": return pointProperties;
        case "6":
        case "7":
        case "8": return lineProperties;
        default: return lineProperties;
    }
}

function getFeatureTypeBySelectValue(value) {
    switch (value) {
        case "0":
        case "1":
        case "2":
        case "5": return poly;
        case "3":
        case "4": return point;
        case "6":
        case "7":
        case "8": return line;
        default: return line;
    }
}

function getDefaultFilterBySelectValue(value) {
    switch (value) {
        case "0": return defEducationFiler;
        case "1": return defHospitalFiler;
        case "2": return defSportFilter;
        case "3": return defGasFilter;
        case "4": return defPharmacyFilter;
        case "5": return defForestFilter;
        case "6": return defRiverFilter;
        case "7": return defRailwayFilter;
        case "8": return defRoadsFilter;
        default: return "";
    }
}

function getLayerStyleBySelectValue(value) {
    switch (value) {
        case "0": return educationStyle;
        case "1": return hospitalStyle;
        case "2": return sportStyle;
        case "3": return null;
        case "4": return null;
        case "5": return forestStyle;
        case "6": return riverStyle;
        case "7": return railwayStyle;
        case "8": return roadStyle;
        default: return "";
    }
}

function getSpatialJoinData(firstLayer, operator, distance, secondLayer, style) {
    $.ajax({
        url: '/Home/GetSpatialJoinData?firstLayer=' + parseInt(firstLayer) + '&spatialOperator=' + parseInt(operator)
            + '&distance=' + distance + '&secondLayer=' + parseInt(secondLayer),
        type: 'GET',
        success: function (response) {
            spatialFilterLayer = L.geoJSON(JSON.parse(response[0]), {
                style: style,
                pointToLayer: createCustomMarker,
                onEachFeature: addPopup
            });

            layerGroup.addLayer(spatialFilterLayer);
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function resetSpatialSearch() {
    $('#SpatialLeftLayer').val("");
    $('#SpatialOperator').val("");
    $('#SpatialRightLayer').val("");
    $('#spatialDistance').val("");

    if (spatialFilterLayer != undefined && spatialFilterLayer != null) {
        layerGroup.removeLayer(spatialFilterLayer);
    }
}

$('#TypeOfVehicle').on('change', function (e) {
    $('#VehicleId').empty().append('<option selected="selected" hidden disabled>Select vehicle</option>')
    var type = this.value;
    var array = allVehicle.filter(function (item) {
        return item.vehicle_Type == type;
    });

    for (const att of array) {
        $('#VehicleId').append($(document.createElement('option')).prop({
            value: att.vehicle_Id,
            text: att.vehicle_Id
        }));
    }
});

function temporalSearch() {
    var type = $('#temporalType').val();
    if (type == "" || type == null) {
        alert('Please choose type of query');
        return;
    }

    var operator = $('#temporalOperator').val();
    if (operator == "" || operator == null) {
        alert('Please choose operator');
        return;
    }

    var value = $('#temporalValue').val();
    if (value == "" || value == null) {
        alert('Please enter value');
        return;
    }

    var startTime = $('#temporalStartTime').val();
    if (startTime == "" || startTime == null) {
        alert('Please enter start time');
        return;
    }

    var endTime = $('#temporalEndTime').val();
    if (endTime == "" || endTime == null) {
        alert('Please enter end time');
        return;
    }

    var style = roadStyle;

    if (temporalLayer != undefined && temporalLayer != null) {
        layerGroup.removeLayer(temporalLayer);
    }

    getTemporalData(type, operator, value, startTime, endTime, style);
}

function getTemporalData(type, operator, value, startTime, endTime, style) {
    $.ajax({
        url: '/Home/GetTemporalData?type=' + parseInt(type) + '&temporalOperator=' + parseInt(operator)
            + '&value=' + parseFloat(value) + '&startTime=' + parseInt(startTime) + '&endTime=' + parseInt(endTime),
        type: 'GET',
        success: function (response) {
            temporalLayer = L.geoJSON(JSON.parse(response[0]), {
                style: style,
                pointToLayer: createCustomMarker,
                onEachFeature: addPopup
            });

            layerGroup.addLayer(temporalLayer);
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function resetTemporalSearch() {
    $('#temporalType').val("");
    $('#temporalOperator').val("");
    $('#temporalValue').val("");
    $('#temporalStartTime').val("");
    $('#temporalEndTime').val("");

    if (temporalLayer != undefined && temporalLayer != null) {
        layerGroup.removeLayer(temporalLayer);
    }
}

function trajectoriesSearch() {
    var type = $('#TypeOfVehicle').val();
    if (type == "" || type == null) {
        alert('Please choose type of vehicle');
        return;
    }

    var vehicle = $('#VehicleId').val();
    if (vehicle == "" || vehicle == null) {
        alert('Please choose vehicle');
        return;
    }

    var startTime = $('#trajectoryStartTime').val();
    if (startTime == "" || startTime == null) {
        alert('Please enter start time');
        return;
    }

    var endTime = $('#trajectoryEndTime').val();
    if (endTime == "" || endTime == null) {
        alert('Please enter end time');
        return;
    }

    if (trajectoryLayer != undefined && trajectoryLayer != null) {
        layerGroup.removeLayer(trajectoryLayer);
    }

    var filter = "vehicle_id IN ('" + vehicle + "') AND timestep_time >= " + startTime + " AND timestep_time <= " + endTime;

    callWFSService(trajectoryLayer, "point", traffic, filter).then((res) => {
        trajectoryLayer = res;
    });
}

function resetTrajectoriesSearch() {
    $('#TypeOfVehicle').val("");
    $('#VehicleId').val("");
    $('#trajectoryStartTime').val("");
    $('#trajectoryEndTime').val("");
    $('#VehicleId').empty().append('<option selected="selected" hidden disabled>Select vehicle</option>')

    if (trajectoryLayer != undefined && trajectoryLayer != null) {
        layerGroup.removeLayer(trajectoryLayer);
    }
}

function trafficJamSearch() {
    var startTime = $('#trafficStartTime').val();
    if (startTime == "" || startTime == null) {
        alert('Please enter time');
        return;
    }

    if (trafficLayer != undefined && trafficLayer != null) {
        layerGroup.removeLayer(trafficLayer);
    }
    var filter = "timestep_time = " + startTime;
    trafficLayer = callWMSService(trafficLayer, "PointStacker", traffic, filter);
}

function resetTrafficJamSearch() {
    $('#trafficStartTime').val("");

    if (trafficLayer != undefined && trafficLayer != null) {
        layerGroup.removeLayer(trafficLayer);
    }
}

legend.onAdd = function () {
    var div = L.DomUtil.create('div', 'legend');
    div.innerHTML = `
            <div class="form-check form-switch" style="margin-top: 5px;">
            <div class="legend-tag education"></div>
            <input class="form-check-input" type="checkbox" onchange="educationChange()" id="educationCheck">
            <label class="form-check-label" for="flexSwitchCheckChecked">Educational inst..</label>
        </div>
        <div class="form-check form-switch">
            <div class="legend-tag hospital"></div>
            <input class="form-check-input" type="checkbox" onchange="hospitalChange()" id="hospitalCheck">
            <label class="form-check-label" for="flexSwitchCheckChecked">Hospitals</label>
        </div>
        <div class="form-check form-switch">
            <div class="legend-tag sport"></div>
            <input class="form-check-input" type="checkbox" onchange="sportChange()" id="sportCheck">
            <label class="form-check-label" for="flexSwitchCheckChecked">Sport institutions</label>
        </div>
        <div class="form-check form-switch">
            <div class="legend-tag gas">
                <img class="marker" src="marker-default.png" />
            </div>
            <input class="form-check-input" type="checkbox" onchange="gasChange()" id="gasCheck">
            <label class="form-check-label" for="flexSwitchCheckChecked">Gas stations</label>
        </div>
        <div class="form-check form-switch">
            <div class="legend-tag pharmacy">
                <img class="marker" src="marker-red.png" />
            </div>
            <input class="form-check-input" type="checkbox" onchange="pharmacyChange()" id="pharmacyCheck">
            <label class="form-check-label" for="flexSwitchCheckChecked">Pharmacies</label>
        </div>
        <div class="form-check form-switch">
            <div class="legend-tag forest">
                <div class="line"></div>
            </div>
            <input class="form-check-input" type="checkbox" onchange="forestChange()" id="forestCheck">
            <label class="form-check-label" for="flexSwitchCheckChecked">Forests</label>
        </div>
        <div class="form-check form-switch">
            <div class="legend-tag river">
                <div class="line"></div>
            </div>
            <input class="form-check-input" type="checkbox" onchange="riverChange()" id="riverCheck">
            <label class="form-check-label" for="flexSwitchCheckChecked">Rivers</label>
        </div>
        <div class="form-check form-switch">
            <div class="legend-tag railway">
                <div class="line"></div>
            </div>
            <input class="form-check-input" type="checkbox" onchange="railwayChange()" id="railwayCheck">
            <label class="form-check-label" for="flexSwitchCheckChecked">Railways</label>
        </div>
        <div class="form-check form-switch">
            <div class="legend-tag roads">
                <div class="line"></div>
            </div>
            <input class="form-check-input" type="checkbox" onchange="roadChange()" id="roadCheck">
            <label class="form-check-label" for="flexSwitchCheckChecked">Roads</label>
        </div>`;
    return div;
}