export function getAttribute(object, attribute) { return object[attribute]; }

export async function getAttributeAsync(object, attribute) { return await object[attribute]; }

export function arrayBuffer(buffer) {
    var bytes = new Uint8Array(buffer);
    return bytes;
}

export function isUndefined(object) {
    return object == undefined;
}

export function getNavigator() { return navigator; }

var resolvers = {}

export function registerMessageListener(container) {
    container.addEventListener("message", async (e) => {
        var message = e.data
        if (message.type.startsWith("Resolve")) {
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
        container.getRegistration().then(reg =>
            reg.active.postMessage(message)
        );
    })
    return await promise;
}

export async function getProxyAttribute(container, id, attribute) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "GetProxyAttribute", id: id, attribute: attribute };
        container.getRegistration().then(reg =>
            reg.active.postMessage(message)
        );
    })
    return await promise;
}

export async function callProxyMethodAsProxy(container, id, method, args = []) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "CallProxyMethodAsProxy", id: id, method: method, args: args };
        container.getRegistration().then(reg =>
            reg.active.postMessage(message)
        );
    })
    return await promise;
}

export async function callProxyAsyncMethodAsProxy(container, id, method, args = []) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "CallProxyAsyncMethodAsProxy", id: id, method: method, args: args };
        container.getRegistration().then(reg =>
            reg.active.postMessage(message)
        );
    })
    return await promise;
}

export async function callProxyMethod(container, id, method, args = []) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "CallProxyMethod", id: id, method: method, args: args };
        container.getRegistration().then(reg =>
            reg.active.postMessage(message)
        );
    })
    return await promise;
}

export async function resolveRespondWith(container, id, args) {
    var message = { type: "CallResolveRespondWith", id: id, args: args };
    container.getRegistration().then(reg =>
        reg.active.postMessage(message)
    );
}