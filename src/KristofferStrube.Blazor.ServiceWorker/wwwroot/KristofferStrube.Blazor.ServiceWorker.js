export function getAttribute(object, attribute) { return object[attribute]; }

export async function getAttributeAsync(object, attribute) { return await object[attribute]; }

export function arrayBuffer(buffer) {
    var bytes = new Uint8Array(buffer);
    return bytes;
}

export function getNavigator() { return navigator; }

var resolvers = {}

export function registerMessageListener(container) {
    container.addEventListener("message", async (e) => {
        var message = e.data
        if (message.type == "ResolveGetProxyAttributeAsProxy") {
            resolvers[message.id].call(this, message.object);
        }
        else if (message.type == "ResolveGetProxyAttribute") {
            resolvers[message.id].call(this, message.object);
        }
        else if (message.type == "ResolveCallProxyMethodAsProxy") {
            resolvers[message.id].call(this, message.object);
        }
        else if (message.type == "ResolveCallProxyMethod") {
            resolvers[message.id].call(this, message.object);
        }
        else {
            DotNet.invokeMethodAsync("KristofferStrube.Blazor.ServiceWorker", `InvokeOn${message.type}Async`, message.id, message.eventId);
        }
    })
}

export async function getProxyAttributeAsProxy(container, id, attribute) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "GetProxyAttributeAsProxy", id: id, attribute: attribute };
        container.getRegistration("/").then(reg =>
            reg.active.postMessage(message)
        );
    })
    return await promise;
}

export async function getProxyAttribute(container, id, attribute) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "GetProxyAttribute", id: id, attribute: attribute };
        container.getRegistration("_content/KristofferStrube.Blazor.ServiceWorker/").then(reg =>
            reg.active.postMessage(message)
        );
    })
    return await promise;
}

export async function callProxyMethodAsProxy(container, id, method) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "CallProxyMethodAsProxy", id: id, method: method };
        container.getRegistration("_content/KristofferStrube.Blazor.ServiceWorker/").then(reg =>
            reg.active.postMessage(message)
        );
    })
    return await promise;
}

export async function callProxyMethod(container, id, method) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "CallProxyMethod", id: id, method: method };
        container.getRegistration("_content/KristofferStrube.Blazor.ServiceWorker/").then(reg =>
            reg.active.postMessage(message)
        );
    })
    return await promise;
}