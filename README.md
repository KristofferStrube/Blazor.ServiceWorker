[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](/LICENSE.md)
[![GitHub issues](https://img.shields.io/github/issues/KristofferStrube/Blazor.ServiceWorker)](https://github.com/KristofferStrube/Blazor.ServiceWorker/issues)
[![GitHub forks](https://img.shields.io/github/forks/KristofferStrube/Blazor.ServiceWorker)](https://github.com/KristofferStrube/Blazor.ServiceWorker/network/members)
[![GitHub stars](https://img.shields.io/github/stars/KristofferStrube/Blazor.ServiceWorker)](https://github.com/KristofferStrube/Blazor.ServiceWorker/stargazers)

<!-- [![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/KristofferStrube.Blazor.ServiceWorker?label=NuGet%20Downloads)](https://www.nuget.org/packages/KristofferStrube.Blazor.ServiceWorker/) -->

# Introduction
A Blazor wrapper for the [Service Workers](https://www.w3.org/TR/service-workers/) API.

The API makes it possible to interact with and register a Service Worker that can control the fetching of resources for the website before any other contexts exist. This enables developers to make Progressive Web Apps (PWA). This project implements a wrapper around the API for Blazor so that we can easily and safely interact and create a Service Workers.

# Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.ServiceWorker/

On each page you can find the corresponding code for the example in the top right corner.

On the *API Coverage Status* page you can get an overview over what parts of the API we support currently.

**This wrapper is still being developed, so support is very limited.**

# Issues
Feel free to open issues on the repository if you find any errors with the package or have wishes for features.

# Related repositories
This project uses the *Blazor.FileAPI* to return rich Blob objects in certain scenarios.
- https://github.com/KristofferStrube/Blazor.FileAPI

# Related articles
This repository was build with inspiration and help from the following series of articles:

- [How to communicate with Service Workers](https://felixgerschau.com/how-to-communicate-with-service-workers/)
- [Wrapping JavaScript libraries in Blazor WebAssembly/WASM](https://blog.elmah.io/wrapping-javascript-libraries-in-blazor-webassembly-wasm/)
- [Call anonymous C# functions from JS in Blazor WASM](https://blog.elmah.io/call-anonymous-c-functions-from-js-in-blazor-wasm/)
- [Using JS Object References in Blazor WASM to wrap JS libraries](https://blog.elmah.io/using-js-object-references-in-blazor-wasm-to-wrap-js-libraries/)
- [Blazor WASM 404 error and fix for GitHub Pages](https://blog.elmah.io/blazor-wasm-404-error-and-fix-for-github-pages/)
- [How to fix Blazor WASM base path problems](https://blog.elmah.io/how-to-fix-blazor-wasm-base-path-problems/)
