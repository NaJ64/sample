import { StartupLikeConstructor } from "./startup";
import { IWebHost } from "./web-host";

export interface IWebHostBuilder {
    build(): IWebHost;
    useStartup(startupConstructor: StartupLikeConstructor): IWebHostBuilder;
}
