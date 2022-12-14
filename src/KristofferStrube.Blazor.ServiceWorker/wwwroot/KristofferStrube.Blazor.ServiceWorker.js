export function isAttributeNullOrUndefined(object, attribute) {
    return object[attribute] == null || object[attribute] == undefined;
}

export function getAttribute(object, attribute) {
    return object[attribute];
}

export async function getAttributeAsync(object, attribute) {
    return await object[attribute];
}

export function arrayBuffer(buffer) {
    var bytes = new Uint8Array(buffer);
    return bytes;
}

export function isUndefined(object) {
    return object == undefined;
}

export function registerEventHandlerAsync(objRef, jSInstance, eventName, invokeMethod) {
    jSInstance.addEventListener(eventName, (e) => objRef.invokeMethodAsync(invokeMethod, DotNet.createJSObjectReference(e)));
}

export function getNavigator() { return navigator; }

var resolvers = {}

export function registerMessageListener(container) {
    container.addEventListener("message", async (e) => {
        var message = e.data
        if (message.type.startsWith("Resolve")) {
            resolvers[message.id].call(this, message.object);
            delete resolvers[message.id];
        }
        else {
            DotNet.invokeMethodAsync("KristofferStrube.Blazor.ServiceWorker", `InvokeOn${message.type}Async`, message.id, message.eventId);
        }
    })
}

export async function getProxyAttributeAsProxy(container, id, objectId, attribute) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "GetProxyAttributeAsProxy", id: id, objectId: objectId, attribute: attribute };
        var controller = container.controller;
        if (controller != null) {
            controller.postMessage(message);
        }
    })
    return await promise;
}

export async function getProxyAsyncAttributeAsProxy(container, id, objectId, attribute) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "GetProxyAsyncAttributeAsProxy", id: id, objectId: objectId, attribute: attribute };
        var controller = container.controller;
        if (controller != null) {
            controller.postMessage(message);
        }
    })
    return await promise;
}

export async function getProxyAttribute(container, id, objectId, attribute) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "GetProxyAttribute", id: id, objectId: objectId, attribute: attribute };
        var controller = container.controller;
        if (controller != null) {
            controller.postMessage(message);
        }
    })
    return await promise;
}

export async function callProxyMethodAsProxy(container, id, objectId, method, args = []) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "CallProxyMethodAsProxy", id: id, objectId: objectId, method: method, args: args };
        var controller = container.controller;
        if (controller != null) {
            controller.postMessage(message);
        }
    })
    return await promise;
}

export async function callProxyAsyncMethodAsProxy(container, id, objectId, method, args = []) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "CallProxyAsyncMethodAsProxy", id: id, objectId: objectId, method: method, args: args };
        var controller = container.controller;
        if (controller != null) {
            controller.postMessage(message);
        }
    })
    return await promise;
}

export async function callProxyMethod(container, id, objectId, method, args = []) {
    var promise = new Promise((resolve, _) => {
        resolvers[id] = resolve;
        var message = { type: "CallProxyMethod", id: id, objectId: objectId, method: method, args: args };
        var controller = container.controller;
        if (controller != null) {
            controller.postMessage(message);
        }
    })
    return await promise;
}

export async function resolveProxy(container, id, args) {
    var message = { type: "CallResolve", id: id, args: args };
    var controller = container.controller;
    if (controller != null) {
        controller.postMessage(message);
    }
}