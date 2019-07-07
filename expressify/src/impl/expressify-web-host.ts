import { ICertificateLoader, ICertificateOptions } from "cert-loader";
import express from "express";
import http from "http";
import https from "https";
import { Container } from "inversify";
import path from "path";
import { IEndPointOptions } from "../end-point-options";
import { IHostingOptions } from "../hosting-options";
import { IStartupLike, StartupLikeConstructor } from "../startup";
import { IWebHost } from "../web-host";

export class ExpressifyWebHost implements IWebHost {

    private readonly _environment: string;
    private readonly _configuration: Map<string, any>;
    private readonly _startupConstructor: StartupLikeConstructor | null;
    private readonly _hostingOptions: IHostingOptions;
    private readonly _certificateLoader: ICertificateLoader;
    private readonly _contentRootPath: string;

    private _inversifyContainer?: Container;
    private _expressApplication?: express.Application;
    private _startup?: IStartupLike;

    public constructor(
        environment: string,
        configuration: Map<string, any>,
        startupConstructor: StartupLikeConstructor | null,
        hostingOptions: IHostingOptions,
        certificateLoader: ICertificateLoader,
        contentRootPath?: string
    ) {
        this._environment = environment;
        this._configuration = configuration || new Map<string, any>();
        this._startupConstructor = startupConstructor;
        this._hostingOptions = hostingOptions;
        this._certificateLoader = certificateLoader;
        this._contentRootPath = contentRootPath || path.resolve("./");
    }

    public run(): void {
        (async () => {

            this._inversifyContainer = this.createInversifyContainer();
            this._expressApplication = this.createExpressApplication();
            this.useStartup();
            await this.listenAsync();

        })();
    }

    private createInversifyContainer(): Container {
        return new Container();
    }

    private createExpressApplication(): express.Application {
        return express();
    }

    private useStartup(): void {
        if (!this._startupConstructor) {
            return;
        }
        if (!this._inversifyContainer) {
            throw new Error("Inversify container has not been configured");
        }
        if (!this._expressApplication) {
            throw new Error("Express application has not been configured");
        }
        this._startup = new this._startupConstructor(this._configuration, this._environment);
        if (this._startup.configureServices) {
            this._startup.configureServices(this._inversifyContainer);
        }
        if (this._startup.configure) {
            this._expressApplication = this._startup.configure(this._expressApplication, this._inversifyContainer, this._environment) || this._expressApplication;
        }
    }

    private async listenAsync(): Promise<void> {
        try {
            
            // Get express app instance
            let expressApplication = this._expressApplication as express.Application;

            // Get HTTP / HTTPS binding options
            let hosting = this._hostingOptions;

            // Create list of server promises to resolve
            let serverPromises: Promise<string>[] = [];

            // Get list of endpoints to iterate over
            let endpointsList = this.getEndpoints(hosting);
            for(let endpoint of endpointsList) {

                let isHttps = endpoint.url.startsWith("https://");
                let isHttp = endpoint.url.startsWith("http://");
                let skipChars: number = 0;
                if (isHttp) {
                    skipChars = 7;
                }
                if (isHttps) {
                    skipChars = 8;
                }
                if (!isHttp && !isHttps) {
                    isHttp = true;
                }
                
                let hostNameAndPort = endpoint.url.substring(skipChars).split(":");
                let hostName = hostNameAndPort[0];
                let port = isHttp ? 80 : 443;
                if (hostNameAndPort.length > 1) {
                    port = parseInt(hostNameAndPort[1]);
                }

                let serverPromise: Promise<string>;
                if (isHttp) {
                    serverPromise = this.createHttpServerAsync(expressApplication, hostName, port);
                } else if (isHttps) {
                    serverPromise = this.createHttpsServerAsync(expressApplication, hostName, port, endpoint.certificate);
                } else {
                    serverPromise = Promise.reject("Server endpoint could not determine HTTP or HTTPS protocol for listener");
                }
                serverPromises.push(serverPromise);

            }

            // Will wait for all servers to startup or reject (throw exception) if any fails to start
            let listeningOnEndPoints = await Promise.all(serverPromises);
            listeningOnEndPoints = listeningOnEndPoints.sort();

            // Log
            console.log(`Hosting environment: ${this._environment || '(none)'}`);
            console.log(`Content root path: ${this._contentRootPath || '(none)'}`);
            for (let endPoint of listeningOnEndPoints) {
                console.log(`Now listening on: ${endPoint}`);
            }
            console.log(`Application started. Press Ctrl+C to shut down.`);

        } catch (err) {
            console.log("Listening error: ", (err as Error));
        }
    }

    private getEndpoints(hostingOptions: IHostingOptions): IEndPointOptions[] {
        let endpoints = Object.entries(hostingOptions.endPoints || {});
        return endpoints.map(x => x["1"]);
    }

    private async createHttpServerAsync(expressApp: express.Application, hostName: string, port: number): Promise<string> {
        if (!hostName) {
            throw new Error("HTTP host name not specified");
        }
        if (!port) {
            throw new Error("HTTP port not specified");
        }
        return await new Promise<string>((resolve, reject) => {
            try {
                http.createServer(expressApp).listen(port, hostName, () => {
                    resolve(`http://${hostName}:${port}`);
                });
            } catch (err) {
                reject(err);
            }
        });
    }

    private async createHttpsServerAsync(expressApp: express.Application, hostName: string, port: number, certificate?: ICertificateOptions): Promise<string> {
        if (!hostName) {
            throw new Error("HTTPS host name not specified");
        }
        if (!port) {
            throw new Error("HTTPS port not specified");
        }
        let httpsServerOptions = await this.loadHttpsServerOptionsAsync(certificate);
        return await new Promise<string>((resolve, reject) => {
            try {
                https.createServer(httpsServerOptions, expressApp).listen(port, hostName, () => {
                    resolve(`https://${hostName}:${port}`);
                });
            } catch (err) {
                reject(err);
            }
        });
    }

    private async loadHttpsServerOptionsAsync(options?: ICertificateOptions): Promise<https.ServerOptions> {
        if (!this._certificateLoader) {
            throw new Error("A certificate loader must be provided");
        }
        if (!options) {
            throw new Error("Certificate info must be provided for HTTPS endpoints!");
        }
        let useFile = !!options.path;
        if (useFile && !options.password) {
            throw new Error("A password must be provided when loading certificate(s) from file path");
        }
        let certificate = await this._certificateLoader.loadAsync(options);
        let serverOptions = {};
        if (useFile) {
            serverOptions = { pfx: certificate, passphrase: options.password };
        } else {
            serverOptions = { cert: certificate };
        }
        return serverOptions;
    }


}