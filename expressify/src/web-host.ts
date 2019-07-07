import { FileCertificateLoader } from "cert-loader";
import { JsonConfigurationLoader } from "config-loader";
import { ExpressifyWebHostBuilder } from "./impl/expressify-web-host-builder";
import { IWebHostBuilder } from "./web-host-builder";

export interface IWebHost {
    run(): void;
}

export namespace WebHost {
    export function createDefaultBuilder(...args: any[]): IWebHostBuilder {
        let env = process && process.env && process.env["NODE_ENV"] || "Development";
        return new ExpressifyWebHostBuilder(
            env, 
            new JsonConfigurationLoader(env), 
            new FileCertificateLoader(), 
            ...args
        );
    }
}