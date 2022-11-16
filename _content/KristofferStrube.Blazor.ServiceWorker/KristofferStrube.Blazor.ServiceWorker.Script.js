const params = new Proxy(new URLSearchParams(self.location.search), {
    get: (searchParams, prop) => searchParams.get(prop),
});
let id = params.id;
let root = self.location.origin + params.root + "/";

let proxyDict = {};
proxyDict[id] = self;

let resolvers = {};

self.oninstall = (event) => {
    invokePost("Install", generateGUID(), event);
}
self.onactivate = (event) => {
    invokePost("Activate", generateGUID(), event);
}
self.onfetch = (event) => {
    var skippedURLs = [
        root,
        root + "_vs/browserLink",
        root + "css/bootstrap/bootstrap.min.css",
        root + "css/app.css",
        root + "KristofferStrube.Blazor.ServiceWorker.WasmExample.styles.css",
        root + "css/open-iconic/font/css/open-iconic-bootstrap.min.css",
        root + "favicon.png",
        root + "_content/KristofferStrube.Blazor.ServiceWorker/KristofferStrube.Blazor.ServiceWorker.js",
    ]
    if (!(skippedURLs.includes(event.request.url) ||
        event.request.url.startsWith("http://") ||
        event.request.url.startsWith("wss://") ||
        event.request.url.startsWith("ws://") ||
        event.request.url.startsWith(root + "?") ||
        event.request.url.includes("/_framework/"))) {
        event.respondWith(handleFetch(event));
    }
}

async function handleFetch(event) {
    var id = generateGUID();
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
    });
    invokePost("Fetch", id, event);
    return await promise;
}

self.onpush = (event) => {
    invokePost("Push", generateGUID(), event);
}

self.addEventListener("message", (e) => {
    var message = e.data;
    if (message.type == "GetProxyAttributeAsProxy") {
        var obj = proxyDict[message.objectId][message.attribute];
        var objectId = generateGUID();
        proxyDict[objectId] = obj;
        resolvePost(message.type, message.id, objectId);
    }
    else if (message.type == "GetProxyAsyncAttributeAsProxy") {
        proxyDict[message.objectId][message.attribute].then(obj => {
            var objectId = generateGUID();
            proxyDict[objectId] = obj;
            resolvePost(message.type, message.id, objectId);
        });
    }
    else if (message.type == "GetProxyAttribute") {
        var obj = proxyDict[message.objectId][message.attribute];
        resolvePost(message.type, message.id, obj);
    }
    else if (message.type.startsWith("Call")) {
        if (proxyDict[message.objectId] == undefined && resolvers[message.id] == undefined) {
            return;
        }
        for (let i = 0; i < message.args.length; i++) {
            if (message.args[i] != null && message.args[i].length == 36 && message.args[i].charAt(8) == '-') {
                message.args[i] = proxyDict[message.args[i]];
            }
        }
        if (message.type == "CallProxyMethodAsProxy") {
            var obj = proxyDict[message.objectId][message.method].apply(proxyDict[message.objectId], message.args);
            var objectId = generateGUID();
            proxyDict[objectId] = obj;
            resolvePost(message.type, message.id, objectId);
        }
        else if (message.type == "CallProxyAsyncMethodAsProxy") {
            proxyDict[message.objectId][message.method].apply(proxyDict[message.objectId], message.args).then(obj => {

                var objectId = generateGUID();
                if (obj == undefined) {
                    objectId = undefined;
                }
                else {
                    proxyDict[objectId] = obj;
                }
                resolvePost(message.type, message.id, objectId);
            });
        }
        else if (message.type == "CallProxyMethod") {
            var obj = proxyDict[message.objectId][message.method].apply(proxyDict[message.objectId], message.args);
            resolvePost(message.type, message.id, obj);
        }
        else if (message.type == "CallResolve") {
            resolvers[message.id].apply(this, message.args);
        }
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