import { ICertificateLoader } from "cert-loader";
import { IConfigurationLoader } from "config-loader";
import { IHostingOptions } from "../hosting-options";
import { StartupLikeConstructor } from "../startup";
import { IWebHost } from "../web-host";
import { IWebHostBuilder } from "../web-host-builder";
import { ExpressifyWebHost } from "./expressify-web-host";

export class ExpressifyWebHostBuilder implements IWebHostBuilder {

    // TODO:  Update this object to use toggle-able static file server middleware

    private readonly _hostingSection: string = "kestrel";  // Default in ASP.NET Core appsettings
    private readonly _environment: string;
    private readonly _configurationLoader: IConfigurationLoader;
    private readonly _certificateLoader: ICertificateLoader;
    private _startupConstructor: StartupLikeConstructor | null;

    public constructor(environment: string, configurationLoader: IConfigurationLoader, certificateLoader: ICertificateLoader, ...args: any[]) {
        this._environment = environment;
        this._configurationLoader = configurationLoader;
        this._certificateLoader = certificateLoader;
        this._startupConstructor = null;
    }

    public useStartup(startupConstructor: StartupLikeConstructor) {
        this._startupConstructor = startupConstructor;
        return this;
    }

    private buildHostingOptions(configuration: Map<string, any>): IHostingOptions {
        let defaultHostingOptions: IHostingOptions = {
            endPoints: {
                defaultHttp: {
                    url: "http://localhost:5000"
                }
            }
        };
        let hostingSection = this._hostingSection;
        return configuration.get(hostingSection) || defaultHostingOptions;
    }

    public build(): IWebHost {

        // Get all arguments from builder
        let environment = this._environment;
        let configuration = this._configurationLoader.load();
        let startup = this._startupConstructor;
        let options = this.buildHostingOptions(configuration);
        let certificateLoader = this._certificateLoader;

        // Create new web host instance
        let webHost = new ExpressifyWebHost(
            environment,
            configuration,
            startup,
            options,
            certificateLoader
        );

        // Return the new web host object
        return webHost;

    }

}