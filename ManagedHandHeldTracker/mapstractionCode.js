(function(){for(var a=null,c="core",b,d=document.getElementsByTagName("script"),h=0;h<d.length;h++){var g=d[h].src.replace(/%20/g,"").match(/^(.*?)mxn\.js(\?\(\[?(.*?)\]?\))?$/);if(null!==g){b=g[1];g[3]&&(h=g[3].split(",["),a=h[0].replace("]",""),h[1]&&(c+=","+h[1]));break}}if(null!==a&&"none"!=a){a=a.replace(/ /g,"").split(",");c=c.replace(/ /g,"").split(",");b='<script type="text/javascript" src="'+b+"mxn.";d=[];for(h=0;h<c.length;h++)for(d.push(b+c[h]+'.js">\x3c/script>'),g=0;g<a.length;g++)d.push(b+
a[g]+"."+c[h]+'.js">\x3c/script>');document.write(d.join(""))}})();
(function(){var a={},c=function(h,g,f,c,k){if(!b(h,g,f))throw"Method "+f+" of object "+g+" is not supported by API "+h+". Are you missing a script tag?";if("undefined"!=typeof a[h][g].deferrable&&!0===a[h][g].deferrable[f])d.deferUntilLoaded.call(c,function(){return a[h][g][f].apply(c,k)});else return a[h][g][f].apply(c,k)},b=function(h,b,f){if("undefined"==typeof a[h])throw"API "+h+" not loaded. Are you missing a script tag?";if("undefined"==typeof a[h][b])throw"Object definition "+b+" in API "+
h+" not loaded. Are you missing a script tag?";return"function"==typeof a[h][b][f]},d=window.mxn={register:function(h,b){a.hasOwnProperty(h)||(a[h]={});d.util.merge(a[h],b)},addProxyMethods:function(h,a,b){for(var c=0;c<a.length;c++){var d=a[c];h.prototype[d]=b?new Function("return this.invoker.go('"+d+"', arguments, { overrideApi: true } );"):new Function("return this.invoker.go('"+d+"', arguments);")}},checkLoad:function(a){if(!1===this.loaded[this.api]){var b=this;this.onload[this.api].push(function(){a.callee.apply(b,
a)});return!0}return!1},deferUntilLoaded:function(a){!1===this.loaded[this.api]?this.onload[this.api].push(a):a.call(this)},addEvents:function(a,b){for(var f=0;f<b.length;f++){var c=b[f];if(c in a)throw"Event or method "+c+" already declared.";a[c]=new d.Event(c,a)}}};d.Event=function(a,b){var f=[];if(!a)throw"Event name must be provided";this.addHandler=function(a,b){f.push({context:b,handler:a})};this.removeHandler=function(a,b){for(var e=0;e<f.length;e++)f[e].handler==a&&f[e].context==b&&f.splice(e,
1)};this.removeAllHandlers=function(){f=[]};this.fire=function(c){c=[a,b,c];for(var d=0;d<f.length;d++)f[d].handler.apply(f[d].context,c)}};d.Invoker=function(a,g,f){var d={overrideApi:!1,context:null,fallback:null};this.go=function(k,e,m){e="undefined"!=typeof e?Array.prototype.slice.apply(e):[];"undefined"==typeof m&&(m=d);var n;n=m.overrideApi?e.shift():f.apply(a);if("string"!=typeof n)throw"API ID not available.";"undefined"!=typeof m.context&&null!==m.context&&e.push(m.context);return"function"!=
typeof m.fallback||b(n,g,k)?c(n,g,k,a,e):m.fallback.apply(a,e)}};d.util={merge:function(a,b){for(var f in b)b.hasOwnProperty(f)&&(a.hasOwnProperty(f)&&"object"===typeof a[f]&&"object"===typeof b[f]?d.util.merge(a[f],b[f]):a[f]=b[f])},$m:function(){for(var a=[],b=0;b<arguments.length;b++){var f=arguments[b];"string"==typeof f&&(f=document.getElementById(f));if(1==arguments.length)return f;a.push(f)}return a},loadScript:function(a,b){var f=document.createElement("script");f.type="text/javascript";f.src=
a;if(b)if(f.addEventListener)f.addEventListener("load",b,!0);else if(f.attachEvent){var c=!1;f.attachEvent("onreadystatechange",function(){c||"complete"!==document.readyState||(c=!0,b())})}document.getElementsByTagName("head")[0].appendChild(f)},convertLatLonXY_Yahoo:function(a,b){var c=1<<26-b,d=c/360,k=c/(2*Math.PI),c=new YCoordPoint(c/2,c/2),e=new YCoordPoint;e.x=Math.floor(c.x+a.lon*d);d=Math.sin(a.lat*Math.PI/180);e.y=Math.floor(c.y+.5*Math.log((1+d)/(1-d))*-k);return e},loadStyle:function(a){var b=
document.createElement("link");b.type="text/css";b.rel="stylesheet";b.href=a;document.getElementsByTagName("head")[0].appendChild(b)},getStyle:function(a,b){var c;a.currentStyle?c=a.currentStyle[b]:window.getComputedStyle&&(c=window.getComputedStyle(a,"").getPropertyValue(b));return c},lonToMetres:function(a,b){return 111200*a*Math.cos(Math.PI/180*b)},metresToLon:function(a,b){return a/(111200*Math.cos(Math.PI/180*b))},KMToMiles:function(a){return a/1.609344},milesToKM:function(a){return 1.609344*
a},getDegreesFromGoogleZoomLevel:function(a,b){return 360*a/Math.pow(2,b+8)},getGoogleZoomLevelFromDegrees:function(a,b){return d.util.logN(360*a/b,2)-8},logN:function(a,b){return Math.log(a)/Math.log(b)},getAvailableProviders:function(){var b=[],c;for(c in a)a.hasOwnProperty(c)&&b.push(c);return b},stringFormat:function(a){var b=Array.prototype.slice.apply(arguments);b.shift();return a.replace(/\{\d+\}/g,function(a){a=a.slice(1,-1);return b[a]})},traverse:function(a){var b=Array.prototype.slice.apply(arguments);
b.shift();for(var c=a;"undefined"!=typeof c&&null!==c&&0<b.length;)c=b.shift()(c)}};d.util.Color=function(){3==arguments.length?(this.red=arguments[0],this.green=arguments[1],this.blue=arguments[2]):1==arguments.length&&this.setHexColor(arguments[0])};d.util.Color.prototype.reHex=/^#?([0-9a-fA-F]{3}|[0-9a-fA-F]{6})$/;d.util.Color.prototype.setHexColor=function(a){if(a=a.match(this.reHex))a=a[1];else throw"Invalid HEX color format, expected #000, 000, #000000 or 000000";3==a.length&&(a=a.replace(/\w/g,
function(a){return a.concat(a)}));this.red=parseInt(a.substr(0,2),16);this.green=parseInt(a.substr(2,2),16);this.blue=parseInt(a.substr(4,2),16)};d.util.Color.prototype.getHexColor=function(){var a=(this.blue|this.green<<8|this.red<<16).toString(16).toUpperCase();6>a.length&&(a="0"+a);return"#"+a}})();(function(){var a=mxn.util.$m,c=function(){this.invoker.go("init",[this.currentElement,this.api]);this.applyOptions()},b=mxn.Mapstraction=function(e,b,n){b||(b=mxn.util.getAvailableProviders()[0]);this.api=b;this.maps={};this.currentElement=a(e);this.eventListeners=[];this.tileLayers=[];this.markers=[];this.polylines=[];this.images=[];this.controls=[];this.loaded={};this.onload={};this.onload[b]=[];this.element=e;this.options={enableScrollWheelZoom:!1,enableDragging:!0};this.addControlsArgs={};this.invoker=
new mxn.Invoker(this,"Mapstraction",function(){return this.api});mxn.addEvents(this,"load click endPan changeZoom markerAdded markerRemoved polylineAdded polylineRemoved".split(" "));c.apply(this)};b.ROAD=1;b.SATELLITE=2;b.HYBRID=3;b.PHYSICAL=4;mxn.addProxyMethods(b,"addLargeControls addMapTypeControls addOverlay addSmallControls applyOptions getBounds getCenter getMapType getPixelRatio getZoom getZoomLevelForBoundingBox mousePosition resizeTo setBounds setCenter setCenterAndZoom setMapType setZoom toggleTileLayer".split(" "));
b.prototype.setOptions=function(e){mxn.util.merge(this.options,e);this.applyOptions()};b.prototype.setOption=function(e,a){this.options[e]=a;this.applyOptions()};b.prototype.enableScrollWheelZoom=function(){this.setOption("enableScrollWheelZoom",!0)};b.prototype.dragging=function(e){this.setOption("enableDragging",e)};b.prototype.swap=function(e,b){if(this.api!==b){var n=this.getCenter(),d=this.getZoom();this.currentElement.style.visibility="hidden";this.currentElement.style.display="none";this.currentElement=
a(e);this.currentElement.style.visibility="visible";this.currentElement.style.display="block";this.api=b;this.onload[b]=[];if(void 0===this.maps[this.api]){c.apply(this);for(var f=0;f<this.markers.length;f++)this.addMarker(this.markers[f],!0);for(f=0;f<this.polylines.length;f++)this.addPolyline(this.polylines[f],!0)}this.setCenterAndZoom(n,d);this.addControls(this.addControlsArgs)}};b.prototype.isLoaded=function(e){null===e&&(e=this.api);return this.loaded[e]};b.prototype.setDebug=function(e){null!==
e&&(this.debug=e);return this.debug};b.prototype.setDefer=function(e){this.loaded[this.api]=!e};b.prototype.runDeferred=function(){for(;0<this.onload[this.api].length;)this.onload[this.api].shift().apply(this)};b.prototype.clickHandler=function(e,a,b){this.callEventListeners("click",{location:new g(e,a)})};b.prototype.moveendHandler=function(e){this.callEventListeners("moveend",{})};b.prototype.addEventListener=function(){var e={};e.event_type=arguments[0];e.callback_function=arguments[1];3==arguments.length?
(e.back_compat_mode=!1,e.callback_object=arguments[2]):(e.back_compat_mode=!0,e.callback_object=null);this.eventListeners.push(e)};b.prototype.callEventListeners=function(e,a){a.source=this;for(var b=0;b<this.eventListeners.length;b++){var c=this.eventListeners[b];c.event_type==e&&(c.back_compat_mode?"click"==c.event_type?c.callback_function(a.location):c.callback_function():c.callback_function.call(c.callback_object||this,a))}};b.prototype.addControls=function(e){this.addControlsArgs=e;this.invoker.go("addControls",
arguments)};b.prototype.addMarker=function(e,a){e.mapstraction=this;e.api=this.api;e.location.api=this.api;e.map=this.maps[this.api];var b=this.invoker.go("addMarker",arguments);e.setChild(b);a||this.markers.push(e);this.markerAdded.fire({marker:e})};b.prototype.addMarkerWithData=function(e,a){e.addData(a);this.addMarker(e)};b.prototype.addPolylineWithData=function(e,a){e.addData(a);this.addPolyline(e)};b.prototype.removeMarker=function(e){for(var a,b=0;b<this.markers.length;b++)if(a=this.markers[b],
e==a){this.invoker.go("removeMarker",arguments);e.onmap=!1;this.markers.splice(b,1);this.markerRemoved.fire({marker:e});break}};b.prototype.removeAllMarkers=function(){for(var e;0<this.markers.length;)e=this.markers.pop(),this.invoker.go("removeMarker",[e])};b.prototype.declutterMarkers=function(e){if(!1===this.loaded[this.api]){var a=this;this.onload[this.api].push(function(){a.declutterMarkers(e)})}else{var b=this.maps[this.api];switch(this.api){case "multimap":b.declutterGroup(e.groupName);break;
case "  dummy":break;default:this.debug&&alert(this.api+" not supported by Mapstraction.declutterMarkers")}}};b.prototype.addPolyline=function(e,a){e.api=this.api;e.map=this.maps[this.api];var b=this.invoker.go("addPolyline",arguments);e.setChild(b);a||this.polylines.push(e);this.polylineAdded.fire({polyline:e})};var d=function(e){this.invoker.go("removePolyline",arguments);e.onmap=!1;this.polylineRemoved.fire({polyline:e})};b.prototype.removePolyline=function(e){for(var a,b=0;b<this.polylines.length;b++)if(a=
this.polylines[b],e==a){this.polylines.splice(b,1);d.call(this,e);break}};b.prototype.removeAllPolylines=function(){for(var e;0<this.polylines.length;)e=this.polylines.pop(),d.call(this,e)};var h=function(e,a,b){var c=[];if(e)for(e=0;e<this.markers.length;e++){var d=this.markers[e];b&&!b(d)||c.push(d.location)}if(a)for(e=0;e<this.polylines.length;e++)if(a=this.polylines[e],!b||b(a))for(d=0;d<a.points.length;d++)c.push(a.points[d]);return c};b.prototype.autoCenterAndZoom=function(){var e=h.call(this,
!0,!0);this.centerAndZoomOnPoints(e)};b.prototype.centerAndZoomOnPoints=function(e){for(var a=new f(90,180,-90,-180),b=0,c=e.length;b<c;b++)a.extend(e[b]);this.setBounds(a)};b.prototype.visibleCenterAndZoom=function(){var e=h.call(this,!0,!0,function(e){return e.getAttribute("visible")});this.centerAndZoomOnPoints(e)};b.prototype.polylineCenterAndZoom=function(e){e=e||0;var a=h.call(this,!1,!0);if(0<e){for(var b=[],c=0;c<a.length;c++){var d=a[c],f=d.latConv(),l=d.lonConv(),f=e/f,k=e/l,l=new g(d.lat+
f,d.lon+k),d=new g(d.lat-f,d.lon-k);b.push(l,d)}a=a.concat(b)}this.centerAndZoomOnPoints(a)};b.prototype.addImageOverlay=function(e,a,b,c,d,f,h){var g=document.createElement("img");g.style.display="block";g.setAttribute("id",e);g.setAttribute("src",a);g.style.position="absolute";g.style.zIndex=1;g.setAttribute("west",c);g.setAttribute("south",d);g.setAttribute("east",f);g.setAttribute("north",h);this.invoker.go("addImageOverlay",arguments,{context:{imgElm:g}})};b.prototype.setImageOpacity=function(e,
a){0>a&&(a=0);100<=a&&(a=100);var b=a/100,c=document.getElementById(e);"string"==typeof c.style.filter&&(c.style.filter="alpha(opacity:"+a+")");"string"==typeof c.style.KHTMLOpacity&&(c.style.KHTMLOpacity=b);"string"==typeof c.style.MozOpacity&&(c.style.MozOpacity=b);"string"==typeof c.style.opacity&&(c.style.opacity=b)};b.prototype.setImagePosition=function(e){var a=document.getElementById(e),b={latLng:{top:a.getAttribute("north"),left:a.getAttribute("west"),bottom:a.getAttribute("south"),right:a.getAttribute("east")},
pixels:{top:0,right:0,bottom:0,left:0}};this.invoker.go("setImagePosition",arguments,{context:b});a.style.top=b.pixels.top.toString()+"px";a.style.left=b.pixels.left.toString()+"px";a.style.width=(b.pixels.right-b.pixels.left).toString()+"px";a.style.height=(b.pixels.bottom-b.pixels.top).toString()+"px"};b.prototype.addJSON=function(e){e="string"==typeof e?eval("("+e+")"):e;e=e.features;var a="",b,c,d=[];"FeatureCollection"==e.type&&this.addJSON(e.features);for(var f=0;f<e.length;f++)switch(b=e[f],
b.geometry.type){case "Point":a="<strong>"+b.title+"</strong><p>"+b.description+"</p>";c=new l(new g(b.geometry.coordinates[1],b.geometry.coordinates[0]));d.push(c);this.addMarkerWithData(c,{infoBubble:a,label:b.title,date:'new Date("'+b.date+'")',iconShadow:b.icon_shadow,marker:b.id,iconShadowSize:b.icon_shadow_size,icon:b.icon,iconSize:b.icon_size,category:b.source_id,draggable:!1,hover:!1});break;case "Polygon":a=new k([]),mapstraction.addPolylineWithData(a,{fillColor:b.poly_color,date:'new Date("'+
b.date+'")',category:b.source_id,width:b.line_width,opacity:b.line_opacity,color:b.line_color,polygon:!0}),d.push(a)}return d};b.prototype.addTileLayer=function(e,a,b,c,d,f){if(e)return this.invoker.go("addTileLayer",[e,a||.6,b||"Mapstraction",c||1,d||18,f||!1])};b.prototype.addFilter=function(e,a,b){this.filters||(this.filters=[]);this.filters.push([e,a,b])};b.prototype.removeFilter=function(e,a,b){if(this.filters)for(var c=0;c<this.filters.length;c++)this.filters[c][0]!=e||a&&(this.filters[c][1]!=
a||this.filters[c][2]!=b)||(this.filters.splice(c,1),c--)};b.prototype.toggleFilter=function(e,a,b){this.filters||(this.filters=[]);for(var c=!1,d=0;d<this.filters.length;d++)this.filters[d][0]==e&&this.filters[d][1]==a&&this.filters[d][2]==b&&(this.filters.splice(d,1),d--,c=!0);c||this.addFilter(e,a,b)};b.prototype.removeAllFilters=function(){this.filters=[]};b.prototype.doFilter=function(e,a){var b=this.maps[this.api],c=0,d;if(this.filters)switch(this.api){case "multimap":var f=[];for(d=0;d<this.filters.length;d++)f.push(new MMSearchFilter(this.filters[d][0],
this.filters[d][1],this.filters[d][2]));b.setMarkerFilters(f);b.redrawMap();break;case "  dummy":break;default:for(f=0;f<this.markers.length;f++){b=!0;for(d=0;d<this.filters.length;d++)this.applyFilter(this.markers[f],this.filters[d])||(b=!1);b?(c++,e?e(this.markers[f]):this.markers[f].show()):a?a(this.markers[f]):this.markers[f].hide();this.markers[f].setAttribute("visible",b)}}return c};b.prototype.applyFilter=function(e,a){var b=!0;switch(a[1]){case "ge":e.getAttribute(a[0])<a[2]&&(b=!1);break;
case "le":e.getAttribute(a[0])>a[2]&&(b=!1);break;case "eq":e.getAttribute(a[0])==a[2]&&(b=!1)}return b};b.prototype.getAttributeExtremes=function(a){for(var b,c,d=0;d<this.markers.length;d++){if(!b||b>this.markers[d].getAttribute(a))b=this.markers[d].getAttribute(a);if(!c||c<this.markers[d].getAttribute(a))c=this.markers[d].getAttribute(a)}for(;d<this.polylines.length;d++){if(!b||b>this.polylines[0].getAttribute(a))b=this.polylines[0].getAttribute(a);if(!c||c<this.polylines[0].getAttribute(a))c=
this.polylines[0].getAttribute(a)}return[b,c]};b.prototype.getMap=function(){return this.maps[this.api]};var g=mxn.LatLonPoint=function(a,b){this.lat=Number(a);this.lng=this.lon=Number(b);this.invoker=new mxn.Invoker(this,"LatLonPoint")};mxn.addProxyMethods(g,["fromProprietary","toProprietary"],!0);g.prototype.toString=function(){return this.lat+", "+this.lon};g.prototype.distance=function(a){var b=Math.PI/180,c=(this.lat-a.lat)*b,d=(this.lon-a.lon)*b;a=Math.sin(c/2)*Math.sin(c/2)+Math.cos(this.lat*
b)*Math.cos(a.lat*b)*Math.sin(d/2)*Math.sin(d/2);return 12742*Math.atan2(Math.sqrt(a),Math.sqrt(1-a))};g.prototype.equals=function(a){return this.lat==a.lat&&this.lon==a.lon};g.prototype.latConv=function(){return 10*this.distance(new g(this.lat+.1,this.lon))};g.prototype.lonConv=function(){return 10*this.distance(new g(this.lat,this.lon+.1))};var f=mxn.BoundingBox=function(a,b,c,d){this.sw=new g(a,b);this.ne=new g(c,d)};f.prototype.getSouthWest=function(){return this.sw};f.prototype.getNorthEast=
function(){return this.ne};f.prototype.isEmpty=function(){return this.ne==this.sw};f.prototype.contains=function(a){return a.lat>=this.sw.lat&&a.lat<=this.ne.lat&&a.lon>=this.sw.lon&&a.lon<=this.ne.lon};f.prototype.toSpan=function(){return new g(Math.abs(this.sw.lat-this.ne.lat),Math.abs(this.sw.lon-this.ne.lon))};f.prototype.extend=function(a){this.sw.lat>a.lat&&(this.sw.lat=a.lat);this.sw.lon>a.lon&&(this.sw.lon=a.lon);this.ne.lat<a.lat&&(this.ne.lat=a.lat);this.ne.lon<a.lon&&(this.ne.lon=a.lon)};
var l=mxn.Marker=function(a){this.api=null;this.location=a;this.proprietary_marker=this.onmap=!1;this.attributes=[];this.invoker=new mxn.Invoker(this,"Marker",function(){return this.api});mxn.addEvents(this,["openInfoBubble","closeInfoBubble","click"])};mxn.addProxyMethods(l,"fromProprietary hide openBubble closeBubble show toProprietary update".split(" "));l.prototype.setChild=function(a){this.proprietary_marker=a;a.mapstraction_marker=this;this.onmap=!0};l.prototype.setLabel=function(a){this.labelText=
a};l.prototype.addData=function(a){for(var b in a)if(a.hasOwnProperty(b))switch(b){case "label":this.setLabel(a.label);break;case "infoBubble":this.setInfoBubble(a.infoBubble);break;case "icon":a.iconSize&&a.iconAnchor?this.setIcon(a.icon,a.iconSize,a.iconAnchor):a.iconSize?this.setIcon(a.icon,a.iconSize):this.setIcon(a.icon);break;case "iconShadow":a.iconShadowSize?this.setShadowIcon(a.iconShadow,[a.iconShadowSize[0],a.iconShadowSize[1]]):this.setIcon(a.iconShadow);break;case "infoDiv":this.setInfoDiv(a.infoDiv[0],
a.infoDiv[1]);break;case "draggable":this.setDraggable(a.draggable);break;case "hover":this.setHover(a.hover);this.setHoverIcon(a.hoverIcon);break;case "hoverIcon":this.setHoverIcon(a.hoverIcon);break;case "openBubble":this.openBubble();break;case "closeBubble":this.closeBubble();break;case "groupName":this.setGroupName(a.groupName);break;default:this.setAttribute(b,a[b])}};l.prototype.setInfoBubble=function(a){this.infoBubble=a};l.prototype.setInfoDiv=function(a,b){this.infoDiv=a;this.div=b};l.prototype.setIcon=
function(a,b,c){this.iconUrl=a;b&&(this.iconSize=b);c&&(this.iconAnchor=c)};l.prototype.setIconSize=function(a){a&&(this.iconSize=a)};l.prototype.setIconAnchor=function(a){a&&(this.iconAnchor=a)};l.prototype.setShadowIcon=function(a,b){this.iconShadowUrl=a;b&&(this.iconShadowSize=b)};l.prototype.setHoverIcon=function(a){this.hoverIconUrl=a};l.prototype.setDraggable=function(a){this.draggable=a};l.prototype.setHover=function(a){this.hover=a};l.prototype.setGroupName=function(a){this.groupName=a};l.prototype.setAttribute=
function(a,b){this.attributes[a]=b};l.prototype.getAttribute=function(a){return this.attributes[a]};var k=mxn.Polyline=function(a){this.api=null;this.points=a;this.attributes=[];this.proprietary_polyline=this.onmap=!1;this.pllID="mspll-"+(new Date).getTime()+"-"+Math.floor(Math.random()*Math.pow(2,16));this.invoker=new mxn.Invoker(this,"Polyline",function(){return this.api})};mxn.addProxyMethods(k,["fromProprietary","hide","show","toProprietary","update"]);k.prototype.addData=function(a){for(var b in a)if(a.hasOwnProperty(b))switch(b){case "color":this.setColor(a.color);
break;case "width":this.setWidth(a.width);break;case "opacity":this.setOpacity(a.opacity);break;case "closed":this.setClosed(a.closed);break;case "fillColor":this.setFillColor(a.fillColor);break;default:this.setAttribute(b,a[b])}};k.prototype.setChild=function(a){this.proprietary_polyline=a;this.onmap=!0};k.prototype.setColor=function(a){this.color=7==a.length&&"#"==a[0]?a.toUpperCase():a};k.prototype.setWidth=function(a){this.width=a};k.prototype.setOpacity=function(a){this.opacity=a};k.prototype.setClosed=
function(a){this.closed=a};k.prototype.setFillColor=function(a){this.fillColor=a};k.prototype.setAttribute=function(a,b){this.attributes[a]=b};k.prototype.getAttribute=function(a){return this.attributes[a]};k.prototype.simplify=function(a){var b=[];b[0]=this.points[0];for(var c=0,d=1;d<this.points.length-1;d++)this.points[d].distance(this.points[c])>=a&&(b[b.length]=this.points[d],c=d);b[b.length]=this.points[this.points.length-1];this.points=b};(mxn.Radius=function(a,b){this.center=a;var c=a.latConv(),
d=a.lonConv(),f=Math.PI/180;this.calcs=[];for(var g=0;360>g;g+=b)this.calcs.push([Math.cos(g*f)/c,Math.sin(g*f)/d])}).prototype.getPolyline=function(a,b){for(var c=[],d=0;d<this.calcs.length;d++){var f=new g(this.center.lat+a*this.calcs[d][0],this.center.lon+a*this.calcs[d][1]);c.push(f)}c.push(c[0]);c=new k(c);c.setColor(b);return c}})();mxn.register("openlayers",{Mapstraction:{init:function(a,c){var b=this,d=new OpenLayers.Map(a.id,{maxExtent:new OpenLayers.Bounds(-2.003750834E7,-2.003750834E7,2.003750834E7,2.003750834E7),maxResolution:156543,numZoomLevels:18,units:"m",projection:"EPSG:900913"});this.layers={};this.layers.osm=new OpenLayers.Layer.TMS("OpenStreetMap",["http://a.tile.openstreetmap.org/","http://b.tile.openstreetmap.org/","http://c.tile.openstreetmap.org/"],{type:"png",getURL:function(a){var b=this.map.getResolution(),
c=Math.round((a.left-this.maxExtent.left)/(b*this.tileSize.w));a=Math.round((this.maxExtent.top-a.top)/(b*this.tileSize.h));var b=this.map.getZoom(),d=Math.pow(2,b);if(0>a||a>=d)return null;c=b+"/"+(c%d+d)%d+"/"+a+"."+this.type;a=this.url;a instanceof Array&&(a=this.selectUrl(c,a));return a+c},displayOutsideMaxExtent:!0});d.events.register("click",d,function(a){a=d.getLonLatFromViewPortPx(a.xy);var g=new mxn.LatLonPoint;g.fromProprietary(c,a);b.click.fire({location:g})});d.events.register("zoomend",
d,function(a){b.changeZoom.fire()});d.events.register("moveend",d,function(a){b.moveendHandler(b);b.endPan.fire()});var h=function(a){b.load.fire();this.events.unregister("loadend",this,h)},g;for(g in this.layers)this.layers.hasOwnProperty(g)&&!0===this.layers[g].visibility&&this.layers[g].events.register("loadend",this.layers[g],h);d.addLayer(this.layers.osm);this.tileLayers.push(["http://a.tile.openstreetmap.org/",this.layers.osm,!0]);this.maps[c]=d;this.loaded[c]=!0},applyOptions:function(){var a=
this.maps[this.api].getControlsByClass("OpenLayers.Control.Navigation");0<a.length&&(a=a[0],this.options.enableScrollWheelZoom?a.enableZoomWheel():a.disableZoomWheel(),this.options.enableDragging?a.activate():a.deactivate())},resizeTo:function(a,c){this.currentElement.style.width=a;this.currentElement.style.height=c;this.maps[this.api].updateSize()},addControls:function(a){for(var c=this.maps[this.api],b=c.controls.length;1<b;b--)c.controls[b-1].deactivate(),c.removeControl(c.controls[b-1]);"large"==
a.zoom?c.addControl(new OpenLayers.Control.PanZoomBar):("small"==a.zoom&&c.addControl(new OpenLayers.Control.ZoomPanel),a.pan&&c.addControl(new OpenLayers.Control.PanPanel));a.overview&&c.addControl(new OpenLayers.Control.OverviewMap);a.map_type&&c.addControl(new OpenLayers.Control.LayerSwitcher);a.scale&&c.addControl(new OpenLayers.Control.ScaleLine)},addSmallControls:function(){var a=this.maps[this.api];this.addControlsArgs.pan=!1;this.addControlsArgs.scale=!1;this.addControlsArgs.zoom="small";
a.addControl(new OpenLayers.Control.ZoomBox);a.addControl(new OpenLayers.Control.LayerSwitcher({ascending:!1}))},addLargeControls:function(){this.maps[this.api].addControl(new OpenLayers.Control.PanZoomBar);this.addControlsArgs.pan=!0;this.addControlsArgs.zoom="large"},addMapTypeControls:function(){this.maps[this.api].addControl(new OpenLayers.Control.LayerSwitcher({ascending:!1}));this.addControlsArgs.map_type=!0},setCenterAndZoom:function(a,c){var b=this.maps[this.api];a.toProprietary(this.api);
b.setCenter(a.toProprietary(this.api),c)},addMarker:function(a,c){var b=this.maps[this.api],d=a.toProprietary(this.api);this.layers.markers||(this.layers.markers=new OpenLayers.Layer.Markers("markers"),b.addLayer(this.layers.markers));this.layers.markers.addMarker(d);return d},removeMarker:function(a){a=a.proprietary_marker;this.layers.markers.removeMarker(a);a.destroy()},declutterMarkers:function(a){throw"Not supported";},addPolyline:function(a,c){var b=this.maps[this.api],d=a.toProprietary(this.api);
this.layers.polylines||(this.layers.polylines=new OpenLayers.Layer.Vector("polylines"),b.addLayer(this.layers.polylines));this.layers.polylines.addFeatures([d]);return d},removePolyline:function(a){this.layers.polylines.removeFeatures([a.proprietary_polyline])},removeAllPolylines:function(){for(var a=[],c=0,b=this.polylines.length;c<b;c++)a.push(this.polylines[c].proprietary_polyline);this.layers.polylines&&this.layers.polylines.removeFeatures(a)},getCenter:function(){var a=this.maps[this.api].getCenter(),
c=new mxn.LatLonPoint;c.fromProprietary(this.api,a);return c},setCenter:function(a,c){var b=this.maps[this.api],d=a.toProprietary(this.api);b.setCenter(d)},setZoom:function(a){this.maps[this.api].zoomTo(a)},getZoom:function(){return this.maps[this.api].zoom},getZoomLevelForBoundingBox:function(a){var c=this.maps[this.api],b=a.getSouthWest();a=a.getNorthEast();b.lon>a.lon&&(b.lon-=360);var d=new OpenLayers.Bounds;d.extend((new mxn.LatLonPoint(b.lat,b.lon)).toProprietary(this.api));d.extend((new mxn.LatLonPoint(a.lat,
a.lon)).toProprietary(this.api));return c.getZoomForExtent(d)},setMapType:function(a){throw"Not implemented (setMapType)";},getMapType:function(){return mxn.Mapstraction.ROAD},getBounds:function(){var a=this.maps[this.api].calculateBounds(),c=new OpenLayers.LonLat(a.left,a.bottom),b=new mxn.LatLonPoint(0,0);b.fromProprietary(this.api,c);a=new OpenLayers.LonLat(a.right,a.top);c=new mxn.LatLonPoint(0,0);c.fromProprietary(this.api,a);return new mxn.BoundingBox(b.lat,b.lon,c.lat,c.lon)},setBounds:function(a){var c=
this.maps[this.api],b=a.getSouthWest();a=a.getNorthEast();b.lon>a.lon&&(b.lon-=360);var d=new OpenLayers.Bounds;d.extend((new mxn.LatLonPoint(b.lat,b.lon)).toProprietary(this.api));d.extend((new mxn.LatLonPoint(a.lat,a.lon)).toProprietary(this.api));c.zoomToExtent(d)},addImageOverlay:function(a,c,b,d,h,g,f,l){var k=this.maps[this.api],e=new OpenLayers.Bounds;e.extend((new mxn.LatLonPoint(h,d)).toProprietary(this.api));e.extend((new mxn.LatLonPoint(f,g)).toProprietary(this.api));a=new OpenLayers.Layer.Image(a,
c,e,new OpenLayers.Size(l.imgElm.width,l.imgElm.height),{isBaseLayer:!1,alwaysInRange:!0});k.addLayer(a);this.setImageOpacity(a.div.id,b)},setImagePosition:function(a,c){},addOverlay:function(a,c){var b=this.maps[this.api],d=new OpenLayers.Layer.GML("kml",a,{format:OpenLayers.Format.KML,formatOptions:new OpenLayers.Format.KML({extractStyles:!0,extractAttributes:!0}),projection:new OpenLayers.Projection("EPSG:4326")});c&&d.events.register("loadend",d,function(){dataExtent=this.getDataExtent();b.zoomToExtent(dataExtent)});
b.addLayer(d)},addTileLayer:function(a,c,b,d,h,g){d=this.maps[this.api];h=a.replace(/\{Z\}/g,"${z}");h=h.replace(/\{X\}/g,"${x}");h=h.replace(/\{Y\}/g,"${y}");c=new OpenLayers.Layer.XYZ(b,h,{sphericalMercator:!1,opacity:c});g||c.addOptions({displayInLayerSwitcher:!1,isBaseLayer:!1});d.addLayer(c);this.tileLayers.push([a,c,!1])},toggleTileLayer:function(a){for(var c=this.tileLayers.length-1;0<=c;c--)this.tileLayers[c][0]==a&&(this.tileLayers[c][2]=!this.tileLayers[c][2],this.tileLayers[c][1].setVisibility(this.tileLayers[c][2]))},
getPixelRatio:function(){},mousePosition:function(a){var c=this.maps[this.api],b=document.getElementById(a);null!==b&&c.events.register("mousemove",c,function(a){a=c.getLonLatFromViewPortPx(a.xy);var h=new mxn.LatLonPoint;h.fromProprietary("openlayers",a);a=h.lat.toFixed(4)+" / "+h.lon.toFixed(4);b.innerHTML=a});b.innerHTML="0.0000 / 0.0000"}},LatLonPoint:{toProprietary:function(){var a=2.003750834E7*this.lon/180,c=Math.log(Math.tan((90+this.lat)*Math.PI/360))/(Math.PI/180);return new OpenLayers.LonLat(a,
2.003750834E7*c/180)},fromProprietary:function(a){var c=a.lon/2.003750834E7*180;a=a.lat/2.003750834E7*180;a=180/Math.PI*(2*Math.atan(Math.exp(a*Math.PI/180))-Math.PI/2);this.lon=c;this.lat=a}},Marker:{toProprietary:function(){var a,c,b;a=this.iconSize?new OpenLayers.Size(this.iconSize[0],this.iconSize[1]):new OpenLayers.Size(21,25);c=this.iconAnchor?new OpenLayers.Pixel(-this.iconAnchor[0],-this.iconAnchor[1]):new OpenLayers.Pixel(-(a.w/2),-a.h);this.icon=this.iconUrl?new OpenLayers.Icon(this.iconUrl,
a,c):new OpenLayers.Icon("http://openlayers.org/dev/img/marker-gold.png",a,c);var d=new OpenLayers.Marker(this.location.toProprietary("openlayers"),this.icon);if(this.infoBubble){b=new OpenLayers.Popup.FramedCloud(null,this.location.toProprietary("openlayers"),new OpenLayers.Size(100,100),this.infoBubble,this.icon,!0);var h=this.map;if(this.hover)d.events.register("mouseover",d,function(a){h.addPopup(b);b.show()}),d.events.register("mouseout",d,function(a){b.hide();h.removePopup(b)});else{var g=!1;
d.events.register("mousedown",d,function(a){g?(b.hide(),h.removePopup(b),g=!1):(h.addPopup(b),b.show(),g=!0)})}this.popup=b}d.events.register("click",d,function(a){d.mapstraction_marker.click.fire()});this.hoverIconUrl&&(icon=this.iconUrl||"http://openlayers.org/dev/img/marker-gold.png",hovericon=this.hoverIconUrl,d.events.register("mouseover",d,function(a){d.setUrl(hovericon)}),d.events.register("mouseout",d,function(a){d.setUrl(icon)}));return d},openBubble:function(){this.infoBubble&&(this.popup=
new OpenLayers.Popup.FramedCloud(null,this.location.toProprietary("openlayers"),new OpenLayers.Size(100,100),this.infoBubble,this.icon,!0));this.popup&&this.map.addPopup(this.popup,!0)},closeBubble:function(){this.popup&&(this.popup.hide(),this.map.removePopup(this.popup))},hide:function(){this.proprietary_marker.display(!1)},show:function(){this.proprietary_marker.display(!0)},update:function(){}},Polyline:{toProprietary:function(){for(var a=[],c={strokeColor:this.color||"#000000",strokeOpacity:this.opacity||
1,strokeWidth:this.width||1,fillColor:this.fillColor||"#000000",fillOpacity:this.getAttribute("fillOpacity")||.2},b=0,d=this.points.length;b<d;b++)olpoint=this.points[b].toProprietary("openlayers"),a.push(new OpenLayers.Geometry.Point(olpoint.lon,olpoint.lat));a=this.closed?new OpenLayers.Geometry.LinearRing(a):new OpenLayers.Geometry.LineString(a);return new OpenLayers.Feature.Vector(a,null,c)},show:function(){throw"Not implemented";},hide:function(){throw"Not implemented";}}});