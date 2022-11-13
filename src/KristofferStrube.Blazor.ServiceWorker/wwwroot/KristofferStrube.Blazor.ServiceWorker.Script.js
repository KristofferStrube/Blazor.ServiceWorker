const params = new Proxy(new URLSearchParams(self.location.search), {
    get: (searchParams, prop) => searchParams.get(prop),
});
let id = params.id;

let proxyDict = {};

self.oninstall = (event) => {
    invokePost("Install", generateGUID(), event);
}
self.onactivate = (event) => {
    invokePost("Activate", generateGUID(), event);
}
self.onfetch = (event) => {
    invokePost("Fetch", generateGUID(), event);
}
self.onpush = (event) => {
    invokePost("Push", generateGUID(), event);
}

self.addEventListener("message", (e) => {
    var message = e.data;
    if (message.type == "GetProxyAttributeAsProxy") {
        var obj = proxyDict[message.id][message.attribute];
        var objectId = generateGUID();
        proxyDict[objectId] = obj;
        resolvePost(message.type, message.id, objectId);
    }
    else if (message.type == "GetProxyAttribute") {
        var obj = proxyDict[message.id][message.method];
        resolvePost(message.type, message.id, obj);
    }
    else if (message.type == "CallProxyMethodAsProxy") {
        var obj = proxyDict[message.id][message.method].call(proxyDict[message.id]);
        var objectId = generateGUID();
        proxyDict[objectId] = obj;
        resolvePost(message.type, message.id, objectId);
    }
    else if (message.type == "CallProxyMethod") {
        var obj = proxyDict[message.id][message.method].call(proxyDict[message.id]);
        resolvePost(message.type, message.id, obj);
    }
});

skipWaiting();

function invokePost(type, eventId, event) {
    clients.matchAll({ type: "window", includeUncontrolled: true }).then(([windowClient]) => {
        if (windowClient != undefined) {
            proxyDict[eventId] = event;
            var message = { type: type, id: id, eventId: eventId };
            windowClient.postMessage(message);
        }
    });
}

function resolvePost(type, id, object) {
    clients.matchAll({ type: "window", includeUncontrolled: true }).then(([windowClient]) => {
        if (windowClient != undefined) {
            var message = { type: `Resolve${type}`, id: id, object: object };
            windowClient.postMessage(message);
        }
    });
}

// https://stackoverflow.com/a/8809472/9905146
function generateGUID() { // Public Domain/MIT
    var d = new Date().getTime();//Timestamp
    var d2 = ((typeof performance !== 'undefined') && performance.now && (performance.now() * 1000)) || 0;//Time in microseconds since page-load or 0 if unsupported
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16;//random number between 0 and 16
        if (d > 0) {//Use timestamp until depleted
            r = (d + r) % 16 | 0;
            d = Math.floor(d / 16);
        } else {//Use microseconds since page-load if supported
            r = (d2 + r) % 16 | 0;
            d2 = Math.floor(d2 / 16);
        }
        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
}